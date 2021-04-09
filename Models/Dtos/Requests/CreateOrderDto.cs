using System.Collections.Generic;

namespace Models.DTOs.Requests
{
    public class CreateOrderDto
    {
        public ICollection<long> ProductPackTemplateIds { get; set; }
    }
}