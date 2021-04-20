using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Db;

namespace Infrastructure.Implementations
{
    public class CourierMessageRepository : RepositoryBase, ICourierMessageRepository
    {
        public CourierMessageRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<CourierMessage> GetById(long id)
        {
            return await Context.CourierMessages.FindAsync(id);
        }

        public async Task<ICollection<CourierMessage>> GetForCourier(long courierId, int limit, int offset)
        {
            return await Context.CourierMessages
                .Where(cm => cm.CourierAccountId == courierId)
                .Skip(offset)
                .Take(25)
                .ToListAsync();
        }

        public async Task Update(CourierMessage courierMessage)
        {
            Context.CourierMessages.Update(courierMessage);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(CourierMessage courierMessage)
        {
            Context.CourierMessages.Remove(courierMessage);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(CourierMessage courierMessage)
        {
            Context.CourierMessages.Add(courierMessage);
            await Context.SaveChangesAsync();
        }
    }
}