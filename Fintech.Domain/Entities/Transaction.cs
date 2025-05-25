namespace Fintech.Domain.Entities;

public class Transaction
{
    public string Id { get; set; } = null!;
    public string TransactionId { get; set; }
    public DateTime CreateDate { get; set; }
    public string TransactionType { get; set; } = null!;
    public string TransactionStatus { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public decimal Amount { get; set; }
    public string FromAccountId { get; set; } = null!;
    public string ToAccountId { get; set; } = null!;
    public string UserId { get; set; } = null!;
}