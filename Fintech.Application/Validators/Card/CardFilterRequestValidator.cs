using Fintech.Domain.Models.Card.FilteredRequest;
using Fintech.Shared.Enums;
using Fintech.Shared.Extension;
using Fintech.Shared.Helpers;
using FluentValidation;

namespace Fintech.Application.Validators.Card;

public class CardFilterRequestValidator : AbstractValidator<CardFilteredRequest>
{
    private List<string> _validFilters = new()
    {
        "id",
        "pan",
        "cvv",
        "currency",
        "cardname",
        "cardtype",
        "cardbrand",
        "balance"
    };

    public CardFilterRequestValidator()
    {
        RuleFor(x => x.Pan).NotEmpty().WithMessage("Pan cannot be empty")
            .Must(PanValidator.ValidatePan).WithMessage(ErrorMessages.NotValidPanFormat.GetMessage());

        RuleFor(x => x.Filters).Must(x => x.All(x
                => _validFilters.Contains(x.ToLowerInvariant())))
            .WithMessage(" Invalid filter(s) provided. Allowed filters are: " + string.Join(", ", _validFilters))
            .When(x => x.Filters?.Any() == true);
    }
}