
using System.Threading.Tasks;
using Models.Db.TokenSessions;

namespace Infrastructure.Abstractions
{
    public interface IManagerTokenSessionRepository
    {
        Task<ManagerTokenSession> GetById(long id);
        
        Task<ManagerTokenSession> GetByToken(string token);

        Task Update(ManagerTokenSession managerTokenSession);

        Task Remove(ManagerTokenSession managerTokenSession);

        Task Insert(ManagerTokenSession managerTokenSession);
    }
}