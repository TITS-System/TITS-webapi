using System.Collections.Generic;

namespace Models.Db
{
    public class Courier
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public virtual ICollection<LatLng> LatLngs { get; set; }
    }
}