namespace Fintech.Shared.Helpers;

public class PanGeneratorByBrand
{
    private static Random _random = new Random();

    public static string GeneratePan(string brand)
    {
        return brand switch
        {
            "Visa" => GenerateVisaPan(),
            "Master" => GenerateMasterCardPan(),
            _ => throw new ArgumentException("Invalid card brand")
        };
    }

    private static string GenerateVisaPan()
    {
        string bin = "400000"; 
        string pan = bin + _random.Next(100000000, 999999999).ToString();

        if (pan.Length > 16)
            pan = pan.Substring(0, 16);

        return pan + CalculateLuhnCheckDigit(pan); 
    }

    private static string GenerateMasterCardPan()
    {
        string bin = "510000";
        string pan = bin + _random.Next(100000000, 999999999).ToString();

        if (pan.Length > 16)
            pan = pan.Substring(0, 16);

        return pan + CalculateLuhnCheckDigit(pan); 
    }

    public static int CalculateLuhnCheckDigit(string pan)
    {
        int sum = 0;
        bool doubleDigit = true;

        for (int i = pan.Length - 1; i >= 0; i--)
        {
            int digit = int.Parse(pan[i].ToString());
            if (doubleDigit)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }

            sum += digit;
            doubleDigit = !doubleDigit;
        }

        int mod = sum % 10;
        return (10 - mod) % 10; 
    }
}