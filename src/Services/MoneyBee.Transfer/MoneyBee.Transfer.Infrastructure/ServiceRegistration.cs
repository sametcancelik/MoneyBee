using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyBee.Transfer.Application.Interfaces;
using MoneyBee.Transfer.Application.Interfaces.Persistance;
using MoneyBee.Transfer.Infrastructure.ExternalServices;
using MoneyBee.Transfer.Infrastructure.Persistence;
using Polly;
using Polly.Extensions.Http;

namespace MoneyBee.Transfer.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TransferDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<ITransferDbContext>(provider => provider.GetRequiredService<TransferDbContext>());

        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2.0, retryAttempt)));

        var circuitBreakerPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30.0));

        services.AddHttpClient<IFraudService, FraudService>(client =>
        {
            client.BaseAddress = new Uri(configuration["ExternalServices:FraudUrl"]!);
            client.Timeout = TimeSpan.FromSeconds(30.0);
        }).AddPolicyHandler(retryPolicy).AddPolicyHandler(circuitBreakerPolicy);

        services.AddHttpClient<IExchangeRateService, ExchangeRateService>(client =>
        {
            client.BaseAddress = new Uri(configuration["ExternalServices:ExchangeRateUrl"]!);
            client.Timeout = TimeSpan.FromSeconds(30.0);
        }).AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ICustomerService, CustomerService>(client =>
        {
            client.BaseAddress = new Uri(configuration["ExternalServices:CustomerUrl"]!);
            client.Timeout = TimeSpan.FromSeconds(30.0);
        }).AddPolicyHandler(retryPolicy);
    }
}