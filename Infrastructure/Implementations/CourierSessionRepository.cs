using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Db;

namespace Infrastructure.Implementations
{
    public class CourierSessionRepository : RepositoryBase, ICourierSessionRepository
    {
        public CourierSessionRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<CourierSession> GetById(long id)
        {
            return await Context.WorkerSessions.FindAsync(id);
        }

        public async Task Update(CourierSession courierSession)
        {
            Context.WorkerSessions.Update(courierSession);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(CourierSession courierSession)
        {
            Context.WorkerSessions.Remove(courierSession);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(CourierSession courierSession)
        {
            Context.WorkerSessions.Add(courierSession);
            await Context.SaveChangesAsync();
        }

        public async Task<CourierSession> GetLastByWorkerId(long workerId)
        {
            return await Context.WorkerSessions.Where(ws => ws.CourierAccountId == workerId).OrderBy(ws => ws.Id).LastOrDefaultAsync();
        }
    }
}