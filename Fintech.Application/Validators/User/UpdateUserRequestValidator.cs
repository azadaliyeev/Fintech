using Fintech.Domain.Models.User.Update;
using FluentValidation;

namespace Fintech.Application.Validators.User;

public class UpdateUserRequestValidator:AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {

        RuleFor(x => x)
            .Custom((request, context) =>
            {
                if (string.IsNullOrWhiteSpace(request.FirstName) && string.IsNullOrWhiteSpace(request.LastName)
                                                                 && string.IsNullOrWhiteSpace(request.Country))
                {
                    context.AddFailure("At least one field must be provided for update.");
                }
            });
        
        
        RuleFor(x=>x.UserId).NotEmpty().WithMessage("User id can not be empty");
    }
}
