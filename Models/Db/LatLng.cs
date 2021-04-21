using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db
{
    public class LatLng
    {
        public long Id { get; set; }

        public float Lat { get; set; }
        public float Lng { get; set; }
        
        [ForeignKey(nameof(Order))]
        public long? OrderId { get; set; }
        
        public virtual Order Order { get; set; }
        
        [ForeignKey(nameof(RestaurantLocation))]
        public long? RestaurantLocationId { get; set; }
        
        public virtual Restaurant RestaurantLocation { get; set; }

        [ForeignKey(nameof(Delivery))]
        public long? DeliveryId { get; set; }

        public virtual Delivery Delivery { get; set; }

        [ForeignKey(nameof(Zone))]
        public long? ZoneId { get; set; }

        public virtual Zone Zone { get; set; }
    }
}