using System.Data;
using System.Linq.Expressions;
using Fintech.Domain.Entities;
using Fintech.Domain.Repositories;
using Fintech.Repository.DbCotext;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Repository.Repositories;

public class CurrencyRepository(FintechDbContext context, Lazy<IDbConnection> connection, IDbTransaction _transaction)
    : BaseSqlRepository(context, connection, _transaction), ICurrencyRepository
{
    public void Update(Currency currency) => Context.Currencies.Update(currency);

    public async Task<Currency?> GetCurrency() => await Context.Currencies.SingleOrDefaultAsync();

    public async Task AddAsync(Currency currency) => await Context.Currencies.AddAsync(currency);

    public async Task<bool> ExistAsync() => await Context.Currencies.AnyAsync();

    public IQueryable<Currency> Where(Expression<Func<Currency, bool>> predicate) =>
        Context.Currencies.Where(predicate);

    public async Task<List<Currency>> GetAllAsync() => await Context.Currencies.ToListAsync();

    public async Task<Currency?> GetByCodeAsync(string code) =>
        await Context.Currencies.Where(x => x.CurrencyCode == code).FirstOrDefaultAsync();
}