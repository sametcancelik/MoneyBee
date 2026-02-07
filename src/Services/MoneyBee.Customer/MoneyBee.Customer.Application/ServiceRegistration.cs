using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MoneyBee.Customer.Application.Common.Behaviors;

namespace MoneyBee.Customer.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);
    }
}