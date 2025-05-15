using Fintech.Domain.Builders;
using Fintech.Domain.Entities;

namespace Fintech.Application.Directors;

public class AccountDirector(IAccountBuilder accountBuilder)
{
    public void Build(List<string>? filters, Account acc)
    {
        if (filters != null)
        {
            GetByFilter(filters, acc);
            return;
        }

        accountBuilder.SetId(acc.Id);
        accountBuilder.SetIban(acc.Iban);
        accountBuilder.SetBalance(acc.Balance);
        accountBuilder.SetAccountType(acc.AccountType);
        accountBuilder.SetCurrency(acc.Currency);
        accountBuilder.SetStatus(acc.Status);
    }


    private void GetByFilter(List<string> filters, Account account)
    {
        foreach (var key in filters)
        {
            switch (key.ToLowerInvariant())
            {
                case "id":
                    accountBuilder.SetId(account.Id);
                    break;
                case "iban":
                    accountBuilder.SetIban(account.Iban);
                    break;
                case "balance":
                    accountBuilder.SetBalance(account.Balance);
                    break;
                case "accounttype":
                    accountBuilder.SetAccountType(account.AccountType);
                    break;
                case "currency":
                    accountBuilder.SetCurrency(account.Currency);
                    break;
                case "status":
                    accountBuilder.SetStatus(account.Status);
                    break;
            }
        }
    }

    public Dictionary<string, object> GetResult() => accountBuilder.GetResult();
}