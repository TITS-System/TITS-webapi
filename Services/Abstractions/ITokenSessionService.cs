using System.Threading.Tasks;
using Models.Db.Sessions;
using Models.Dtos;
using Models.DTOs.Requests;

namespace Services.Abstractions
{
    public interface ITokenSessionService
    {
        Task<LoginResultDto> Login(LoginDto loginDto);

        Task<TokenSession> GetByToken(string token);

        Task Logout(TokenSession tokenSession);
    }
}