namespace Fintech.Domain.Models.User.Verification;

public record UserVerificationRequest(string FirstName, string LastName, string Email,string UserId);