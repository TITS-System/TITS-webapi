using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db
{
    public class ScheduledWorkSession
    {
        public long Id { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        [ForeignKey(nameof(Account))]
        public long AccountId { get; set; }
        
        public virtual Account Account { get; set; }

        [ForeignKey(nameof(WorkPoint))]
        public long WorkPointId { get; set; }

        public virtual WorkPoint WorkPoint { get; set; }
        
        public virtual ICollection<WorkSession> OpenedWorkSessions { get; set; }
    }
}