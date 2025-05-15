using Fintech.Domain.Models.Currency.Convert;
using FluentValidation;

namespace Fintech.Application.Validators.Currency;

public class ConvertCurrencyRequestValidator : AbstractValidator<ConvertCurrencyRequest>
{
    public ConvertCurrencyRequestValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0");
        RuleFor(x => x.FromCurrencies)
            .IsInEnum()
            .WithMessage("FromCurrency must be a valid currency");
        RuleFor(x => x.ToCurrencies)
            .IsInEnum()
            .WithMessage("ToCurrency must be a valid currency");
    }
}