using Fintech.Domain.Services.Currency;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fintech.Application.BackgroundServices;

public class CurrencyBackgroundService(
    IServiceProvider serviceProvider)
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