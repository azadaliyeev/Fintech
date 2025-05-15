using Fintech.Domain.Models.Currency.Convert;
using Fintech.Domain.Services.Currency;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.Api.Controllers;

public class CurrencyController(ICurrencyService currencyService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllCurrencies() =>
        CreateActionResult(await currencyService.GetAllCurrenciesAsync());

    [HttpPost]
    public async Task<IActionResult> ConvertCurrency(ConvertCurrencyRequest request)
        => CreateActionResult(await currencyService.ConvertCurrencyAsync(request));
}