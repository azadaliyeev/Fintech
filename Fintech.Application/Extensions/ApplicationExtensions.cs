using System.Reflection;
using Fintech.Application.BackgroundServices;
using Fintech.Application.Builders;
using Fintech.Application.Directors;
using Fintech.Application.ExceptionHandler;
using Fintech.Application.Integrations;
using Fintech.Application.Patterns.BuilderPattern.QueryBuilder;
using Fintech.Application.Patterns.BuilderPattern.QueryDirector;
using Fintech.Application.Services.AccountService;
using Fintech.Application.Services.Authentication;
using Fintech.Application.Services.CardService;
using Fintech.Application.Services.CurrencyService;
using Fintech.Application.Services.EmailService;
using Fintech.Application.Services.UserService;
using Fintech.Application.Services.VerificationTokenService;
using Fintech.Domain.Builders;
using Fintech.Domain.Patterns.BuilderPatterns.QueryBuilder;
using Fintech.Domain.Services.Accounts;
using Fintech.Domain.Services.Authentication;
using Fintech.Domain.Services.Cards;
using Fintech.Domain.Services.Currency;
using Fintech.Domain.Services.Email;
using Fintech.Domain.Services.Users;
using Fintech.Domain.Services.VerificationToken;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Fintech.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        
        
        services.AddHostedService<CurrencyBackgroundService>();
        services.AddHostedService<CheckUserVerifiedBackgroundService>();


        services.Configure<ApiBehaviorOptions>(opt => { opt.SuppressModelStateInvalidFilter = true; });


        services.AddScoped<CurrencyHttpClient>();
        services.AddHttpClient();

        services.AddScoped<UserQueryDirector>();
        
        services.AddScoped<IUserQueryBuilder, UserQueryBuilder>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IVerificationTokenService, VerificationTokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAuthentication, Authentication>();
        services.AddScoped<ICardService, CardService>();
        services.AddScoped<IAccountBuilder, AccountBuilder>();
        services.AddScoped<IUserBuilder, UserBuilder>();
        services.AddScoped<ICardBuilder, CardBuilder>();
        services.AddScoped<UserDirector>();
        services.AddScoped<AccountDirector>();
        services.AddScoped<CardDirector>();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddExceptionHandler<GlobalExceptionHandler>();
        return services;
    }
}