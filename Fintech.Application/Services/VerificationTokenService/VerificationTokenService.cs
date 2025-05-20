using Fintech.Domain.Entities;
using Fintech.Domain.Services.VerificationToken;
using Fintech.Domain.UnitOfWork;
using Fintech.Shared.ServiceResults;

namespace Fintech.Application.Services.VerificationTokenService;

public class VerificationTokenService(IUnitOfWork unitOfWork) : IVerificationTokenService
{
    public async Task<VerificationToken> CreateTokenAsync(string userId)
    {
        var verificationToken = new VerificationToken
        {
            Id = Guid.NewGuid().ToString(),
            UserId = userId,
            Token = Guid.NewGuid().ToString()
        };

        await unitOfWork.VerificationTokenRepository.AddAsync(verificationToken);
        await unitOfWork.CommitAsync();

        return verificationToken;
    }

    public async Task<TokenValidationResult> VerifyUserTokenAsync(string token)
    {
        var verificationToken = await unitOfWork.VerificationTokenRepository.GetByTokenAsync(token);

        if (verificationToken is null)
            return TokenValidationResult.Fail("Verification token not found");

        if (verificationToken.IsUsed)
            return TokenValidationResult.Fail("Verification token already used");

        if (verificationToken.ExpireDate < DateTime.UtcNow)
            return TokenValidationResult.Fail("Verification token expired");

        var user = await unitOfWork.UserRepository.GetByIdAsync(verificationToken.UserId);
        if (user is { Id: "" })
            return TokenValidationResult.Fail("User not found");
        verificationToken.IsUsed = true;
        unitOfWork.VerificationTokenRepository.Update(verificationToken);
        return TokenValidationResult.Success("Verification token verified successfully", user!.Id);
    }

    public async Task DeleteTokensAsync(List<User> users)
    {
        var usersId = users.Select(x => x.Id).ToList();
        await unitOfWork.VerificationTokenRepository.DeleteTokensAysnc(usersId);
    }
}