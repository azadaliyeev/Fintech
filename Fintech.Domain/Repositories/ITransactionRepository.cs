using Fintech.Domain.Entities;

namespace Fintech.Domain.Repositories;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task UpdateAsync(Transaction transaction);
    Task DeleteAsync(Transaction transaction);
    Task GetByIdAsync(string id);
}