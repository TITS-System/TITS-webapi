using System.Collections.Generic;
using Models.DTOs.Responses;

namespace Models.DTOs.General
{
    public class ProductDto
    {
        public string Title { get; set; }

        public float Price { get; set; }

        public ICollection<IngredientDto> Ingredients { get; set; }

        public ProductCategoryDto ProductCategory { get; set; }
    }
}