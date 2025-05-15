namespace Fintech.Shared.Helpers;

public static class PanValidator
{
    public static bool ValidatePan(string pan)
    {
        if (pan.Length != 16)
            return false;

        string panWithoutCheckDigit = pan.Substring(0, 15); // İlk 15 rəqəm
        char expectedCheckDigit = PanGeneratorByBrand.CalculateLuhnCheckDigit(panWithoutCheckDigit).ToString()[0]; // Gözlənilən son rəqəm
        char actualCheckDigit = pan[15]; // Əslində olan 16-cı rəqəm

        return expectedCheckDigit == actualCheckDigit;
    }
}