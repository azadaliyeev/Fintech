using Fintech.Domain.Models.Card;
using Fintech.Domain.Models.Card.Block;
using Fintech.Domain.Models.Card.Create;
using Fintech.Domain.Models.Card.FilteredRequest;
using Fintech.Domain.Models.Card.Inactive;
using Fintech.Domain.Models.Card.Verification;
using Fintech.Shared.ServiceResults;

namespace Fintech.Domain.Services.Cards;

public interface ICardService
{
    Task<ServiceResult<CardDto>> CreateAsync(CreateCardRequest request);
    Task<ServiceResult<CardDto>> GetCardByPanAsync(string pan);
    Task<ServiceResult<Dictionary<string, object>>> GetCardByFilterAsync(CardFilteredRequest request);
    Task<ServiceResult> VerificationCardAsync(CardVerificationRequest request);
    Task<ServiceResult> InactiveCardAsync(InactiveCardRequest request);

    Task<ServiceResult> BlockCard(BlockCardRequest request);
}