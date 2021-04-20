using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Db;

namespace Infrastructure.Abstractions
{
    public interface ICourierMessageRepository
    {
        Task<CourierMessage> GetById(long id);

        Task<ICollection<CourierMessage>> GetForCourier(long courierId, int limit, int offset);

        Task Update(CourierMessage courierMessage);

        Task Remove(CourierMessage courierMessage);

        Task Insert(CourierMessage courierMessage);
    }
}