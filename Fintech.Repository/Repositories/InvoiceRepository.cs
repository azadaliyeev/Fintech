using System.Data;
using Fintech.Domain.Entities;
using Fintech.Domain.Repositories;
using Fintech.Repository.DbCotext;

namespace Fintech.Repository.Repositories;

public class InvoiceRepository(FintechDbContext context, Lazy<IDbConnection> connection, IDbTransaction transaction)
    : BaseSqlRepository(context, connection, transaction), IInvoiceRepository
{
    public Task AddAsync(Invoice invoice)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Invoice invoice)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Invoice invoice)
    {
        throw new NotImplementedException();
    }

    public Task GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}