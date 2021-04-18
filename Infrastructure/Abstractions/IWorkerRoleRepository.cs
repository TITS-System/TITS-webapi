using System.Threading.Tasks;
using Models.Db.Account;

namespace Infrastructure.Abstractions
{
    public interface IWorkerRoleRepository
    {
        Task<WorkerRole> GetById(long id);
        
        Task Update(WorkerRole workerRole);

        Task Remove(WorkerRole workerRole);

        Task Insert(WorkerRole workerRole);
        
        Task<WorkerRole> GetByTitleEn(string titleEn);
    }
}