using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;

internal class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : IdentityDbContext<User>(options)
{
    internal DbSet<Restaurant> Restaurants { get; set;}
    internal DbSet<Dish> Dishes { get; set;}

    /*+
     * Since .NET8, we can define primary constructor in class definition. See the class definition.
    
    public RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : base(options)
    {

    }
    */

    /*
     * Change to ConnectionString from appsettings.json. See the ctor
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RestaurantsDb;Trusted_Connection=True;");
    }
    */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
            .OwnsOne(r => r.Address); // One-to-one relationship

        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Dishes) // One-to-many relationship (one restaurant to many dishes)
            .WithOne() // Many-to-one relationship from dish side (many dishes to one restaurant)
            .HasForeignKey(d => d.RestaurantId); // One-to-many relationship

        // A restaurant can have only one owner, whereas an owner can own multiple restaurants.
        modelBuilder.Entity<User>()
            .HasMany(u => u.OwnedRestaurants)
            .WithOne(r => r.Owner)
            .HasForeignKey(r => r.OwnerId);
    }
}
