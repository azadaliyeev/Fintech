using Fintech.Domain.Builders;
using Fintech.Domain.Entities;

namespace Fintech.Application.Directors;

public class CardDirector(ICardBuilder cardBuilder)
{
    public void Build(List<string>? filters, Card card)
    {
        if (filters != null)
        {
            GetByFilter(filters, card);
            return;
        }

        cardBuilder.SetId(card.Id);
        cardBuilder.SetPan(card.Pan);
        cardBuilder.SetCurrency(card.Currency);
        cardBuilder.SetCardName(card.CardName);
        cardBuilder.SetCardType(card.CardType);
        cardBuilder.SetCardBrand(card.CardBrand);
        cardBuilder.SetBalance(card.Balance);
    }

    private void GetAll1()
    {
        
    }
    

    private void GetByFilter(List<string> filters, Card card)
    {
        foreach (var key in filters)
        {
            switch (key.ToLowerInvariant())
            {
                case "id":
                    cardBuilder.SetId(card.Id);
                    break;
                case "pan":
                    cardBuilder.SetPan(card.Pan);
                    break;
                case "currency":
                    cardBuilder.SetCurrency(card.Currency);
                    break;
                case "cardname":
                    cardBuilder.SetCardName(card.CardName);
                    break;
                case "cardtype":
                    cardBuilder.SetCardType(card.CardType);
                    break;
                case "cardbrand":
                    cardBuilder.SetCardBrand(card.CardBrand);
                    break;
                case "balance":
                    cardBuilder.SetBalance(card.Balance);
                    break;
            }
        }
    }

    public Dictionary<string, object> GetResult() => cardBuilder.GetResult();
}