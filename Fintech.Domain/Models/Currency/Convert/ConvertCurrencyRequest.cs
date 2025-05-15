namespace Fintech.Domain.Models.Currency.Convert;

public record ConvertCurrencyRequest(
    decimal Amount,
    Shared.Enums.Currencies FromCurrencies,
    Shared.Enums.Currencies ToCurrencies);