using BookStore.Infrastructure;

namespace BookStore.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddOpenApi();

        // Layer Dependency Injection
        services
            .AddInfrastructureDependencyInjection(configuration);

        return services;
    }
}

