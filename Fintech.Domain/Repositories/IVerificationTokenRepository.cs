using Fintech.Domain.Entities;

namespace Fintech.Domain.Repositories;

public interface IVerificationTokenRepository
{
    Task AddAsync(VerificationToken verificationToken);
    void Update(VerificationToken verificationToken);
    void Delete(VerificationToken verificationToken);
    Task<VerificationToken?> GetByUserId(string userId);

    Task<VerificationToken?> GetByTokenAsync(string token);
}