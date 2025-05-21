namespace Fintech.Domain.Models.User;

public record UserDto(string Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Country,
    DateTime? DateOfBirth);