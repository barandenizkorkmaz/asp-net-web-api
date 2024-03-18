using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;

internal class RestaurantsDbContext : DbContext
{
    internal DbSet<Restaurant> Restaurants { get; set;}
    internal DbSet<Dish> Dishes { get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RestaurantsDb;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
            .OwnsOne(r => r.Address); // One-to-one relationship

        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Dishes) // One-to-many relationship (one restaurant to many dishes)
            .WithOne() // Many-to-one relationship from dish side (many dishes to one restaurant)
            .HasForeignKey(d => d.RestaurantId); // One-to-many relationship

    }
}
