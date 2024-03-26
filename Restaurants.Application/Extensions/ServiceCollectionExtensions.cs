using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.User;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        //services.AddScoped<IRestaurantService, RestaurantService>(); // No longer needed since we switch to CQRS pattern
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));

        services.AddAutoMapper(applicationAssembly); // Inject AutoMapper dependency 

        services.AddValidatorsFromAssembly(applicationAssembly) // Inject Fluent Validation dependency
            .AddFluentValidationAutoValidation(); // Invoke the configuration

        services.AddScoped<IUserContext, UserContext>();

        services.AddHttpContextAccessor(); // Inject HTTPContext to access it properly in UserContext class.

    }
}
