using System.Linq.Expressions;
using Fintech.Domain.Entities;

namespace Fintech.Domain.Repositories;

public interface ICurrencyRepository
{
    void Update(Currency currency);
    Task<Currency?> GetCurrency();

    Task AddAsync(Currency currency);

    Task<bool> ExistAsync();

    IQueryable<Currency> Where(Expression<Func<Currency, bool>> predicate);

    Task<List<Currency>> GetAllAsync();
    
    Task<Currency?> GetByCodeAsync(string code);
}