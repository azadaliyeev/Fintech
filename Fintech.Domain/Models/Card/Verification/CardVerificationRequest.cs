namespace Fintech.Domain.Models.Card.Verification;

public record CardVerificationRequest(string Pan, string Cvv, DateTime ExpireDate);