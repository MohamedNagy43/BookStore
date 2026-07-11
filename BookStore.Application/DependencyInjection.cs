using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencyInjection(this IServiceCollection services)
    {

        return services;
    }

    private static IServiceCollection AddConfig(this IServiceCollection services)
    {
        return services;
    }
}
