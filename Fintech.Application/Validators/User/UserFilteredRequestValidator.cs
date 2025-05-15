using Fintech.Domain.Models.User.FilteredRequest;
using FluentValidation;

namespace Fintech.Application.Validators.User;

public class UserFilteredRequestValidator : AbstractValidator<UserFilteredRequest>
{
    private readonly List<string> _allowedFields = new List<string>
    {
        "id",
        "firstname",
        "lastname",
        "email",
        "phonenumber",
        "country",
        "dateofbirth"
    };

    public UserFilteredRequestValidator()
    {
        RuleFor(x => x.Fields)
            .Must(fields => fields.All(field => _allowedFields.Contains(field.ToLowerInvariant())))
            .WithMessage("Invalid field(s) provided. Allowed fields are: " + string.Join(", ", _allowedFields))
            .When(x => x.Fields?.Any() == true);


        RuleFor(x => x.UserId).NotEmpty().WithMessage("User id can not be empty");
    }
};