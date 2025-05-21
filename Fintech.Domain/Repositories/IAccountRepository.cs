using System.Linq.Expressions;
using Fintech.Domain.Entities;

namespace Fintech.Domain.Repositories;

public interface IAccountRepository
{
    Task AddAsync(Account account);
    void UpdateAsync(Account account);
    void DeleteAsync(Account account);
    Task<Account?> GetByIdAsync(string id);
    IQueryable<Account> GetAll();
    IQueryable<Account> Where(Expression<Func<Account, bool>> predicate);

    Task<Account> GetByIbanAsync(string iban);

    Task BlockAccountsByUserIdAsync(string userId, string status);

}