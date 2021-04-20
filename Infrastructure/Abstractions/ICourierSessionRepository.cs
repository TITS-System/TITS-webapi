using System.Threading.Tasks;
using Models.Db;

namespace Infrastructure.Abstractions
{
    public interface ICourierSessionRepository
    {
        Task<CourierSession> GetById(long id);

        Task Update(CourierSession courierSession);

        Task Remove(CourierSession courierSession);

        Task Insert(CourierSession courierSession);

        Task<CourierSession> GetLastByWorkerId(long workerId);
    }
}