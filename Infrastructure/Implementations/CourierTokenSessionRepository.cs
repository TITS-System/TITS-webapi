using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Db.TokenSessions;

namespace Infrastructure.Implementations
{
    public class CourierTokenSessionRepository : RepositoryBase, ICourierTokenSessionRepository
    {
        public CourierTokenSessionRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<CourierTokenSession> GetById(long id)
        {
            return await Context.CourierTokenSessions.FindAsync(id);
        }

        public async Task<CourierTokenSession> GetByToken(string token)
        {
            return await Context.CourierTokenSessions.FirstOrDefaultAsync(ts => ts.Token == token);
        }

        public async Task Update(CourierTokenSession courierTokenSession)
        {
            Context.CourierTokenSessions.Update(courierTokenSession);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(CourierTokenSession courierTokenSession)
        {
            Context.CourierTokenSessions.Remove(courierTokenSession);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(CourierTokenSession courierTokenSession)
        {
            Context.CourierTokenSessions.Add(courierTokenSession);
            await Context.SaveChangesAsync();
        }
    }
}