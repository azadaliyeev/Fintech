using System.ComponentModel.DataAnnotations;

namespace Fintech.Domain.Entities;

public class User(
    string id,
    string? firstName,
    string? lastName,
    string? email,
    string phoneNumber,
    string country,
    DateTime dateOfBirth,
    string password,
    string? status,
    bool isVerified)
{
    public User() : this(string.Empty, string.Empty, string.Empty, null, string.Empty, string.Empty,
        DateTime.UtcNow, string.Empty,
        null, false)
    {
    }

    [StringLength(100)] public string Id { get; set; } = id;
    public string? FirstName { get; set; } = firstName;
    public string? LastName { get; set; } = lastName;
    public string? Email { get; set; } = email;
    public string PhoneNumber { get; set; } = phoneNumber;
    public string? Country { get; set; } = country;
    public DateTime DateOfBirth { get; set; } = dateOfBirth;
    public string Password { get; set; } = password;

    public string? Status { get; set; } = status;

    public bool IsVerified { get; set; } = isVerified;

    public virtual List<Account> Accounts { get; set; } = null!;
    public virtual List<Card>? Cards { get; set; }
    public virtual VerificationToken? VerificationToken { get; set; }
}