using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db
{
    public class LatLng
    {
        public long Id { get; set; }

        public float Lat { get; set; }

        public float Lng { get; set; }

        [ForeignKey(nameof(Account))]
        public long AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}