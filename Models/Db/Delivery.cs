using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Account;
using Models.Enums;

namespace Models.Db
{
    public class Delivery
    {
        public long Id { get; set; }

        [ForeignKey(nameof(Order))]
        public long OrderId { get; set; }

        public virtual Order Order { get; set; }

        public DateTime StartTime { get; set; }
        
        public DateTime? EndTime { get; set; }

        // Delivery is only created, when courier takes an orderб, so it's never null
        [ForeignKey(nameof(CourierAccount))]
        public long CourierAccountId { get; set; }

        public virtual WorkerAccount CourierAccount { get; set; }

        public virtual ICollection<LatLng> LatLngs { get; set; }

        public DeliveryStatus Status { get; set; }
    }
}