using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Db
{
    public class AccountSession
    {
        public long Id { get; set; }

        [ForeignKey(nameof(Account))]
        public long AccountId { get; set; }

        public virtual Account Account { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Token { get; set; }
    }
}