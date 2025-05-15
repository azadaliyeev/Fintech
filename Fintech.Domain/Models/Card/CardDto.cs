namespace Fintech.Domain.Models.Card;

public record CardDto(
    string Id,
    string Pan,
    string Cvv,
    string Currency,
    string CardName,
    string CardType,
    string CardBrand,
    decimal Balance);