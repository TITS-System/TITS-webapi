using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db
{
    public class AccountToRole
    {
        [ForeignKey(nameof(Account))]
        public long AccountId { get; set; }

        public virtual Account Account { get; set; }

        [ForeignKey(nameof(Role))]
        public long RoleId { get; set; }

        public virtual AccountRole Role { get; set; }
    }
}