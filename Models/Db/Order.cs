using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db
{
    public class Order
    {
        public long Id { get; set; }

        [ForeignKey(nameof(Account))]
        public long AccountId { get; set; }

        public virtual Account Account { get; set; }

        public DateTime CreationDateTime { get; set; }

        public virtual ICollection<OrderProductPack> ProductPacks { get; set; }
    }
}