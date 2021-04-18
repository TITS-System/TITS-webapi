using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.Verbatims;
using Models.Db.Account;
using Services.Abstractions;

namespace Services.Implementations
{
    public class WorkerRoleService : IWorkerRoleService
    {
        private IWorkerAccountRepository _workerAccountRepository;

        private IWorkerRoleRepository _workerRoleRepository;

        private IWorkerToRoleRepository _workerToRoleRepository;

        private IMapper _mapper;

        public WorkerRoleService(IWorkerAccountRepository workerAccountRepository, IWorkerRoleRepository workerRoleRepository, IWorkerToRoleRepository workerToRoleRepository, IMapper mapper)
        {
            _workerAccountRepository = workerAccountRepository;
            _workerRoleRepository = workerRoleRepository;
            _workerToRoleRepository = workerToRoleRepository;
            _mapper = mapper;
        }

        public async Task AddToRole(long workerId, long roleId)
        {
            var workerAccount = await _workerAccountRepository.GetById(workerId);

            if (workerAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            var role = await _workerRoleRepository.GetById(roleId);

            if (role == null)
            {
                throw new("Role not found");
            }

            WorkerAccountToRole workerAccountToRole = new WorkerAccountToRole()
            {
                WorkerAccountId = workerId,
                WorkerRoleId = role.Id
            };

            await _workerToRoleRepository.Insert(workerAccountToRole);
        }

        public async Task AddToRole(long workerId, string roleTitleEn)
        {
            var workerAccount = await _workerAccountRepository.GetById(workerId);

            if (workerAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            var role = await _workerRoleRepository.GetByTitleEn(roleTitleEn);

            if (role == null)
            {
                throw new("Role not found");
            }

            WorkerAccountToRole workerAccountToRole = new WorkerAccountToRole()
            {
                WorkerAccountId = workerId,
                WorkerRoleId = role.Id
            };

            await _workerToRoleRepository.Insert(workerAccountToRole);
        }

        public async Task RemoveFromRole(long workerId, long roleId)
        {
            var workerAccount = await _workerAccountRepository.GetById(workerId);

            if (workerAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            var role = await _workerRoleRepository.GetById(roleId);

            if (role == null)
            {
                throw new("Role not found");
            }

            var workerAccountToRole = await _workerToRoleRepository.GetPair(workerId, roleId);

            if (workerAccountToRole == null)
            {
                throw new("Account is not in role");
            }

            await _workerToRoleRepository.Remove(workerAccountToRole);
        }

        public async Task RemoveFromRole(long workerId, string roleTitleEn)
        {
            var workerAccount = await _workerAccountRepository.GetById(workerId);

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