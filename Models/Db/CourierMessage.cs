using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Account;

namespace Models.Db
{
    public class CourierMessage
    {
        public long Id { get; set; }
        
        public string Content { get; set; }

        public DateTime CreationDateTime { get; set; }

        [ForeignKey(nameof(CourierAccount))]
        public long CourierAccountId { get; set; }

        public virtual CourierAccount CourierAccount { get; set; }
    }
}