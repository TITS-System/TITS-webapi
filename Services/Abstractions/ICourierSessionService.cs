using System.Threading.Tasks;
using Models.DTOs;
using Models.DTOs.Misc;

namespace Services.Abstractions
{
    public interface ICourierSessionService
    {
        Task<BeginCourierSessionResultDto> Begin(long courierId);
        
        Task Close(long courierId);
        
        Task<TimeDto> GetDuration(long courierId);
    }
}