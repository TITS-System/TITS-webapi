using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Db;

namespace Infrastructure.Implementations
{
    public class RestaurantRepository : RepositoryBase, IRestaurantRepository
    {
        public RestaurantRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<Restaurant> GetById(long id)
        {
            return await Context.Restaurants.FindAsync(id);
        }

        public async Task Update(Restaurant restaurant)
        {
            Context.Restaurants.Update(restaurant);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(Restaurant restaurant)
        {
            Context.Restaurants.Remove(restaurant);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(Restaurant restaurant)
        {
            Context.Restaurants.Add(restaurant);
            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<Restaurant>> GetAll()
        {
            return await Context.Restaurants.ToListAsync();
        }
    }
}