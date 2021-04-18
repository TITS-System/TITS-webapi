using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Db.Account;

namespace Infrastructure.Implementations
{
    public class WorkerAccountRepository : RepositoryBase, IWorkerAccountRepository
    {
        public WorkerAccountRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<WorkerAccount> GetById(long id)
        {
            return await Context.WorkerAccounts.FindAsync(id);
        }

        public async Task<WorkerAccount> GetByLogin(string login)
        {
            return await Context.WorkerAccounts.FirstOrDefaultAsync(wa=>wa.Login == login);
        }

        public async Task Update(WorkerAccount workerAccount)
        {
            Context.WorkerAccounts.Update(workerAccount);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(WorkerAccount workerAccount)
        {
            Context.WorkerAccounts.Remove(workerAccount);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(WorkerAccount workerAccount)
        {
            Context.WorkerAccounts.Add(workerAccount);
            await Context.SaveChangesAsync();
        }
    }
}