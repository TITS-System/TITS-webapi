using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Db.Sessions;

namespace Infrastructure.Implementations
{
    public class TokenSessionRepository : RepositoryBase, ITokenSessionRepository
    {
        public TokenSessionRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<TokenSession> GetById(long id)
        {
            return await Context.TokenSessions.FindAsync(id);
        }

        public async Task<TokenSession> GetByToken(string token)
        {
            return await Context.TokenSessions.FirstOrDefaultAsync(ts => ts.Token == token);
        }

        public async Task Update(TokenSession tokenSession)
        {
            Context.TokenSessions.Update(tokenSession);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(TokenSession tokenSession)
        {
            Context.TokenSessions.Remove(tokenSession);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(TokenSession tokenSession)
        {
            Context.TokenSessions.Add(tokenSession);
            await Context.SaveChangesAsync();
        }
    }
}