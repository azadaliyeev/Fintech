namespace Fintech.Shared.ServiceResults;

public class TokenValidationResult
{
    public bool IsValid { get; set; }
    public string? Message { get; set; }
    public string? UserId { get; set; }


    public static TokenValidationResult Success(string message,string userId)
    {
        return new TokenValidationResult
        {
            IsValid = true,
            Message = message,
            UserId = userId
        };
    }
    

    public static TokenValidationResult Fail(string message)
    {
        return new TokenValidationResult
        {
            IsValid = false,
            Message = message
        };
    }
}