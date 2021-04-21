using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.Verbatims;
using Models.Db.Account;
using Models.Dtos;
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

        public async Task<ManagerFullInfoDto> GetManagerInfo(long managerId)
        {
            var managerAccount = await _managerAccountRepository.GetById(managerId);

            var managerFullInfoDto = _mapper.Map<ManagerFullInfoDto>(managerAccount);

            return managerFullInfoDto;
        }

        public async Task ChangeManagerProfile(ChangeManagerProfileDto changeManagerProfileDto)
        {
            var managerAccount = await _managerAccountRepository.GetById(changeManagerProfileDto.ManagerId);

            if (managerAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            managerAccount.Login = string.IsNullOrEmpty(changeManagerProfileDto.Login) ? managerAccount.Login : changeManagerProfileDto.Login;
            managerAccount.Password = string.IsNullOrEmpty(changeManagerProfileDto.Password) ? managerAccount.Password : changeManagerProfileDto.Password;
            managerAccount.Username = string.IsNullOrEmpty(changeManagerProfileDto.Username) ? managerAccount.Username : changeManagerProfileDto.Username;

            await _managerAccountRepository.Update(managerAccount);
        }
    }
}