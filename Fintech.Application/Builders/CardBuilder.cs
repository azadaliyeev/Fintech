using Fintech.Domain.Builders;

namespace Fintech.Application.Builders;

public class CardBuilder : ICardBuilder
{
    private readonly Dictionary<string, object> _result = new();

    public void SetId(string id) => _result["Id"] = id;

    public void SetPan(string pan) => _result["Pan"] = pan;

    public void SetCurrency(string currency) => _result["Currency"] = currency;


    public void SetCardName(string cardName) => _result["CardName"] = cardName;


    public void SetCardType(string cardType) => _result["CardType"] = cardType;

    public void SetCardBrand(string cardBrand) => _result["CardBrand"] = cardBrand;


    public void SetBalance(decimal balance) => _result["Balance"] = balance;


    public Dictionary<string, object> GetResult()
    {
        return _result;
    }
}