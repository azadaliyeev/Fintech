using System.Data;
using System.Reflection;
using Fintech.Domain.Repositories;
using Fintech.Domain.UnitOfWork;
using Fintech.Repository.DbCotext;
using Fintech.Repository.Repositories;
using Fintech.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Fintech.Repository.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStringOption>(configuration.GetSection(ConnectionStringOption.Key));

        services.AddDbContext<FintechDbContext>((options) =>
        {
            var connectionString = configuration.GetSection(ConnectionStringOption.Key)
                .Get<ConnectionStringOption>();

            options.UseNpgsql(connectionString?.DefaultConnection,
                npgsqlOptionsAction =>
                {
                    npgsqlOptionsAction.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
        });


        services.AddScoped<IDbConnection>((opt) =>
        {
            var connectionStrings = opt.GetRequiredService<IOptions<ConnectionStringOption>>().Value;

            var connection = new Npgsql.NpgsqlConnection(connectionStrings.DefaultConnection);

            return connection;
        });

        services.AddScoped<IDbTransaction>(provider =>
        {
            var connection = provider.GetRequiredService<IDbConnection>();
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection.BeginTransaction();
        });


        services.AddScoped(opt =>
            new Lazy<IDbConnection>(opt.GetRequiredService<IDbConnection>));

        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IVerificationTokenRepository, VerificationTokenRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        return services;
    }
}