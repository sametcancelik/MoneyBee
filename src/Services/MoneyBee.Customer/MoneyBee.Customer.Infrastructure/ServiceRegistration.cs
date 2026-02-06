using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyBee.Customer.Application.Interfaces;
using MoneyBee.Customer.Application.Interfaces.Persistance;
using MoneyBee.Customer.Infrastructure.Persistence;
using MoneyBee.Customer.Infrastructure.ExternalServices;
using Polly;
using Polly.Extensions.Http;

namespace MoneyBee.Customer.Infrastructure;
public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CustomerDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ICustomerDbContext>(provider => provider.GetRequiredService<CustomerDbContext>());

        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        var circuitBreakerPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

        services.AddHttpClient<IKycService, KycService>(client =>
        {
            client.BaseAddress = new Uri(configuration["ExternalServices:KycUrl"]);
            client.Timeout = TimeSpan.FromSeconds(10);
        })
        .AddPolicyHandler(retryPolicy)
        .AddPolicyHandler(circuitBreakerPolicy);
    }
}