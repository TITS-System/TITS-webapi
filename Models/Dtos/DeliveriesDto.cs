using System.Collections;
using System.Collections.Generic;

namespace Models.Dtos
{
    public class DeliveriesDto
    {
        public DeliveriesDto(ICollection<DeliveryDto> deliveries)
        {
            Deliveries = deliveries;
        }

        public ICollection<DeliveryDto> Deliveries { get; set; }
    }
}