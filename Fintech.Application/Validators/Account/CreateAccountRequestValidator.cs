using Fintech.Domain.Models.Account.Create;
using Fintech.Shared.Enums;
using FluentValidation;

namespace Fintech.Application.Validators.Account;

public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountRequestValidator()
    {
        RuleFor(x => x.Balance).GreaterThanOrEqualTo(0)
            .WithMessage("Balance must be greater than or equal to 0");

        RuleFor(x => x.UserId).Length(36)
            .WithMessage("UserId must be 36 characters long");

        RuleFor(x => x.AccountType).IsInEnum()
            .WithMessage("Account Type must be either Master or Default");

        RuleFor(x => x.Currencies).IsInEnum().WithMessage(" Currency must be either Azn, Usd, Eur, Try or Rub");
    }
}