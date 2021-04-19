using System.Threading.Tasks;
using Models.Db;

namespace Infrastructure.Abstractions
{
    public interface IWorkerSessionRepository
    {
        Task<WorkerSession> GetById(long id);

        Task Update(WorkerSession workerSession);

        Task Remove(WorkerSession workerSession);

        Task Insert(WorkerSession workerSession);

        Task<WorkerSession> GetLastByWorkerId(long workerId);
    }
}