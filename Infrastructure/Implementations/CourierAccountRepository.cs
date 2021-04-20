using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Db.Account;

namespace Infrastructure.Implementations
{
    public class CourierAccountRepository : RepositoryBase, ICourierAccountRepository
    {
        public CourierAccountRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<CourierAccount> GetById(long id)
        {
            return await Context.CourierAccounts.FindAsync(id);
        }

        public async Task<CourierAccount> GetByLogin(string login)
        {
            return await Context.CourierAccounts.FirstOrDefaultAsync(wa => wa.Login == login);
        }

        public async Task<ICollection<CourierAccount>> GetByRestaurant(long restaurantId)
        {
            return await Context.CourierAccounts.Where(wa => wa.AssignedToRestaurantId == restaurantId).ToListAsync();
        }

        public async Task Update(CourierAccount courierAccount)
        {
            Context.CourierAccounts.Update(courierAccount);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(CourierAccount courierAccount)
        {
            Context.CourierAccounts.Remove(courierAccount);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(CourierAccount courierAccount)
        {
            Context.CourierAccounts.Add(courierAccount);
            await Context.SaveChangesAsync();
        }
    }
}