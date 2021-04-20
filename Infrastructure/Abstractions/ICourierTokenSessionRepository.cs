using System.Threading.Tasks;
using Models.Db.TokenSessions;

namespace Infrastructure.Abstractions
{
    public interface ICourierTokenSessionRepository
    {
        Task<CourierTokenSession> GetById(long id);
        
        Task<CourierTokenSession> GetByToken(string token);

        Task Update(CourierTokenSession courierTokenSession);

        Task Remove(CourierTokenSession courierTokenSession);

        Task Insert(CourierTokenSession courierTokenSession);
    }
}