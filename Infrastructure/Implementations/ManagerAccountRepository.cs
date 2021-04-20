using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Db.Account;

namespace Infrastructure.Implementations
{
    public class ManagerAccountRepository : RepositoryBase, IManagerAccountRepository
    {
        public ManagerAccountRepository(TitsDbContext context) : base(context)
        {
        }


        public async Task<ManagerAccount> GetById(long id)
        {
            return await Context.ManagerAccounts.FindAsync(id);
        }

        public async Task<ManagerAccount> GetByLogin(string login)
        {
            return await Context.ManagerAccounts.FirstOrDefaultAsync(wa => wa.Login == login);
        }

        public async Task Update(ManagerAccount managerAccount)
        {
            Context.ManagerAccounts.Update(managerAccount);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(ManagerAccount managerAccount)
        {
            Context.ManagerAccounts.Remove(managerAccount);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(ManagerAccount managerAccount)
        {
            Context.ManagerAccounts.Add(managerAccount);
            await Context.SaveChangesAsync();
        }
    }
}