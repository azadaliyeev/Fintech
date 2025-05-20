using Fintech.Domain.Models.Authentication.SignUp;
using FluentValidation;

namespace Fintech.Application.Validators.Authentication;

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        
        RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(@".*\d.*")
            .Matches(@".*[A-Z].*")
            .WithMessage("Password must contain at least one digit and one uppercase letter");


        RuleFor(x => x.PhoneNumber).Matches("^\\+?[1-9]\\d{1,14}$")
            .WithMessage("Invalid phone number format");
    }
}