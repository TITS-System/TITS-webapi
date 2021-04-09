using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db
{
    public class WorkSession
    {
        public long Id { get; set; }

        [ForeignKey(nameof(ScheduledWorkSession))]
        public long? ScheduledWorkSessionId { get; set; }

        public virtual ScheduledWorkSession ScheduledWorkSession  { get; set; }

        public bool IsClosed { get; set; }
        
        // This is an actual time, when session was opened by user
        // can't be null
        // (because what it should be, an unstarted active session WTF ???)
        public DateTime OpenDateTime { get; set; }
        
        // This is an actual time, when session was closed by user
        public DateTime? CloseDateTime { get; set; }

        [ForeignKey(nameof(Account))]
        public long AccountId { get; set; }
        
        public virtual Account Account { get; set; }

        [ForeignKey(nameof(WorkPoint))]
        public long WorkPointId { get; set; }

        public virtual WorkPoint WorkPoint { get; set; }

        // All pauses of this work session
        public ICollection<WorkSessionPause> WorkSessionPauses { get; set; }
    }
}