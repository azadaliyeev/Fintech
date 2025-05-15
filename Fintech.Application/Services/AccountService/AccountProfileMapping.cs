using AutoMapper;
using Fintech.Domain.Entities;
using Fintech.Domain.Models.Account;
using Fintech.Domain.Models.Account.Dto;
using Fintech.Domain.Models.Card;

namespace Fintech.Application.Services.AccountService;

public class AccountProfileMapping : Profile
{
    public AccountProfileMapping()
    {
        CreateMap<Account, AccountDto>()
            .ConstructUsing(x => new AccountDto(x.Id, x.Iban, x.Balance, x.AccountType, x.Currency, x.Status));


        CreateMap<Account, AccountWithCard>()
            .ConstructUsing(x => new AccountWithCard(x.Id, x.Iban, x.Balance, x.AccountType, x.Currency, x.Status,
                x.User.Cards != null
                    ? x.User.Cards.Select(x =>
                            new CardDto(x.Id, x.Pan, x.Cvv, x.Currency, x.CardName, x.CardType, x.CardBrand, x.Balance))
                        .ToList()
                    : new List<CardDto>()
            ));
    }
}