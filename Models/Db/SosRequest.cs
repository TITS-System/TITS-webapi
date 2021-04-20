using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Account;

namespace Models.Db
{
    public class SosRequest
    {
        public long Id { get; set; }

        [ForeignKey(nameof(CourierAccount))]
        public long CourierAccountId { get; set; }

        public virtual CourierAccount CourierAccount { get; set; }

        public DateTime CreationDateTime { get; set; }

        public DateTime? ResolveDateTime { get; set; }

        [ForeignKey(nameof(ResolverManagerAccount))]
        public long? ResolverManagerAccountId { get; set; }

        public virtual ManagerAccount ResolverManagerAccount { get; set; }
    }
}