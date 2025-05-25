namespace Fintech.Domain.Models.Payments.Responses;

public class TransactionStatusResponse(string transactionId, string status)
{
    public TransactionStatusResponse() : this(string.Empty, string.Empty)
    {
    }

    public string TransactionId { get; set; } = transactionId;
    public string Status { get; set; } = status;
}