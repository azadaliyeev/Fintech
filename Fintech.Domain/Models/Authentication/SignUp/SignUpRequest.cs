namespace Fintech.Domain.Models.Authentication.SignUp;

public record SignUpRequest(string Email,string Password,string PhoneNumber,string Country,DateTime DateOfBirth);