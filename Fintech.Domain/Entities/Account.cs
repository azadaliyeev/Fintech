using System.ComponentModel.DataAnnotations;

namespace Fintech.Domain.Entities;

public class Account(
    string Id,
    string Iban,
    decimal? Balance,
    string AccountType,
    DateTime CreateDate,
    string Currency,
    string? Status,
    string UserId)
{
    public Account() : this(string.Empty, string.Empty, 0, string.Empty, DateTime.UtcNow, string.Empty, null,
        string.Empty)
    {
    }


    [StringLength(100)] public string Id { get; set; } = null!;
    public string Iban { get; set; } = null!;
    public decimal Balance { get; set; }
    public string AccountType { get; set; } = null!;
    public DateTime CreateDate { get; set; }
    public string Currency { get; set; } = null!;
    public string? Status { get; set; } = null!;
    public string UserId { get; set; } = null!;


    public User User { get; set; } = null!;
}