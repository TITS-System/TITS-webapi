using System.Collections.Generic;

namespace Models.DTOs.General
{
    public class ProductPackDto
    {
        public string Title { get; set; }
        public float Price { get; set; }
        public ICollection<ProductDto> Products { get; set; }
    }
}