using Fintech.Domain.Models.Card.Block;
using Fintech.Shared.Enums;
using Fintech.Shared.Extension;
using Fintech.Shared.Helpers;
using FluentValidation;

namespace Fintech.Application.Validators.Card;

public class BlockCardRequestValidator : AbstractValidator<BlockCardRequest>
{
    public BlockCardRequestValidator()
    {
        RuleFor(x => x.Pan).NotEmpty().WithMessage("Pan can not be empty")
            .Must(PanValidator.ValidatePan).WithMessage(ErrorMessages.NotValidPanFormat.GetMessage());

        RuleFor(x => x.Userid).NotEmpty().WithMessage("UserId can not be empty");
    }
}