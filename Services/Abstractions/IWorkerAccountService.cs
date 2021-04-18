using System.Threading.Tasks;
using Models.DTOs;
using Models.DTOs.Misc;
using Models.DTOs.WorkerAccountDtos;

namespace Services.Abstractions
{
    public interface IWorkerAccountService
    {
        Task<CreatedDto> CreateAccount(CreateWorkerAccountDto createWorkerAccountDto);

        Task<GetRolesResultDto> GetRoles(long workerId);
    }
}