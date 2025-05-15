namespace Fintech.Shared.Helpers;

public static class GenerateCvv
{
    public static string Generate()
    {
        Random random = new Random();
        var result = random.Next(100, 999);
        return result.ToString();
    }
}