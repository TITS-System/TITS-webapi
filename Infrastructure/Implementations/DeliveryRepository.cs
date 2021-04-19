using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Db;

namespace Infrastructure.Implementations
{
    public class DeliveryRepository : RepositoryBase, IDeliveryRepository
    {
        public DeliveryRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<Delivery> GetById(long id)
        {
            return await Context.Deliveries.FindAsync(id);
        }

        public async Task<ICollection<Delivery>> GetByOrderId(long orderId)
        {
            return await Context.Deliveries.Where(d => d.OrderId == orderId).ToListAsync();
        }

        public async Task<ICollection<Delivery>> GetByCourierId(long courierId)
        {
            return await Context.Deliveries.Where(d => d.CourierAccountId == courierId).ToListAsync();
        }

        public async Task Update(Delivery delivery)
        {
            Context.Deliveries.Update(delivery);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(Delivery delivery)
        {
            Context.Deliveries.Remove(delivery);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(Delivery delivery)
        {
            Context.Deliveries.Add(delivery);
            await Context.SaveChangesAsync();
        }
    }
}