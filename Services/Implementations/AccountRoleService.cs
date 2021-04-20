using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.Verbatims;
using Models.Db.Account;
using Services.Abstractions;

namespace Services.Implementations
{
    public class AccountRoleService : IAccountRoleService
    {
        private IAccountRepository _accountRepository;

        private IWorkerRoleRepository _workerRoleRepository;

        private IWorkerToRoleRepository _workerToRoleRepository;

        private IMapper _mapper;

        public AccountRoleService(IAccountRepository accountRepository, IWorkerRoleRepository workerRoleRepository, IWorkerToRoleRepository workerToRoleRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _workerRoleRepository = workerRoleRepository;
            _workerToRoleRepository = workerToRoleRepository;
            _mapper = mapper;
        }

        public async Task AddToRole(long accountId, long roleId)
        {
            var account = await _accountRepository.GetById(accountId);

            if (account == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            var role = await _workerRoleRepository.GetById(roleId);

            if (role == null)
            {
                throw new("Role not found");
            }

            AccountToRole accountToRole = new AccountToRole()
            {
                WorkerAccountId = accountId,
                WorkerRoleId = role.Id
            };

            await _workerToRoleRepository.Insert(accountToRole);
        }

        public async Task AddToRole(long accountId, string roleTitleEn)
        {
            var workerAccount = await _accountRepository.GetById(accountId);

            if (workerAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            var role = await _workerRoleRepository.GetByTitleEn(roleTitleEn);

            if (role == null)
            {
                throw new("Role not found");
            }

            AccountToRole accountToRole = new AccountToRole()
            {
                WorkerAccountId = accountId,
                WorkerRoleId = role.Id
            };

            await _workerToRoleRepository.Insert(accountToRole);
        }

        public async Task RemoveFromRole(long accountId, long roleId)
        {
            var workerAccount = await _accountRepository.GetById(accountId);

            if (workerAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            var role = await _workerRoleRepository.GetById(roleId);

            if (role == null)
            {
                throw new("Role not found");
            }

            var workerAccountToRole = await _workerToRoleRepository.GetPair(accountId, roleId);

            if (workerAccountToRole == null)
            {
                throw new("Account is not in role");
            }

            await _workerToRoleRepository.Remove(workerAccountToRole);
        }

        public async Task RemoveFromRole(long accountId, string roleTitleEn)
        {
            var workerAccount = await _accountRepository.GetById(accountId);

            if (workerAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            var role = await _workerRoleRepository.GetByTitleEn(roleTitleEn);

            if (role == null)
            {
                throw new("Role not found");
            }

            var workerAccountToRole = await _workerToRoleRepository.GetPair(workerAccount.Id, role.Id);

            if (workerAccountToRole == null)
            {
                throw new("Account is not in role");
            }

            await _workerToRoleRepository.Remove(workerAccountToRole);
        }
    }
}