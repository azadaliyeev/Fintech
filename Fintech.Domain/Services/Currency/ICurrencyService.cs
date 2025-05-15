using Fintech.Domain.Models.Currency;
using Fintech.Domain.Models.Currency.Convert;
using Fintech.Shared.ServiceResults;

namespace Fintech.Domain.Services.Currency;

public interface ICurrencyService
{
    Task UpdateCurrencyAsync();
    Task<ServiceResult<List<CurrencyDto>>> GetAllCurrenciesAsync();
    Task<ServiceResult<ConvertCurrencyResponse>> ConvertCurrencyAsync(ConvertCurrencyRequest request);
}