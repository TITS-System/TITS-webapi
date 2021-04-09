using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db
{
    public class OrderProductPack
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public float Price { get; set; }
        
        public virtual ICollection<OrderProduct> Products { get; set; }

        [ForeignKey(nameof(Order))]
        public long OrderId { get; set; }
        
        public virtual Order Order { get; set; }
    }
}