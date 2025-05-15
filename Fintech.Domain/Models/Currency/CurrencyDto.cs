namespace Fintech.Domain.Models.Currency;

public record CurrencyDto(string Currency,
    decimal ExchangeRate,
    decimal BuyRate,
    decimal SellRate);