using Fintech.Shared.Enums;

namespace Fintech.Shared.Extension;

public static class CurrencyExtension
{
    public static string Get(this Currencies currencies)
    {
        return currencies switch
        {
            Currencies.Azn => "azn",
            Currencies.Usd => "usd",
            Currencies.Eur => "eur",
            Currencies.Rub => "rub",
            Currencies.Try => "try",
            _ => throw new ArgumentOutOfRangeException(nameof(currencies), currencies, null)
        };
    }
}