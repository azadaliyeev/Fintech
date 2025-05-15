using System.Text;

namespace Fintech.Shared.Helpers;

public static class IbanValidator
{
    public static bool IsValid(string iban)
    {
        if (string.IsNullOrEmpty(iban))
            return false;


        iban = iban.Replace(" ", "").ToUpper();

        if (iban.Length != 28 || !iban.StartsWith("AZ"))
            return false;

        string rearrangedIban = iban.Substring(4) + iban.Substring(0, 4);

        StringBuilder numericIban = new StringBuilder();

        foreach (var c in rearrangedIban)
        {
            if (char.IsLetter(c))
                numericIban.Append((c - 'A' + 10).ToString());
            else if (char.IsDigit(c))
                numericIban.Append(c);
            else
                return false;
        }


        // MOD-97 yoxlaması
        string number = numericIban.ToString();
        int remainder = 0;

        foreach (char digit in number)
        {
            int digitValue = digit - '0';
            remainder = (remainder * 10 + digitValue) % 97;
        }

        return remainder == 1;
    }
}