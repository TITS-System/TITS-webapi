using System.Collections.Generic;

namespace Models.Db
{
    public class MenuProductPack
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public float Price { get; set; }
        
        public virtual ICollection<MenuProduct> Products { get; set; }
    }
}