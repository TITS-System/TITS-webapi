using System.Collections.Generic;

namespace Models.DTOs.Requests
{
    public class CreateProductDto
    {
        public string Title { get; set; }

        public float Price { get; set; }
        
        public ICollection<long> Ingredients { get; set; }

        public long ProductCategoryId { get; set; }

        public long ProductPackId { get; set; }
    }
}