using Fintech.Domain.Models.Account.Block;
using Fintech.Domain.Services.Accounts;
using Fintech.Shared.Enums;
using Fintech.Shared.Extension;
using Fintech.Shared.Helpers;
using FluentValidation;

namespace Fintech.Application.Validators.Account;

public class BlockAccountRequestValidator : AbstractValidator<BlockAccountRequest>
{
    public BlockAccountRequestValidator(IAccountService accountService)
    {
        RuleFor(x => x.Iban).NotEmpty().WithMessage(ErrorMessages.Null.GetMessage())
            .Must(accountService.IsValidIban).WithMessage(ErrorMessages.NotValidIban.GetMessage());

        RuleFor(x => x.UserId).NotEmpty().WithMessage(ErrorMessages.Null.GetMessage());
    }
}