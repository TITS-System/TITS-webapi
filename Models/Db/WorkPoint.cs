using System.Collections.Generic;

namespace Models.Db
{
    public class WorkPoint
    {
        public long Id { get; set; }

        public virtual ICollection<Account> AssignedAccounts { get; set; }
        
        public virtual ICollection<WorkSession> WorkSessions { get; set; }
    }
}