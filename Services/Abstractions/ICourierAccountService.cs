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

        Task<CourierFullInfoDto> GetFullInfo(long courierId);

        Task<GetRolesResultDto> GetRoles(long courierId);
        
        Task Delete(long courierId);

        Task ChangeCourierProfile(ChangeCourierProfileDto changeCourierProfileDto);
    }
}