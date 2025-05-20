namespace Fintech.Domain.Models.Authentication.SignUp;

public record SignUpRequest(string PhoneNumber, string Password, string Country, DateTime DateOfBirth);