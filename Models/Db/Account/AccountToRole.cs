using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db.Account
{
    public class AccountToRole
    {
        [ForeignKey(nameof(CourierAccount))]
        public long WorkerAccountId { get; set; }

        public virtual CourierAccount CourierAccount { get; set; }

        [ForeignKey(nameof(WorkerRole))]
        public long WorkerRoleId { get; set; }

        public virtual WorkerRole WorkerRole { get; set; }
    }
}