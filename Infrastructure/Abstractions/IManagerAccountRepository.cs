using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Db.Account;

namespace Infrastructure.Abstractions
{
    public interface IManagerAccountRepository
    {
        Task<ManagerAccount> GetById(long id);

        Task<ManagerAccount> GetByLogin(string login);

        Task Update(ManagerAccount managerAccount);

        Task Remove(ManagerAccount managerAccount);

        Task Insert(ManagerAccount managerAccount);
    }
}