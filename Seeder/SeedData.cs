using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure;
using Infrastructure.Abstractions;
using Infrastructure.Implementations;
using Infrastructure.Verbatims;
using Microsoft.Extensions.Logging;
using Models.Db;
using Models.Db.Account;
using Models.Dtos;
using Models.DTOs;
using Models.DTOs.WorkerAccountDtos;
using Npgsql.Logging;
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
        private IZoneRepository _zoneRepository;
        private DeliveryService _deliveryService;
        private OrderService _orderService;

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
            var sosRequestRepository = new SosRequestRepository(Context);
            var orderRepository = new OrderRepository(Context);
            _zoneRepository = new ZoneRepository(Context);
            var deliveryRepository = new DeliveryRepository(Context);
            _restaurantRepository = restaurantRepository;
            _latLngRepository = new LatLngRepository(Context);

            _courierAccountService = new CourierAccountService(courierAccountRepository, workerRoleRepository, workerToRoleRepository, restaurantRepository, sosRequestRepository, mapper);
            _managerAccountService = new ManagerAccountService(managerAccountRepository, workerRoleRepository, workerToRoleRepository, restaurantRepository, mapper);
            _accountRoleService = new AccountRoleService(accountRepository, workerRoleRepository, workerToRoleRepository, mapper);
            _deliveryService = new DeliveryService(orderRepository, deliveryRepository, courierAccountRepository, _latLngRepository, mapper, new LoggerFactory().CreateLogger<DeliveryService>());
            _orderService = new OrderService(mapper, orderRepository, _restaurantRepository, _latLngRepository, _deliveryService);
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


            var zone = await SeedZone();

            // Save it with a relation
            var restaurant = new Restaurant() {LocationLatLngId = latLng.Id, AddressString = "ул. Крахмалева, 49, Брянск, Брянская обл., 241050", ZoneId = zone.Id};
            await _restaurantRepository.Insert(restaurant);

            zone.RestaurantId = restaurant.Id;
            await _zoneRepository.Update(zone);

            // Save back reference id
            latLng.RestaurantLocationId = restaurant.Id;
            await _latLngRepository.Update(latLng);

            var courierId = (await _courierAccountService.CreateCourier(new CreateCourierAccountDto() {Login = "Courier", Password = "Courier", Username = "Misha", RestaurantId = restaurant.Id})).Id;
            var managerId = (await _managerAccountService.CreateManager(new CreateManagerAccountDto() {Login = "Manager", Password = "Manager", Username = "Vitaliy"})).Id;

            await _accountRoleService.AddToRole(courierId, WorkerRolesVerbatim.Courier);
            await _accountRoleService.AddToRole(managerId, WorkerRolesVerbatim.Manager);

            await _courierAccountService.AssignToRestaurant(new AssignToRestaurantDto() {CourierId = courierId, RestaurantId = restaurant.Id});

            await _orderService.Create(new CreateOrderDto(restaurant.Id, "Содержание заказа #1", "Адрес #1", "Доп адрес #1", new LatLngDto() {Lat = 51, Lng = 51}));
            await _orderService.Create(new CreateOrderDto(restaurant.Id, "Содержание заказа #2", "Адрес #2", "Доп адрес #2", new LatLngDto() {Lat = 52, Lng = 52}));
            await _orderService.Create(new CreateOrderDto(restaurant.Id, "Содержание заказа #3", "Адрес #3", "Доп адрес #3", new LatLngDto() {Lat = 53, Lng = 53}));
            await _orderService.Create(new CreateOrderDto(restaurant.Id, "Содержание заказа #4", "Адрес #4", "Доп адрес #4", new LatLngDto() {Lat = 54, Lng = 54}));
            await _orderService.Create(new CreateOrderDto(restaurant.Id, "Содержание заказа #5", "Адрес #5", "Доп адрес #5", new LatLngDto() {Lat = 55, Lng = 55}));
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

        private async Task<Zone> SeedZone()
        {
            Zone zone = new Zone();

            await _zoneRepository.Insert(zone);
            
            // {lat:53.263692, lng:34.339379},{lat:53.263769, lng: 34.344400},{lat:53.261696, lng:34.344690},{lat:53.260977, lng:34.340087}
            LatLng[] points = {
                new() {Lat = 53.263692f, Lng = 34.339379f, ZoneId = zone.Id},
                new() {Lat = 53.263769f, Lng = 34.344400f, ZoneId = zone.Id},
                new() {Lat = 53.261696f, Lng = 34.344690f, ZoneId = zone.Id},
                new() {Lat = 53.260977f, Lng = 34.340087f, ZoneId = zone.Id},
            };

            await _latLngRepository.Insert(points);

            return zone;
        }
    }
}