using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Db.Account;

namespace Infrastructure.Implementations
{
    public class AccountRepository : RepositoryBase, IAccountRepository
    {
        public AccountRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<AccountBase> GetById(long id)
        {
            return await Context.Accounts.FindAsync(id);
        }

        public async Task<AccountBase> GetByLogin(string login)
        {
            return await Context.Accounts.FirstOrDefaultAsync(a => a.Login == login);
        }

        public async Task Update(AccountBase account)
        {
            Context.Accounts.Update(account);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(AccountBase account)
        {
            Context.Accounts.Remove(account);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(AccountBase account)
        {
            Context.Accounts.Add(account);
            await Context.SaveChangesAsync();
        }
    }
}