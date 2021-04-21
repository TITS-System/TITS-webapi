using System.Threading.Tasks;
using Models.Dtos;
using Models.DTOs.Misc;
using Models.DTOs.WorkerAccountDtos;

namespace Services.Abstractions
{
    public interface IManagerAccountService
    {
        Task<CreatedDto> CreateManager(CreateManagerAccountDto createManagerAccountDto);

        Task<ManagerFullInfoDto> GetManagerInfo(long managerId);
        
        Task ChangeManagerProfile(ChangeManagerProfileDto changeManagerProfileDto);
    }
}