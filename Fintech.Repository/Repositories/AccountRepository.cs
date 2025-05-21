using System.Data;
using System.Linq.Expressions;
using Fintech.Domain.Entities;
using Fintech.Domain.Repositories;
using Fintech.Repository.DbCotext;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Repository.Repositories;

public class AccountRepository(FintechDbContext context, Lazy<IDbConnection> connection, IDbTransaction dbTransaction)
    : BaseSqlRepository(context, connection, dbTransaction), IAccountRepository
{
    public async Task AddAsync(Account account) => await Context.Accounts.AddAsync(account);

    public void UpdateAsync(Account account) => Context.Accounts.Update(account);

    public void DeleteAsync(Account account) => Context.Accounts.Remove(account);

    public async Task<Account?> GetByIdAsync(string id) => await Context.Accounts.FindAsync(id);

    public IQueryable<Account> GetAll() => Context.Accounts.AsQueryable().AsNoTracking();

    public async Task<List<Account>> GetByUserIdAsync(string userId) =>
        await Context.Accounts.Where(x => x.UserId == userId).ToListAsync();

    public IQueryable<Account> Where(Expression<Func<Account, bool>> predicate) => Context.Accounts.Where(predicate);

    public async Task<Account> GetByIbanAsync(string iban) =>
        await Context.Accounts.FirstOrDefaultAsync(x => x.Iban == iban) ?? new Account();

    public async Task BlockAccountsByUserIdAsync(string userId, string status)
    {
        await Context.Accounts.Where(x => x.UserId == userId)
            .ExecuteUpdateAsync(s => s.SetProperty(x => x.Status, status));
    }

   
}