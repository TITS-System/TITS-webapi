using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Account;

namespace Models.Db
{
    public class CourierSession
    {
        public long Id { get; set; }

        public bool IsClosed { get; set; }
        
        // This is an actual time, when session was opened by user
        // can't be null
        // (because what it should be, an unstarted active session WTF ???)
        public DateTime OpenDateTime { get; set; }
        
        // This is an actual time, when session was closed by user
        public DateTime? CloseDateTime { get; set; }

        [ForeignKey(nameof(CourierAccount))]
        public long CourierAccountId { get; set; }
        
        public virtual CourierAccount CourierAccount { get; set; }
    }
}