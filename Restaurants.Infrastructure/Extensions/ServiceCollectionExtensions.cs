using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    /*
     * Extension methods allow developers to add new methods to the public contract of an existing CLR type, 
     * without having to sub-class it or recompile the original type. Extension Methods help blend the flexibility 
     * of "duck typing" support popular within dynamic languages today with the performance and compile-time validation 
     * of strongly-typed languages.
     * Extension Methods enable a variety of useful scenarios, and help make possible the really powerful LINQ query framework... .
     * 
     * References:
     *  https://stackoverflow.com/questions/846766/use-of-this-keyword-in-formal-parameters-for-static-methods-in-c-sharp
     *  https://asp-blogs.azurewebsites.net/scottgu/new-orcas-language-feature-extension-methods
     */
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantsDb");
        services.AddDbContext<RestaurantsDbContext>(options => 
            options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()); // In order to log sensitive data such as id of entities.

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();

        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();

        // Add custom policy for authorization
        
        // Create a policy called `HasNationality`. If a user has claim `Nationality`, it means that this user will have this policy.
        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality, "Turkish", "Ukrainian"));


    }
}
