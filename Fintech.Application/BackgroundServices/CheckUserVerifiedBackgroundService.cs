using Fintech.Domain.Services.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fintech.Application.BackgroundServices;

public class CheckUserVerifiedBackgroundService(
    IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scope = serviceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        await userService.CheckUserVerificationAsync();

        await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
    }
}