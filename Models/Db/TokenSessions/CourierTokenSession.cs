using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Account;

namespace Models.Db.TokenSessions
{
    public class CourierTokenSession : TokenSessionBase
    {
        [ForeignKey(nameof(CourierAccount))]
        public long CourierAccountId { get; set; }

        public virtual CourierAccount CourierAccount { get; set; }
    }
}