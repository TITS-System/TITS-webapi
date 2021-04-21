using System.Collections.Generic;

namespace Models.Dtos
{
    public class OrdersDto
    {
        public ICollection<OrderDto> Orders { get; set; }

        public OrdersDto(ICollection<OrderDto> orders)
        {
            Orders = orders;
        }
    }
}