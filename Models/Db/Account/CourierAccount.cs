using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.TokenSessions;

namespace Models.Db.Account
{
    public class CourierAccount : AccountBase
    {
        [ForeignKey(nameof(LastTokenSession))]
        public long? LastTokenSessionId { get; set; }

        public virtual CourierTokenSession LastTokenSession { get; set; }

        [ForeignKey(nameof(AssignedToRestaurant))]
        public long AssignedToRestaurantId { get; set; }

        public virtual Restaurant AssignedToRestaurant { get; set; }

        public long? LastLatLngId { get; set; }

        [ForeignKey(nameof(LastCourierSession))]
        public long? LastCourierSessionId { get; set; }

        public virtual CourierSession LastCourierSession { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }

        public virtual ICollection<CourierMessage> CourierMessages { get; set; }

        // public virtual ICollection<Order> CreatedOrders { get; set; }
    }
}