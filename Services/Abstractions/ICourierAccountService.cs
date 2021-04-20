using System.Threading.Tasks;
using Models.Dtos;
using Models.DTOs;
using Models.DTOs.Misc;
using Models.DTOs.WorkerAccountDtos;

namespace Services.Abstractions
{
    public interface ICourierAccountService
    {
        Task<CreatedDto> CreateCourier(CreateCourierAccountDto createCourierAccountDto);
        
        Task AssignToRestaurant(AssignToRestaurantDto assignToRestaurantDto);

        Task<GetRolesResultDto> GetRoles(long courierId);

        Task ChangeCourierData(ChangeAccountDataDto changeAccountDataDto);
    }
}