using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Models.Db;

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