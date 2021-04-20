using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Db;

namespace Infrastructure.Abstractions
{
    public interface IRestaurantRepository
    {
        Task<Restaurant> GetById(long id);

        Task Update(Restaurant restaurant);

        Task Remove(Restaurant restaurant);

        Task Insert(Restaurant restaurant);

        Task<ICollection<Restaurant>> GetAll();
    }
}