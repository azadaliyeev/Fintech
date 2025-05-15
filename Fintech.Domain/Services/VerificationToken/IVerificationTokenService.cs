using Fintech.Shared.ServiceResults;

namespace Fintech.Domain.Services.VerificationToken;

public interface IVerificationTokenService
{
    Task<Entities.VerificationToken> CreateTokenAsync(string userId);
    Task<TokenValidationResult> VerifyUserTokenAsync(string token);
}