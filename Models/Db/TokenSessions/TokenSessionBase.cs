using System;

namespace Models.Db.TokenSessions
{
    public class TokenSessionBase
    {
        public long Id { get; set; }

        public DateTime StartDate { get; set; }

        // Not null, because token has an expiration date
        public DateTime EndDate { get; set; }

        public string Token { get; set; }
    }
}