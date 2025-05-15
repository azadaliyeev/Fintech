using System.Data;
using Fintech.Domain.Entities;
using Fintech.Domain.Repositories;
using Fintech.Repository.DbCotext;

namespace Fintech.Repository.Repositories;

public class TransactionRepository(FintechDbContext context, Lazy<IDbConnection> connection, IDbTransaction transaction)
    : BaseSqlRepository(context, connection, transaction), ITransactionRepository
{
    public Task AddAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public Task GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}