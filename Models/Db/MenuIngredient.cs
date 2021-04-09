using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db
{
    public class MenuIngredient
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public int Weight { get; set; }

        // TODO: Price for 100g, or 1kg
        // public float Price { get; set; }

        [ForeignKey(nameof(MenuProduct))]
        public long ProductId { get; set; }
        
        public virtual MenuProduct MenuProduct { get; set; }
    }
}