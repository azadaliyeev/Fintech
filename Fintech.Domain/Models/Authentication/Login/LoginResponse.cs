namespace Fintech.Domain.Models.Authentication.Login;

public record LoginResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
);