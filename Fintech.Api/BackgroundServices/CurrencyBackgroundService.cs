using Fintech.Domain.Services.Currency;

namespace Fintech.Api.BackgroundServices;

public class CurrencyBackgroundService(
    IServiceProvider serviceProvider,
    ILogger<CurrencyBackgroundService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            var currencyService = scope.ServiceProvider.GetRequiredService<ICurrencyService>();

            await currencyService.UpdateCurrencyAsync();
            

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}