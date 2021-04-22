using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.Extensions.Logging;
using Models.Db;
using Models.Db.Account;
using Models.Dtos;
using Models.Enums;
using Services.Abstractions;

namespace Services.Implementations
{
    public class AutoDeliveryServerService : IAutoDeliveryServerService
    {
        private IOrderRepository _orderRepository;
        private IDeliveryRepository _deliveryRepository;
        private IRestaurantRepository _restaurantRepository;
        private ICourierAccountRepository _courierAccountRepository;

        private ICourierAccountService _courierAccountService;
        private IRestaurantService _restaurantService;
        private IOrderService _orderService;
        private IDeliveryService _deliveryService;

        private Thread _autoDeliveryServerThread;

        private ILogger<AutoDeliveryServerService> _logger;

        public AutoDeliveryServerService(ICourierAccountService courierAccountService, IOrderService orderService, IDeliveryService deliveryService, IRestaurantService restaurantService, IOrderRepository orderRepository, IDeliveryRepository deliveryRepository, IRestaurantRepository restaurantRepository, ICourierAccountRepository courierAccountRepository, ILogger<AutoDeliveryServerService> logger)
        {
            _courierAccountService = courierAccountService;
            _orderService = orderService;
            _deliveryService = deliveryService;
            _restaurantService = restaurantService;
            _orderRepository = orderRepository;
            _deliveryRepository = deliveryRepository;
            _restaurantRepository = restaurantRepository;
            _courierAccountRepository = courierAccountRepository;
            _logger = logger;

            _autoDeliveryServerThread = new Thread(AutoDeliveryServerRoutine);
            _autoDeliveryServerThread.Start();
        }

        private async void AutoDeliveryServerRoutine()
        {
            while (Thread.CurrentThread.ThreadState != ThreadState.AbortRequested)
            {
                // force wait while db context is initiating
                Thread.Sleep(5000);
                _logger.LogWarning("AutoDeliveryService - serving");
                var restaurants = await _restaurantRepository.GetAllAutoDeliveryServed();
                
                _logger.LogWarning($"There are {restaurants.Count} restaurants with auto serving");

                foreach (var restaurant in restaurants)
                {
                    var unservedOrders = await _orderRepository.GetUnserved(restaurant.Id);
                    var couriers = await _courierAccountRepository.GetByRestaurantAndOnWork(restaurant.Id);

                    // Order couriers
                    var couriersLoad = new List<(CourierAccount courier, int load)>();

                    foreach (var courier in couriers)
                    {
                        var courierDeliveries = await _deliveryRepository.GetByCourierIdAndDate(courier.Id, DateTime.Today.AddDays(-2), DateTime.Today.AddDays(2));

                        couriersLoad.Add((courier, courierDeliveries.Count(d => d.Status == DeliveryStatus.InProgress)));
                    }

                    var orderedCouriers = couriersLoad.OrderBy(cl => cl.load).Select(cl => cl.courier);

                    var couriersQueue = new Queue<CourierAccount>(orderedCouriers);

                    var unservedOrdersQueue = new Queue<Order>(unservedOrders);

                    while (unservedOrdersQueue.Count > 0 && couriersQueue.Count > 0)
                    {
                        var unservedOrder = unservedOrdersQueue.Dequeue();
                        var courier = couriersQueue.Dequeue();

                        await _deliveryService.BeginDelivery(new BeginDeliveryDto()
                        {
                            CourierId = courier.Id,
                            OrderId = unservedOrder.Id
                        });
                    }
                }
            }
        }

        public async Task SetAutoDeliveryMode(long restaurantId, bool mode)
        {
            var restaurant = await _restaurantRepository.GetById(restaurantId);

            if (restaurant == null)
            {
                throw new("Restaurant not found");
            }

            restaurant.UseAutoDeliveryServer = mode;
            await _restaurantRepository.Update(restaurant);
        }

        public async Task<bool> GetMode(long restaurantId)
        {
            var restaurant = await _restaurantRepository.GetById(restaurantId);

            if (restaurant == null)
            {
                throw new("Restaurant not found");
            }

            return restaurant.UseAutoDeliveryServer;
        }
    }
}