using System.Threading.Tasks;
using Models.Db.Account;

namespace Infrastructure.Abstractions
{
    public interface IAccountRepository
    {
        Task<AccountBase> GetById(long id);

        Task<AccountBase> GetByLogin(string login);

        Task Update(AccountBase account);
        Task Remove(AccountBase account);
        Task Insert(AccountBase account);
    }
}