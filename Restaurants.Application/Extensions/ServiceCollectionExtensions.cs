using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Services;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddScoped<IRestaurantService, RestaurantService>();

        services.AddAutoMapper(applicationAssembly); // Inject AutoMapper dependency 

        services.AddValidatorsFromAssembly(applicationAssembly) // Inject Fluent Validation dependency
            .AddFluentValidationAutoValidation(); // Invoke the configuration

    }
}
