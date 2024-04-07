using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants
            .Include(r => r.Dishes)
            .ToListAsync();
        return restaurants;
    }

    public async Task<(IEnumerable<Restaurant>,int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber)
    {
        var searchPhraseLower = searchPhrase?.ToLower(); // if searchPhrase is null, using ? operator will make sure that the program doesn't throw an exception

        var baseQuery = dbContext
            .Restaurants
            .Where(r => searchPhrase == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                || r.Description.ToLower().Contains(searchPhraseLower)))
            .Include(r => r.Dishes);

        var totalCount = await baseQuery.CountAsync();

        var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (restaurants, totalCount);
    }


    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(r => r.Id == id);
        return restaurant;
    }

    public async Task<int> Create(Restaurant entity)
    {
        dbContext.Restaurants.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Delete(Restaurant entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
