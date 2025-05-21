using Fintech.Domain.Models.Card.Block;
using Fintech.Domain.Services.Cards;
using Fintech.Shared.Enums;
using Fintech.Shared.Extension;
using Fintech.Shared.Helpers;
using FluentValidation;

namespace Fintech.Application.Validators.Card;

public class BlockCardRequestValidator : AbstractValidator<BlockCardRequest>
{
    public BlockCardRequestValidator(ICardService cardService)
    {
        RuleFor(x => x.Pan).NotEmpty().WithMessage("Pan can not be empty")
            .Must(cardService.ValidatePan).WithMessage(ErrorMessages.NotValidPanFormat.GetMessage());

        RuleFor(x => x.Userid).NotEmpty().WithMessage("UserId can not be empty");
    }
}