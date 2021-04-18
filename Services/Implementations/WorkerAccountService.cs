using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.Verbatims;
using Models.Db.Account;
using Models.DTOs;
using Models.DTOs.Misc;
using Models.DTOs.Responses;
using Models.DTOs.WorkerAccountDtos;
using Services.Abstractions;

namespace Services.Implementations
{
    public class WorkerAccountService : IWorkerAccountService
    {
        private IWorkerAccountRepository _workerAccountRepository;

        private IWorkerRoleRepository _workerRoleRepository;

        private IWorkerToRoleRepository _workerToRoleRepository;

        private IMapper _mapper;

        public WorkerAccountService(IWorkerAccountRepository workerAccountRepository, IWorkerRoleRepository workerRoleRepository, IWorkerToRoleRepository workerToRoleRepository, IMapper mapper)
        {
            _workerAccountRepository = workerAccountRepository;
            _workerToRoleRepository = workerToRoleRepository;
            _workerRoleRepository = workerRoleRepository;
            _mapper = mapper;
        }

        public async Task<CreatedDto> CreateAccount(CreateWorkerAccountDto createWorkerAccountDto)
        {
            var workerAccount = _mapper.Map<WorkerAccount>(createWorkerAccountDto);

            await _workerAccountRepository.Insert(workerAccount);

            return new CreatedDto(workerAccount.Id);
        }

        public async Task<GetRolesResultDto> GetRoles(long workerId)
        {
            var workerAccount = await _workerAccountRepository.GetById(workerId);

            if (workerAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            var roles = await _workerToRoleRepository.GetWorkerRoles(workerAccount.Id);

            var roleDtos = _mapper.Map<IEnumerable<WorkerRoleDto>>(roles);

            return new GetRolesResultDto(roleDtos);
        }
    }
}