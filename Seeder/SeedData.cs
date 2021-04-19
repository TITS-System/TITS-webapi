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
        private IWorkerAccountService _workerAccountService;
        private IWorkerRoleService _workerRoleService;

        private IRestaurantRepository _restaurantRepository;
        private ILatLngRepository _latLngRepository;
        
        public SeedData()
        {
            Context = new TitsDbContext();

            IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new TitsAutomapperProfile())));

            var workerAccountRepository = new WorkerAccountRepository(Context);
            var workerRoleRepository = new WorkerRoleRepository(Context);
            var workerToRoleRepository = new WorkerToRoleRepository(Context);
            var restaurantRepository = new RestaurantRepository(Context);
            _restaurantRepository = restaurantRepository;
            _latLngRepository = new LatLngRepository(Context);

            _workerAccountService = new WorkerAccountService(workerAccountRepository, workerRoleRepository, workerToRoleRepository, mapper, restaurantRepository);
            _workerRoleService = new WorkerRoleService(workerAccountRepository, workerRoleRepository, workerToRoleRepository, mapper);
        }

        private TitsDbContext Context { get; set; }

        public async Task Seed()
        {
            SeedAccountRoles();

            var courierId = (await _workerAccountService.CreateAccount(new CreateWorkerAccountDto() {Login = "Courier", Password = "Courier", Username = "Misha"})).Id;
            var managerId = (await _workerAccountService.CreateAccount(new CreateWorkerAccountDto() {Login = "Manager", Password = "Manager", Username = "Vitaliy"})).Id;

            await _workerRoleService.AddToRole(courierId, WorkerRolesVerbatim.Courier);
            await _workerRoleService.AddToRole(managerId, WorkerRolesVerbatim.Manager);

            // Create an entity
            var latLng = new LatLng() {Lat = 57.0f, Lng = 49.0f};
            await _latLngRepository.Insert(latLng);
            
            // Save it with a relation
            var restaurant = new Restaurant() {LocationLatLngId = latLng.Id};
            await _restaurantRepository.Insert(restaurant);
            
            // Save back reference id
            latLng.RestaurantId = restaurant.Id;
            await _latLngRepository.Update(latLng);

            await _workerAccountService.AssignToRestaurant(new AssignToRestaurantDto() {WorkerId = courierId, RestaurantId = restaurant.Id});
            await _workerAccountService.AssignToRestaurant(new AssignToRestaurantDto() {WorkerId = managerId, RestaurantId = restaurant.Id});
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