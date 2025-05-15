using Fintech.Domain.Models.Card.Create;
using Fintech.Shared.Enums;
using FluentValidation;

namespace Fintech.Application.Validators.Card;

public class CreateCardRequestValidator : AbstractValidator<CreateCardRequest>
{
    public CreateCardRequestValidator()
    {
        RuleFor(x => x.CardBrand).Must(x => x == CardBrand.Visa || x == CardBrand.Master)
            .WithMessage("Card Brand must be either Visa or MasterCard");

        RuleFor(x => x.CardType).Must(x => x == CardType.DebitCard || x == CardType.VirtualCard)
            .WithMessage("Card Type must be either DebitCard or VirtualCard");

        RuleFor(x => x.CardName)
            .NotEmpty().WithMessage("Card Name can not be empty")
            .Length(1, 50).WithMessage("Card Name must be between 1 and 50 characters long");

    }
}