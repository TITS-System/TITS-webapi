using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db
{
    public class OrderProduct
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<OrderIngredient> Ingredients { get; set; }

        public float Price { get; set; }

        [ForeignKey(nameof(ProductCategory))]
        public long ProductCategoryId { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        [ForeignKey(nameof(OrderProductPack))]
        public long ProductPackId { get; set; }

        public virtual OrderProductPack ProductPack { get; set; }
    }
}