using System.Collections.Generic;

namespace Models.Dtos.Requests
{
    public class CreateOrderDto
    {
        public long DestinationId { get; set; }
        
        public ICollection<long> Templates { get; set; }
    }
}