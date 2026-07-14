using BookStore.Application;
using BookStore.Infrastructure;
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace BookStore.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddOpenApi();

        services
            .AddMapsterConfig();

        // Layer Dependency Injection
        services
            .AddApplicationDependencyInjection()
            .AddInfrastructureDependencyInjection(configuration);

        return services;
    }
    private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
        config.Scan(
            typeof(BookStore.Application.DependencyInjection).Assembly,
            typeof(BookStore.Infrastructure.DependencyInjection).Assembly
        );

        return services.AddSingleton<IMapper>(new Mapper(config));
    }
}

