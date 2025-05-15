using Fintech.Shared.Enums;
using Fintech.Shared.Extension;
using Fintech.Shared.Helpers;
using FluentValidation;

namespace Fintech.Application.Validators.Card;

public class
    CardVerificationRequestValidator : AbstractValidator<
    Fintech.Domain.Models.Card.Verification.CardVerificationRequest>
{
    public CardVerificationRequestValidator()
    {
        RuleFor(x => x.Pan).NotEmpty()
            .WithMessage("Pan can not be empty").Must(PanValidator.ValidatePan)
            .WithMessage(ErrorMessages.NotValidPanFormat.GetMessage());

        RuleFor(x => x.Cvv).Length(3).WithMessage("Cvv must be 3 characters").NotEmpty()
            .WithMessage("Cvv can not be empty");

        RuleFor(x => x.ExpireDate).NotEmpty().WithMessage("ExpireDate can not be empty");
    }
}