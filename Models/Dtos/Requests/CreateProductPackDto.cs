using System.Collections.Generic;

namespace Models.DTOs.Requests
{
    public class CreateProductPackDto
    {
        public ICollection<long> ProductIds { get; set; }
    }
}