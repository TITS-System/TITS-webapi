using System.Threading.Tasks;
using Models.Db;

namespace Infrastructure.Abstractions
{
    public interface IOrderRepository
    {
        Task<Order> GetById(long id);

        Task Update(Order order);

        Task Remove(Order order);

        Task Insert(Order order);
    }
}