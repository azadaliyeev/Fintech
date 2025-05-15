namespace Fintech.Domain.Entities;

public class Invoice
{
    public string Id { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public string ItemDescription { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal AmountPerQuantity { get; set; }
    public decimal SubTotal { get; set; }
    public string UserId { get; set; } = null!;
    public string Status { get; set; } = null!;
}