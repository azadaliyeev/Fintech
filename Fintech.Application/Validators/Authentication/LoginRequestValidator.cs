using Fintech.Domain.Models.Authentication.Login;
using FluentValidation;

namespace Fintech.Application.Validators.Authentication;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email format is not valid")
            .NotEmpty().WithMessage("Email can not be empty");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password can not be empty")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
    }
}