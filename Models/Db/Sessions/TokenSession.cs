using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Account;

namespace Models.Db.Sessions
{
    public class TokenSession
    {
        public long Id { get; set; }

        [ForeignKey(nameof(WorkerAccount))]
        public long WorkerAccountId { get; set; }

        public virtual WorkerAccount WorkerAccount { get; set; }

        public DateTime StartDate { get; set; }

        // Not null, because token has an expiration date
        public DateTime EndDate { get; set; }

        public string Token { get; set; }
    }
}