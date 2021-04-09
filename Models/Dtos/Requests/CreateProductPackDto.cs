using System.Collections.Generic;

namespace Models.DTOs.Requests
{
    public class CreateProductPackDto
    {
        public string Title { get; set; }

        public float Price { get; set; }
        
        public ICollection<long> ProductIds { get; set; }
    }
}