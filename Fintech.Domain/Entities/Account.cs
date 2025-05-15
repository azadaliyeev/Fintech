namespace Fintech.Domain.Entities;

public class Account
{
    public string Id { get; set; } = null!;
    public string Iban { get; set; } = null!;
    public decimal Balance { get; set; }
    public string AccountType { get; set; } = null!;
    public DateTime CreateDate { get; set; }
    public string Currency { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string UserId { get; set; } = null!;


    public User User { get; set; } = null!;

}