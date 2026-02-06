using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;

namespace MoneyBee.Transfer.Application;
public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}