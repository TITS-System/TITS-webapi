using System.Collections.Generic;
using Models.Dtos.General;

namespace Models.Dtos.Responses
{
    public class OrderDto
    {
        public long Id { get; set; }
        public LatLngDto Destination { get; set; }
        public ICollection<OrderProductDto> Products { get; set; }
        public CourierDto Courier { get; set; }
    }
}