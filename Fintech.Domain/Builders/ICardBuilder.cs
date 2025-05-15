namespace Fintech.Domain.Builders;

public interface ICardBuilder
{
    void SetId(string id);
    void SetPan(string pan);
    void SetCurrency(string currency);
    void SetCardName(string cardName);
    void SetCardType(string cardType);
    void SetCardBrand(string cardBrand);
    void SetBalance(decimal balance);

    Dictionary<string,object> GetResult();
}

