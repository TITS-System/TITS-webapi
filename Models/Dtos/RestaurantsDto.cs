using System.Collections.Generic;

namespace Models.Dtos
{
    public class RestaurantsDto
    {
        public ICollection<RestaurantDto> Restaurant { get; set; }

        public RestaurantsDto(ICollection<RestaurantDto> restaurant)
        {
            Restaurant = restaurant;
        }
    }
}