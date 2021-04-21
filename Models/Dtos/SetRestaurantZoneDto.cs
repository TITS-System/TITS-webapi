using System.Collections.Generic;

namespace Models.Dtos
{
    public class SetRestaurantZoneDto
    {
        public long RestaurantId { get; set; }

        public ICollection<LatLngDto> LatLngs { get; set; }

        public SetRestaurantZoneDto(long restaurantId, ICollection<LatLngDto> latLngs)
        {
            RestaurantId = restaurantId;
            LatLngs = latLngs;
        }
    }
}