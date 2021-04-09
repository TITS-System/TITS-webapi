using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db
{
    public class WorkSessionPause
    {
        public long Id { get; set; }

        [ForeignKey(nameof(WorkSession))]
        public long WorkSessionId { get; set; }

        public virtual WorkSession WorkSession { get; set; }

        public bool IsClosed { get; set; }

        // datetime when user started a pause
        // can't be null
        // (because what it should be, an unstarted pause WTF ???)
        public DateTime StartDateTime { get; set; }
        
        // datetime when user ended his pause
        public DateTime? EndDateTime { get; set; }
    }
}