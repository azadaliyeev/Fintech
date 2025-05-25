namespace Fintech.Domain.Models.Payments.Responses;

public class PaymentCardToCardResponse(
    string requestedId,
    string transactionId,
    decimal amount,
    string currency,
    string toPan)
{
    public PaymentCardToCardResponse() : this(string.Empty, string.Empty, -1, string.Empty, string.Empty)
    {
    }

    public string RequestedId { get; set; } = requestedId;
    public string TransactionId { get; set; } = transactionId;
    public decimal Amount { get; set; } = amount;
    public string Currency { get; set; } = currency;
    public string ToPan { get; set; } = toPan;
}