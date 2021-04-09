using System.Collections.Generic;

namespace Models.Db
{
    public class ProductCategory
    {
        public long Id { get; set; }

        public string TitleEn { get; set; }

        public string TitleRu { get; set; }

        public virtual ICollection<MenuProduct> ProductTemplates { get; set; }
        
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}