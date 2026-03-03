using System.Reflection;
using DotNetDemo.Application.Abstractions;
using DotNetDemo.Infrastructure.Dispatching;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetDemo.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();

        foreach (var assembly in assemblies)
        {
            RegisterQueryHandlers(services, assembly);
        }

        return services;
    }

    private static void RegisterQueryHandlers(IServiceCollection services, Assembly assembly)
    {
        var handlerTypes = assembly.GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))
                .Select(i => (Implementation: t, Interface: i)));

        foreach (var (implementation, @interface) in handlerTypes)
        {
            services.AddScoped(@interface, implementation);
        }
    }
}
