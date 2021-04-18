using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Sessions;

namespace Models.Db.Account
{
    public class WorkerAccount : AccountBase
    {
        public string Password { get; set; }

        public virtual ICollection<WorkerAccountToRole> WorkerRoles { get; set; }
        
        [ForeignKey(nameof(LastTokenSession))]
        public long? LastTokenSessionId { get; set; }

        public virtual TokenSession LastTokenSession { get; set; }

        public long? LastLatLngId { get; set; }
        
        
        
        // public virtual ICollection<Order> CreatedOrders { get; set; }
    }
}