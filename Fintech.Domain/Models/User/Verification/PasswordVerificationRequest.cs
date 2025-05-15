namespace Fintech.Domain.Models.User.Verification;

public record PasswordVerificationRequest(string Password,string Token);