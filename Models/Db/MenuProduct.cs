using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db
{
    public class MenuProduct
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public float Price { get; set; }
        
        public virtual ICollection<MenuIngredient> Ingredients { get; set; }

        [ForeignKey(nameof(ProductCategory))]
        public long ProductCategoryId { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        [ForeignKey(nameof(MenuProductPack))]
        public long ProductPackId { get; set; }
        
        public virtual MenuProductPack MenuProductPack { get; set; }
    }
}