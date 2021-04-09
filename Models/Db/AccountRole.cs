using System.Collections.Generic;

namespace Models.Db
{
    public class AccountRole
    {
        public long Id { get; set; }

        public string TitleEn { get; set; }

        public string TitleRu { get; set; }

        public virtual ICollection<AccountToRole> Users { get; set; }
    }
}