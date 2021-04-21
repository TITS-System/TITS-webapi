using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Db;
using Models.Enums;

namespace Infrastructure.Implementations
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        public OrderRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<Order> GetById(long id)
        {
            return await Context.Orders.FindAsync(id);
        }

        public async Task<ICollection<Order>> GetUnserved(long restaurantId)
        {
            return await Context.Orders
                .Where(o =>
                    // если доставок в принципе нет
                    (!o.Deliveries.Any()) ||
                    // или если нет доставки, которая в процессе или завершена
                    !(o.Deliveries.Any(d => d.Status == DeliveryStatus.InProgress || d.Status == DeliveryStatus.Finished))
                    && o.RestaurantId == restaurantId
                )
                .ToListAsync();
        }

        public async Task<ICollection<Order>> GetAllByRestaurant(long restaurantId)
        {
            return await Context.Orders
                .Where(o => o.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task Update(Order order)
        {
            Context.Orders.Update(order);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(Order order)
        {
            Context.Orders.Remove(order);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(Order order)
        {
            Context.Orders.Add(order);
            await Context.SaveChangesAsync();
        }
    }
}