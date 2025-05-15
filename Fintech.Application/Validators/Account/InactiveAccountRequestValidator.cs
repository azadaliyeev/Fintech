using Fintech.Domain.Models.Account.Inactive;
using Fintech.Shared.Helpers;
using FluentValidation;

namespace Fintech.Application.Validators.Account;

public class InactiveAccountRequestValidator : AbstractValidator<InactiveAccountRequest>
{
    public InactiveAccountRequestValidator()
    {
        RuleFor(x => x.Iban).NotEmpty().WithMessage("Iban can not be empty")
            .Must(IbanValidator.IsValid).WithMessage("Not valid iban format");

        RuleFor(x => x.UserId).NotEmpty().WithMessage("User id can not be empty");
    }
}