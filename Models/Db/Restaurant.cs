using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db
{
    public class Restaurant
    {
        public long Id { get; set; }

        [ForeignKey(nameof(LocationLatLng))]
        public long LocationLatLngId { get; set; }

        public virtual LatLng LocationLatLng { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}