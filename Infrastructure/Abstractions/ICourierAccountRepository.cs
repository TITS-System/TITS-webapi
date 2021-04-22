using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Db.Account;

namespace Infrastructure.Abstractions
{
    public interface ICourierAccountRepository
    {
        Task<CourierAccount> GetById(long id);

        Task<CourierAccount> GetByLogin(string login);
        
        Task<ICollection<CourierAccount>> GetByRestaurant(long restaurantId);
        
        Task<ICollection<CourierAccount>> GetByRestaurantAndOnWork(long restaurantId);

        Task Update(CourierAccount courierAccount);

        Task Remove(CourierAccount courierAccount);

        Task Insert(CourierAccount courierAccount);
    }
}