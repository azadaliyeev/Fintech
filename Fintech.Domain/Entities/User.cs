namespace Fintech.Domain.Entities;

public class User
{
    public string Id { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? Country { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Password { get; set; } = null!;

    public string Status { get; set; } = null!;

    public bool IsVerified { get; set; } = false;

    public virtual List<Account>? Accounts { get; set; }
    public virtual List<Card>? Cards { get; set; }
    public virtual VerificationToken? VerificationToken { get; set; }
}