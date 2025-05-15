using Fintech.Shared.Enums;

namespace Fintech.Shared.Extension;

public static class AccountTypeExtension
{
    public static string Get(this AccountType accountType)
    {
        return accountType switch
        {
            AccountType.Master => "Master",
            AccountType.Default => "Default",
            _ => "Unknown"
        };
    }
}