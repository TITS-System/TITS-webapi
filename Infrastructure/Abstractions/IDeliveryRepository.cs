using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Db;

namespace Infrastructure.Abstractions
{
    public interface IDeliveryRepository
    {
        Task<Delivery> GetById(long id);
        
        Task<ICollection<Delivery>> GetByOrderId(long orderId);

        Task Update(Delivery delivery);

        Task Remove(Delivery delivery);

        Task Insert(Delivery delivery);
    }
}