using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure;
using Infrastructure.Abstractions;
using Infrastructure.Implementations;
using Infrastructure.Verbatims;
using Models.Db;
using Models.Db.Account;
using Models.DTOs;
using Models.DTOs.WorkerAccountDtos;
using Services.Abstractions;
using Services.AutoMapperProfiles;
using Services.Implementations;

namespace Seeder
{
    public class SeedData
    {
        private ICourierAccountService _courierAccountService;
        private IManagerAccountService _managerAccountService;
        private IAccountRoleService _accountRoleService;

        private IRestaurantRepository _restaurantRepository;
        private ILatLngRepository _latLngRepository;

        public SeedData()
        {
            Context = new TitsDbContext();

            IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new TitsAutomapperProfile())));

            var courierAccountRepository = new CourierAccountRepository(Context);
            var managerAccountRepository = new ManagerAccountRepository(Context);
            var accountRepository = new AccountRepository(Context);
            var workerRoleRepository = new WorkerRoleRepository(Context);
            var workerToRoleRepository = new WorkerToRoleRepository(Context);
            var restaurantRepository = new RestaurantRepository(Context);
            _restaurantRepository = restaurantRepository;
            _latLngRepository = new LatLngRepository(Context);

            _courierAccountService = new CourierAccountService(courierAccountRepository, workerRoleRepository, workerToRoleRepository, restaurantRepository, mapper);
            _managerAccountService = new ManagerAccountService(managerAccountRepository, workerRoleRepository, workerToRoleRepository, restaurantRepository, mapper);
            _accountRoleService = new AccountRoleService(accountRepository, workerRoleRepository, workerToRoleRepository, mapper);
        }

        private TitsDbContext Context { get; set; }

        public async Task Seed()
        {
            SeedAccountRoles();


            // ДоДо пицца на Крахмалёва 49
            // LAT: 53.26324200333876 LNG: 34.34160463381722

            // Create an entity
            var latLng = new LatLng() {Lat = 53.26324200333876f, Lng = 34.34160463381722f};
            await _latLngRepository.Insert(latLng);

            // Save it with a relation
            var restaurant = new Restaurant() {LocationLatLngId = latLng.Id, AddressString = "ул. Крахмалева, 49, Брянск, Брянская обл., 241050"};
            await _restaurantRepository.Insert(restaurant);

            // Save back reference id
            latLng.RestaurantId = restaurant.Id;
            await _latLngRepository.Update(latLng);

            var courierId = (await _courierAccountService.CreateCourier(new CreateCourierAccountDto() {Login = "Courier", Password = "Courier", Username = "Misha", RestaurantId = restaurant.Id})).Id;
            var managerId = (await _managerAccountService.CreateManager(new CreateManagerAccountDto() {Login = "Manager", Password = "Manager", Username = "Vitaliy"})).Id;

            await _accountRoleService.AddToRole(courierId, WorkerRolesVerbatim.Courier);
            await _accountRoleService.AddToRole(managerId, WorkerRolesVerbatim.Manager);

            await _courierAccountService.AssignToRestaurant(new AssignToRestaurantDto() {CourierId = courierId, RestaurantId = restaurant.Id});
        }

        private void SeedAccountRoles()
        {
            Context.WorkerRoles.AddRange(
                from accountRoleEn in WorkerRolesVerbatim.EnToRu.Keys
                select new WorkerRole {TitleEn = accountRoleEn, TitleRu = WorkerRolesVerbatim.EnToRu[accountRoleEn]}
            );
            Context.SaveChanges();
            Console.WriteLine("Seeded account roles");
        }
    }
}