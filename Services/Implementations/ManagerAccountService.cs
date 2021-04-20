using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Models.Db.Account;
using Models.DTOs.Misc;
using Models.DTOs.WorkerAccountDtos;
using Services.Abstractions;

namespace Services.Implementations
{
    public class ManagerAccountService : IManagerAccountService
    {
        private IManagerAccountRepository _managerAccountRepository;

        private IWorkerRoleRepository _workerRoleRepository;

        private IWorkerToRoleRepository _workerToRoleRepository;

        private IRestaurantRepository _restaurantRepository;

        private IMapper _mapper;
        
        public ManagerAccountService(IManagerAccountRepository managerAccountRepository, IWorkerRoleRepository workerRoleRepository, IWorkerToRoleRepository workerToRoleRepository, IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _managerAccountRepository = managerAccountRepository;
            _workerToRoleRepository = workerToRoleRepository;
            _workerRoleRepository = workerRoleRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        
        public async Task<CreatedDto> CreateManager(CreateManagerAccountDto createManagerAccountDto)
        {
            var managerAccount = _mapper.Map<ManagerAccount>(createManagerAccountDto);

            var findLoginManagerAccount = await _managerAccountRepository.GetByLogin(managerAccount.Login);

            if (findLoginManagerAccount != null)
            {
                throw new("Login already exists");
            }

            await _managerAccountRepository.Insert(managerAccount);

            return new CreatedDto(managerAccount.Id);
        }
    }
}