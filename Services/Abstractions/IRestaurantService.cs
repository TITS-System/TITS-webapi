using System.Threading.Tasks;
using Models.Db;
using Models.Dtos;

namespace Services.Abstractions
{
    public interface IRestaurantService
    {
        Task<GetCouriersResultDto> GetCouriers(long restaurantId);

        Task<RestaurantDto> GetInfo(long restaurantId);
    }
}