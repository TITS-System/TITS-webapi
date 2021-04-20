using System.Threading.Tasks;
using Models.Db.TokenSessions;
using Models.Dtos;
using Models.DTOs.Requests;

namespace Services.Abstractions
{
    public interface ITokenSessionService
    {
        Task<LoginResultDto> LoginCourier(LoginDto loginDto);
        
        Task<LoginResultDto> LoginManager(LoginDto loginDto);

        Task<CourierTokenSession> GetCourierSessionByToken(string token);
        
        Task<ManagerTokenSession> GetManagerSessionByToken(string token);

        Task Logout(CourierTokenSession courierTokenSession);
        
        Task Logout(ManagerTokenSession managerTokenSession);
    }
}