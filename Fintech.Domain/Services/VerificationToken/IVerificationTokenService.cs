using Fintech.Domain.Entities;
using Fintech.Shared.ServiceResults;

namespace Fintech.Domain.Services.VerificationToken;

public interface IVerificationTokenService
{
    Task<Entities.VerificationToken> CreateTokenAsync(string userId);
    Task<TokenValidationResult> VerifyUserTokenAsync(string token);

    Task DeleteTokensAsync(List<User> users);
}