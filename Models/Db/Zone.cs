using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db
{
    public class Zone
    {
        public long Id { get; set; }

        [ForeignKey(nameof(Restaurant))]
        public long? RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public virtual ICollection<LatLng> LatLngs { get; set; }
    }
}