using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.TokenSessions;

namespace Models.Db.Account
{
    public class ManagerAccount : AccountBase
    {
        [ForeignKey(nameof(LastTokenSession))]
        public long? LastTokenSessionId { get; set; }

        public virtual ManagerTokenSession LastTokenSession { get; set; }
    }
}