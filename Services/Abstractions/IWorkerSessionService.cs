using System.Threading.Tasks;
using Models.DTOs;
using Models.DTOs.Misc;

namespace Services.Abstractions
{
    public interface IWorkerSessionService
    {
        Task<BeginWorkSessionResultDto> Begin(long workerId);
        
        Task Close(long workerId);
        
        Task<TimeDto> GetCurrentWorkerSessionDuration(long workerId);
    }
}