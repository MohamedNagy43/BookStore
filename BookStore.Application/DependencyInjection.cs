using BookStore.Application.Features.Authors.Services;
using BookStore.Application.Features.Books.Services;
using BookStore.Application.Features.Categories.Services;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;

namespace BookStore.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencyInjection(this IServiceCollection services)
    {
        services
            .AddFluentValidationConfig();

        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAutherService, AutherService>();
        services.AddScoped<ICategoryService, CategoryService>();
        return services;
    }

    private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        services
             .AddFluentValidationAutoValidation()
             .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
