using Fintech.Domain.Entities;

namespace Fintech.Domain.Repositories;

public interface IInvoiceRepository
{
    Task AddAsync(Invoice invoice);
    Task UpdateAsync(Invoice invoice);
    Task DeleteAsync(Invoice invoice);
    Task GetByIdAsync(string id);
}