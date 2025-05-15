using Fintech.Domain.Models.Card.Inactive;
using Fintech.Shared.Helpers;
using FluentValidation;

namespace Fintech.Application.Validators.Card;

public class InactiveCardRequestValidator : AbstractValidator<InactiveCardRequest>
{
    public InactiveCardRequestValidator()
    {
        RuleFor(x => x.Pan).NotEmpty().WithMessage("Pan can not be empty")
            .Must(PanValidator.ValidatePan);
    }
}