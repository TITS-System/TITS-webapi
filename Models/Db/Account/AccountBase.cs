using System.Collections.Generic;

namespace Models.Db.Account
{
    public class AccountBase
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Login { get; set; }
        
        public string Password { get; set; }

        public virtual ICollection<AccountToRole> WorkerRoles { get; set; }
    }
}