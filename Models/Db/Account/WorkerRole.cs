using System.Collections.Generic;

namespace Models.Db.Account
{
    public class WorkerRole
    {
        public long Id { get; set; }

        public string TitleEn { get; set; }

        public string TitleRu { get; set; }

        public virtual ICollection<WorkerAccountToRole> Users { get; set; }
    }
}