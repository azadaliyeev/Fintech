using AutoMapper;
using Fintech.Domain.Entities;
using Fintech.Domain.Models.Card;
using Fintech.Domain.Models.Card.Create;

namespace Fintech.Application.Services.CardService;

public class CardProfileMapping : Profile
{
    public CardProfileMapping()
    {
        CreateMap<Card, CardDto>().ReverseMap();
        CreateMap<CreateCardRequest, Card>().ReverseMap();
    }
}