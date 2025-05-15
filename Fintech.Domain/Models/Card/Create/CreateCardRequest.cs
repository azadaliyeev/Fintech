using Fintech.Shared.Enums;

namespace Fintech.Domain.Models.Card.Create;

public record CreateCardRequest(
    string UserId,
    string CardName,
    CardType CardType,
    CardBrand CardBrand,
    Shared.Enums.Currencies Currencies);