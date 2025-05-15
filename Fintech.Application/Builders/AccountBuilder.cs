using Fintech.Domain.Builders;

namespace Fintech.Application.Builders;

public class AccountBuilder : IAccountBuilder
{
    private readonly Dictionary<string, object> _result = new();

    public void SetId(string id) => _result["Id"] = id;

    public void SetIban(string iban) => _result["Iban"] = iban;

    public void SetBalance(decimal balance) => _result["Balance"] = balance;

    public void SetAccountType(string accountType) => _result["AccountType"] = accountType;

    public void SetCurrency(string currency) => _result["Currency"] = currency;

    public void SetStatus(string status) => _result["Status"] = status;

    public Dictionary<string, object> GetResult()
    {
        return _result;
    }
}