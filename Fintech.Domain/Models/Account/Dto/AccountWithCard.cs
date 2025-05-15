using Fintech.Domain.Models.Card;

namespace Fintech.Domain.Models.Account.Dto;

public record AccountWithCard(
    string Id,
    string Iban,
    decimal Balance,
    string AccountType,
    string Currency,
    string Status,
    List<CardDto> Cards);
    