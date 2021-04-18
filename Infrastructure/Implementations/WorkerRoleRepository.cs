using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Db.Account;

namespace Infrastructure.Implementations
{
    public class WorkerRoleRepository : RepositoryBase, IWorkerRoleRepository
    {
        public WorkerRoleRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<WorkerRole> GetById(long id)
        {
            return await Context.WorkerRoles.FindAsync(id);
        }

        public async Task Update(WorkerRole workerRole)
        {
            Context.WorkerRoles.Update(workerRole);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(WorkerRole workerRole)
        {
            Context.WorkerRoles.Update(workerRole);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(WorkerRole workerRole)
        {
            Context.WorkerRoles.Update(workerRole);
            await Context.SaveChangesAsync();
        }

        public async Task<WorkerRole> GetByTitleEn(string titleEn)
        {
            return await Context.WorkerRoles.FirstOrDefaultAsync(wr => wr.TitleEn == titleEn);
        }
    }
}