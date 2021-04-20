
using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Db.TokenSessions;

namespace Infrastructure.Implementations
{
    public class ManagerTokenSessionRepository : RepositoryBase, IManagerTokenSessionRepository
    {
        public ManagerTokenSessionRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<ManagerTokenSession> GetById(long id)
        {
            return await Context.ManagerTokenSessions.FindAsync(id);
        }

        public async Task<ManagerTokenSession> GetByToken(string token)
        {
            return await Context.ManagerTokenSessions.FirstOrDefaultAsync(ts => ts.Token == token);
        }

        public async Task Update(ManagerTokenSession managerTokenSession)
        {
            Context.ManagerTokenSessions.Update(managerTokenSession);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(ManagerTokenSession managerTokenSession)
        {
            Context.ManagerTokenSessions.Remove(managerTokenSession);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(ManagerTokenSession managerTokenSession)
        {
            Context.ManagerTokenSessions.Add(managerTokenSession);
            await Context.SaveChangesAsync();
        }
    }
}