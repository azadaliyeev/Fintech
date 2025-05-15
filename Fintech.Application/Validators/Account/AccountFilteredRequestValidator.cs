using Fintech.Domain.Models.Account.FilteredRequest;
using FluentValidation;

namespace Fintech.Application.Validators.Account;

public class AccountFilteredRequestValidator : AbstractValidator<AccountFilteredRequest>
{
    List<string> _allowedFields = new List<string>
    {
        "id",
        "iban",
        "balance",
        "accounttype",
        "currency",
        "status"
    };

    public AccountFilteredRequestValidator()
    {
        RuleFor(x => x.Iban).NotEmpty().WithMessage("Iban can not be empty");

        RuleFor(x => x.Fields)
            .Must(fields => fields.All(field => _allowedFields.Contains(field.ToLowerInvariant())))
            .WithMessage("Invalid field(s) provided. Allowed fields are: " + string.Join(", ", _allowedFields))
            .When(x => x.Fields?.Any() == true);
    }
}