using System.Data;
using System.Linq.Expressions;
using Fintech.Domain.Entities;
using Fintech.Domain.Models.User;
using Fintech.Domain.Repositories;
using Fintech.Repository.DbCotext;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Repository.Repositories;

public class UserRepository(FintechDbContext context, Lazy<IDbConnection> connection, IDbTransaction transaction)
    : BaseSqlRepository(context, connection, transaction), IUserRepository
{
    public IQueryable<User> GetAll() => Context.Users.AsQueryable();
    public async Task AddAsync(User account) => await Context.Users.AddAsync(account);

    public void Update(User account) => Context.Users.Update(account);

    public void DeleteAsync(User account) => Context.Users.Remove(account);

    public async Task<User> GetByIdAsync(string id)
    {
        var user = await Context.Users.FindAsync(id);
        return user ?? new User();
    }

    public async Task<bool> ExistByEmailAsync(string email) =>
        await Context.Users.AnyAsync(x => x.Email == email);

    public async Task<bool> ExistByPhoneNumberAsync(string phoneNumber) =>
        await Context.Users.AnyAsync(x => x.PhoneNumber == phoneNumber);

    public async Task<bool> ExistByIdAsync(string id) => await Context.Users.AnyAsync(x => x.Id == id);

    public async Task<User?> GetByEmailAsync(string email)
    {
        var res = await Context.Users.FirstOrDefaultAsync(x => x.Email == email);
        return res;
    }

    public async Task<int> DeleteUserAsync(string id)
    {
        return await ExecuteAsync("CALL delete_user(@us_id)", new { us_id = id });
    }

    public IQueryable Where(Expression<Func<User, bool>> predicate) => Context.Users.Where(predicate);

    public async Task DeleteDataFromNonVerifiedUsers(List<User> users)
    {
        var usersId = users.Select(x => x.Id).ToList();

        await Context.Users.Where(x => usersId.Contains(x.Id))
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.FirstName, (string?)null)
                .SetProperty(x => x.LastName, (string?)null)
                .SetProperty(x => x.Email, (string?)null));
    }

    
}