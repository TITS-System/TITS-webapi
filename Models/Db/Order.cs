using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db
{
    public class Order
    {
        public long Id { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey(nameof(DestinationLatLng))]
        public long DestinationLatLngId { get; set; }

        public virtual LatLng DestinationLatLng { get; set; }

        [ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}