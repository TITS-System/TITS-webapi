using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Order
    {
        public long Id { get; set; }

        [ForeignKey(nameof(Destination))]
        public long DestinationId { get; set; }

        public virtual LatLng Destination { get; set; }

        public virtual ICollection<OrderProduct> Products { get; set; }

        [ForeignKey(nameof(Courier))]
        public long? CourierId { get; set; }

        public virtual Courier Courier { get; set; }
    }
}