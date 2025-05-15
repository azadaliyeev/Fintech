namespace Fintech.Domain.Models.User.Update;

public record UpdateUserRequest(string FirstName, string LastName, string Country,string UserId);