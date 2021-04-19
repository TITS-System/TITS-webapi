using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Db.Account;

namespace Infrastructure.Abstractions
{
    public interface IWorkerAccountRepository
    {
        Task<WorkerAccount> GetById(long id);

        Task<WorkerAccount> GetByLogin(string login);
        
        Task<ICollection<WorkerAccount>> GetByRestaurant(long restaurantId);

        Task Update(WorkerAccount workerAccount);

        Task Remove(WorkerAccount workerAccount);

        Task Insert(WorkerAccount workerAccount);
    }
}