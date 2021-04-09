using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db
{
    public class Account
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public virtual ICollection<AccountToRole> Roles { get; set; }

        [ForeignKey(nameof(MainWorkPoint))]
        public long MainWorkPointId { get; set; }

        public virtual WorkPoint MainWorkPoint { get; set; }

        [ForeignKey(nameof(LastWorkSession))]
        public long? LastWorkSessionId { get; set; }

        public virtual WorkSession LastWorkSession { get; set; }

        public virtual ICollection<LatLng> LatLngs { get; set; }
    }
}