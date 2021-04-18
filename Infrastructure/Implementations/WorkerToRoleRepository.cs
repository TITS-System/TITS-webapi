using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Db.Account;

namespace Infrastructure.Implementations
{
    public class WorkerToRoleRepository : RepositoryBase, IWorkerToRoleRepository
    {
        public WorkerToRoleRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<WorkerRole>> GetWorkerRoles(long workerId)
        {
            return await Context.WorkerAccountToRoles
                .Where(watr => watr.WorkerAccountId == workerId)
                .Select(watr => watr.WorkerRole)
                .ToListAsync();
        }

        public async Task<IEnumerable<WorkerAccount>> GetRoleWorkers(long roleId)
        {
            return await Context.WorkerAccountToRoles
                .Where(watr => watr.WorkerRoleId == roleId)
                .Select(watr => watr.WorkerAccount)
                .ToListAsync();
        }

        public async Task<WorkerAccountToRole> GetPair(long workerId, long roleId)
        {
            return await Context.WorkerAccountToRoles.FirstOrDefaultAsync(watr => watr.WorkerAccountId == workerId && watr.WorkerRoleId == roleId);
        }

        public async Task Insert(WorkerAccountToRole workerAccountToRole)
        {
            Context.WorkerAccountToRoles.Add(workerAccountToRole);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(WorkerAccountToRole workerAccountToRole)
        {
            Context.WorkerAccountToRoles.Remove(workerAccountToRole);
            await Context.SaveChangesAsync();
        }
    }
}