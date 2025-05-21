using Fintech.Domain.Models.Card.Inactive;
using Fintech.Domain.Services.Cards;
using Fintech.Shared.Enums;
using Fintech.Shared.Extension;
using Fintech.Shared.Helpers;
using FluentValidation;

namespace Fintech.Application.Validators.Card;

public class InactiveCardRequestValidator : AbstractValidator<InactiveCardRequest>
{
    public InactiveCardRequestValidator(ICardService cardService)
    {
        RuleFor(x => x.Pan).NotEmpty().WithMessage("Pan can not be empty")
            .Must(cardService.ValidatePan).WithMessage(ErrorMessages.NotValidPanFormat.GetMessage());
    }
}