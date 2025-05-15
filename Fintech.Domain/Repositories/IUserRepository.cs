using Fintech.Domain.Entities;

namespace Fintech.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    IQueryable<User> GetAll();
    Task AddAsync(User account);
    void Update(User account);
    void DeleteAsync(User account);
    Task<User?> GetByIdAsync(string id);

    Task<bool> ExistByEmailAsync(string email);
    Task<bool> ExistByPhoneNumberAsync(string phoneNumber);
    Task<bool> ExistByIdAsync(string id);

    Task<int> DeleteUserAsync(string id);
}