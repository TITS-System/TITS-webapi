using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Db.Account;

namespace Infrastructure.Abstractions
{
    public interface IWorkerToRoleRepository
    {
        Task<IEnumerable<WorkerRole>> GetWorkerRoles(long workerId);
        
        Task<IEnumerable<WorkerAccount>> GetRoleWorkers(long roleId);

        Task<WorkerAccountToRole> GetPair(long workerId, long roleId);

        Task Insert(WorkerAccountToRole workerAccountToRole);
        
        Task Remove(WorkerAccountToRole workerAccountToRole);
    }
}