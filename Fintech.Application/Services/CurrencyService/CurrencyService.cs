using AutoMapper;
using Fintech.Application.Integrations;
using Fintech.Domain.Entities;
using Fintech.Domain.Models.Currency;
using Fintech.Domain.Models.Currency.Convert;
using Fintech.Domain.Services.Currency;
using Fintech.Domain.UnitOfWork;
using Fintech.Shared.Enums;
using Fintech.Shared.Extension;
using Fintech.Shared.ServiceResults;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Application.Services.CurrencyService;

public class CurrencyService(IUnitOfWork unitOfWork, CurrencyHttpClient currencyHttp, IMapper mapper) : ICurrencyService
{
    public async Task UpdateCurrencyAsync()
    {
        var data = await currencyHttp.GetCurrency();

        if (data == null)
        {
            throw new Exception("No currency data found");
        }

        await FillCurrencyDataAsync(data);
        await unitOfWork.CommitAsync();
    }

    public async Task<ServiceResult<List<CurrencyDto>>> GetAllCurrenciesAsync()
    {
        var currencies = await unitOfWork.CurrencyRepository.GetAllAsync();
        var currencyDto = mapper.Map<List<CurrencyDto>>(currencies);
        return ServiceResult<List<CurrencyDto>>.Success(currencyDto);
    }

    public async Task<ServiceResult<ConvertCurrencyResponse>> ConvertCurrencyAsync(ConvertCurrencyRequest request)
    {
        return request switch
        {
            { FromCurrencies: Currencies.Azn } => await ConvertFromAzn(request.Amount, request.ToCurrencies),
            { ToCurrencies: Currencies.Azn } => await ConvertToAzn(request.Amount, request.FromCurrencies),
            _ => await ConvertViaAzn(request.Amount, request.ToCurrencies, request.FromCurrencies),
        };
    }

    private async Task<ServiceResult<ConvertCurrencyResponse>> ConvertFromAzn(decimal amount, Currencies toCurrencies)
    {
        var toCurrency = await unitOfWork.CurrencyRepository.GetByCodeAsync(toCurrencies.Get());
        var result = amount / toCurrency!.ExchangeRate;
        return ServiceResult<ConvertCurrencyResponse>.Success(
            new ConvertCurrencyResponse(result, toCurrency.CurrencyCode));
    }

    private async Task<ServiceResult<ConvertCurrencyResponse>> ConvertViaAzn(decimal amount, Currencies toCurrencies,
        Currencies fromCurrencies)
    {
        var toCurrency = await unitOfWork.CurrencyRepository.GetByCodeAsync(toCurrencies.Get());
        var fromCurrency = await unitOfWork.CurrencyRepository.GetByCodeAsync(fromCurrencies.Get());
        var azn = amount * fromCurrency!.ExchangeRate;
        var result = azn / toCurrency!.ExchangeRate;


        return ServiceResult<ConvertCurrencyResponse>.Success(
            new ConvertCurrencyResponse(result, toCurrency.CurrencyCode));
    }

    private async Task<ServiceResult<ConvertCurrencyResponse>> ConvertToAzn(decimal amount, Currencies fromCurrencies)
    {
        var fromCurrency = await unitOfWork.CurrencyRepository.GetByCodeAsync(fromCurrencies.Get());
        var result = amount * fromCurrency!.ExchangeRate;
        return ServiceResult<ConvertCurrencyResponse>.Success(
            new ConvertCurrencyResponse(result, Currencies.Azn.Get().ToUpperInvariant()));
    }

    private async Task FillCurrencyDataAsync(List<CurrencyData> data)
    {
        foreach (var c in data)
        {
            await ProcessCurrencyAsync(c);
        }
    }

    private async Task ProcessCurrencyAsync(CurrencyData data)
    {
        var result = await unitOfWork.CurrencyRepository.Where(x => x.CurrencyCode == data.Code.ToLowerInvariant())
            .FirstOrDefaultAsync();
        if (result is null)
        {
            var currency = new Fintech.Domain.Entities.Currency
            {
                Id = Guid.NewGuid().ToString(),
                CurrencyCode = data.Code.ToLowerInvariant(),
                ExchangeRate = data.Value,
                BuyRate = CalculateBuyPrice(data.Value),
                SellRate = CalculateSellPrice(data.Value),
            };
            await unitOfWork.CurrencyRepository.AddAsync(currency);
            return;
        }

        result.ExchangeRate = data.Value;
        result.BuyRate = CalculateBuyPrice(data.Value);
        result.SellRate = CalculateSellPrice(data.Value);
        unitOfWork.CurrencyRepository.Update(result);
    }

    private decimal CalculateBuyPrice(decimal officialRate) => officialRate - (officialRate * 0.01M);
    private decimal CalculateSellPrice(decimal officialRate) => officialRate + (officialRate * 0.01M);
}