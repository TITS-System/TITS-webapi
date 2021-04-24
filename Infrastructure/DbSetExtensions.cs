using System.Linq;
using Models.Db.Account;

namespace Infrastructure
{
    public static class DbSetExtensions
    {
        public static IQueryable<CourierAccount> TakeNonDeleted(this IQueryable<CourierAccount> dbSet)
        {
            return dbSet.Where(ca => !ca.IsDeleted);
        }
    }
}