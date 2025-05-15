namespace Fintech.Domain.Entities;

public class Currency
{
    public string Id { get; set; } = null!;
    public string CurrencyCode { get; set; } = null!;
    public decimal ExchangeRate { get; set; }

    public decimal BuyRate { get; set; }
    public decimal SellRate { get; set; }
    public DateTime? LastUpdated { get; set; } 
    
}
