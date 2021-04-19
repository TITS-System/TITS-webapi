using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Db;

namespace Infrastructure.Implementations
{
    public class WorkerSessionRepository : RepositoryBase, IWorkerSessionRepository
    {
        public WorkerSessionRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<WorkerSession> GetById(long id)
        {
            return await Context.WorkerSessions.FindAsync(id);
        }

        public async Task Update(WorkerSession workerSession)
        {
            Context.WorkerSessions.Update(workerSession);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(WorkerSession workerSession)
        {
            Context.WorkerSessions.Remove(workerSession);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(WorkerSession workerSession)
        {
            Context.WorkerSessions.Add(workerSession);
            await Context.SaveChangesAsync();
        }

        public async Task<WorkerSession> GetLastByWorkerId(long workerId)
        {
            return await Context.WorkerSessions.Where(ws => ws.WorkerAccountId == workerId).OrderBy(ws => ws.Id).LastOrDefaultAsync();
        }
    }
}