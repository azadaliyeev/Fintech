namespace Fintech.Domain.Builders;

public interface IAccountBuilder
{

    void  SetId(string id);
    void SetIban(string iban);
    void SetBalance(decimal balance);
    void SetAccountType(string accountType);
    void SetCurrency(string currency);
    void SetStatus(string status);
    
    Dictionary<string,object> GetResult();
}

