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
        
        [ForeignKey(nameof(MainRestaurant))]
        public long? MainRestaurantId { get; set; }

        public virtual Restaurant MainRestaurant { get; set; }

        public long? LastLatLngId { get; set; }
        
        [ForeignKey(nameof(LastWorkerSession))]
        public long? LastWorkerSessionId { get; set; }

        public virtual WorkerSession LastWorkerSession { get; set; }
        
        public virtual ICollection<Delivery> Deliveries { get; set; }
        
        // public virtual ICollection<Order> CreatedOrders { get; set; }
    }
}