using System.Threading.Tasks;
using Models.Db.Sessions;

namespace Infrastructure.Abstractions
{
    public interface ITokenSessionRepository
    {
        Task<TokenSession> GetById(long id);
        
        Task<TokenSession> GetByToken(string token);

        Task Update(TokenSession tokenSession);

        Task Remove(TokenSession tokenSession);

        Task Insert(TokenSession tokenSession);
    }
}