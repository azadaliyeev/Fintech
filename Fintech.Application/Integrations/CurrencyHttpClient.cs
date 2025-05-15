using System.Net.Http.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using Fintech.Domain.Models.Currency;

namespace Fintech.Application.Integrations;

public class CurrencyHttpClient
{
    private readonly HttpClient _httpClient;

    public CurrencyHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CurrencyData>> GetCurrency()
    {
        var response = await _httpClient.GetAsync("https://www.cbar.az/currencies/02.05.2025.xml");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error fetching currency data");
        }

        var content = await response.Content.ReadAsStringAsync();
        XDocument doc = XDocument.Parse(content);

        var currencies = new[] { "USD", "EUR", "AZN", "RUB", "TRY" };


        var valutes = doc.Descendants("Valute")
            .Select(x => new CurrencyData()
            {
                Code = (string)x.Attribute("Code"),
                Value = (decimal)x.Element("Value"),
            }).Where(x => currencies.Contains(x.Code)).ToList();


        return valutes;
    }
}