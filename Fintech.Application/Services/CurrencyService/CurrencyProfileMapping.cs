using AutoMapper;
using Fintech.Domain.Entities;
using Fintech.Domain.Models.Currency;

namespace Fintech.Application.Services.CurrencyService;

public class CurrencyProfileMapping : Profile
{
    public CurrencyProfileMapping()
    {
        CreateMap<Currency, CurrencyDto>()
            .ConstructUsing(x => new CurrencyDto(x.CurrencyCode, x.ExchangeRate, x.BuyRate, x.SellRate));
    }
}