using System.Threading.Tasks;
using Models.Dtos;

namespace Services.Abstractions
{
    public interface IZoneService
    {
        Task<LatLngsDto> GetRestaurantZone(long restaurantId);

        Task SetRestaurantZone(SetRestaurantZoneDto setRestaurantZoneDto);
    }
}