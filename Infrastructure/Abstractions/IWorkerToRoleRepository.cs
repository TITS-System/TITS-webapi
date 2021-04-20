using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Db.Account;

namespace Infrastructure.Abstractions
{
    public interface IWorkerToRoleRepository
    {
        Task<IEnumerable<WorkerRole>> GetWorkerRoles(long workerId);
        
        Task<IEnumerable<CourierAccount>> GetRoleWorkers(long roleId);

        Task<AccountToRole> GetPair(long workerId, long roleId);

        Task Insert(AccountToRole accountToRole);
        
        Task Remove(AccountToRole accountToRole);
    }
}