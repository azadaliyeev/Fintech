namespace Fintech.Domain.Models.Payments.Requests;

public class PaymentCardToCardRequest(
    string fromUserId,
    string fromPan,
    decimal amount,
    string toPan,
    string currency,
    string? description,
    string transactionId)
{
    public PaymentCardToCardRequest() : this(string.Empty, string.Empty, -1, string.Empty, string.Empty, string.Empty,
        string.Empty)
    {
    }

    public string FromUserId { get; set; } = fromUserId;
    public string FromPan { get; set; } = fromPan;
    public decimal Amount { get; set; } = amount;
    public string ToPan { get; set; } = toPan;
    public string Currency { get; set; } = currency;
    public string? Description { get; set; } = description;
    public string TransactionId { get; set; } = transactionId;
}