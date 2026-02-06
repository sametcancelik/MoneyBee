using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyBee.Transfer.Application.Interfaces;
using MoneyBee.Transfer.Application.Interfaces.Persistance;
using MoneyBee.Transfer.Infrastructure.Persistence;
using MoneyBee.Transfer.Infrastructure.ExternalServices;
using Polly;
using Polly.Extensions.Http;

namespace MoneyBee.Transfer.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TransferDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ITransferDbContext>(provider =>
            provider.GetRequiredService<TransferDbContext>());

        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        var circuitBreakerPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

        services.AddHttpClient<IFraudService, FraudService>(client =>
        {
            client.BaseAddress = new Uri(configuration["ExternalServices:FraudUrl"] ?? "http://localhost:5001");
            client.Timeout = TimeSpan.FromSeconds(5);
        })
        .AddPolicyHandler(retryPolicy)
        .AddPolicyHandler(circuitBreakerPolicy);

        services.AddHttpClient<IExchangeRateService, ExchangeRateService>(client =>
        {
            client.BaseAddress = new Uri(configuration["ExternalServices:ExchangeRateUrl"] ?? "http://localhost:5002");
            client.Timeout = TimeSpan.FromSeconds(5);
        })
        .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ICustomerService, CustomerService>(client =>
        {
            client.BaseAddress = new Uri(configuration["ExternalServices:CustomerUrl"] ?? "http://localhost:5000");
            client.Timeout = TimeSpan.FromSeconds(5);
        })
        .AddPolicyHandler(retryPolicy);
    }
}