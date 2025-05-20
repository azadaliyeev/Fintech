using Fintech.Domain.Models.User.Verification;
using Fintech.Shared.Enums;
using Fintech.Shared.Extension;
using FluentValidation;

namespace Fintech.Application.Validators.User;

public class UserVerificationRequestValidator : AbstractValidator<UserVerificationRequest>
{
    public UserVerificationRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("Not valid email format").NotEmpty()
            .WithMessage(ErrorMessages.Null.GetMessage());
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name can not be empty")
            .MinimumLength(3).WithMessage("First name must be at least 3 characters long");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name can not be empty")
            .MinimumLength(3).WithMessage("Last name must be at least 3 characters long");
    }
}