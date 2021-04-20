using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Account;

namespace Models.Db.TokenSessions
{
    public class ManagerTokenSession : TokenSessionBase
    {
        [ForeignKey(nameof(ManagerAccount))]
        public long ManagerAccountId { get; set; }

        public virtual ManagerAccount ManagerAccount { get; set; }
    }
}