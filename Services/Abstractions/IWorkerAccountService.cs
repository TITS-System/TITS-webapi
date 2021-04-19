using System.Threading.Tasks;
using Models.Dtos;
using Models.DTOs;
using Models.DTOs.Misc;
using Models.DTOs.WorkerAccountDtos;

namespace Services.Abstractions
{
    public interface IWorkerAccountService
    {
        Task<CreatedDto> CreateAccount(CreateWorkerAccountDto createWorkerAccountDto);
        
        Task AssignToRestaurant(AssignToRestaurantDto assignToRestaurantDto);

        Task<GetRolesResultDto> GetRoles(long workerId);

        Task ChangeAccountData(ChangeAccountDataDto changeAccountDataDto);
    }
}