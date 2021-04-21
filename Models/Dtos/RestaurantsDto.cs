using System.Collections.Generic;

namespace Models.Dtos
{
    public class RestaurantsDto
    {
        public ICollection<RestaurantDto> Restaurants { get; set; }

        public RestaurantsDto(ICollection<RestaurantDto> restaurants)
        {
            Restaurants = restaurants;
        }
    }
}