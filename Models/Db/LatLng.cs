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
        
        [ForeignKey(nameof(Restaurant))]
        public long? RestaurantId { get; set; }
        
        public virtual Restaurant Restaurant { get; set; }

        [ForeignKey(nameof(Delivery))]
        public long? DeliveryId { get; set; }

        public virtual Delivery Delivery { get; set; }
    }
}