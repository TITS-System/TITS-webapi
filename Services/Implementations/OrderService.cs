using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Models.Db;
using Models.Dtos;
using Models.DTOs.Misc;
using Services.Abstractions;

namespace Services.Implementations
{
    public class OrderService : IOrderService
    {
        private IMapper _mapper;

        private IOrderRepository _orderRepository;
        private IRestaurantRepository _restaurantRepository;
        private ILatLngRepository _latLngRepository;

        public OrderService(IMapper mapper, IOrderRepository orderRepository, IRestaurantRepository restaurantRepository, ILatLngRepository latLngRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _restaurantRepository = restaurantRepository;
            _latLngRepository = latLngRepository;
        }

        public async Task<CreatedDto> Create(CreateOrderDto createOrderDto)
        {
            var restaurant = await _restaurantRepository.GetById(createOrderDto.RestaurantId);

            if (restaurant == null)
            {
                throw new("Restaurant not found");
            }

            // Create an entity
            var latLng = _mapper.Map<LatLng>(createOrderDto.Destination);
            await _latLngRepository.Insert(latLng);

            var order = _mapper.Map<Order>(createOrderDto);

            // DestinationLatLng is ignored when mapping
            // Save entity with reference
            order.DestinationLatLngId = latLng.Id;
            await _orderRepository.Insert(order);

            // Save backing reference
            latLng.OrderId = order.Id;
            await _latLngRepository.Update(latLng);

            return new CreatedDto(order.Id);
        }

        public async Task<GetUnservedOrdersResultDto> GetUnserved(long restaurantId)
        {
            var restaurant = await _restaurantRepository.GetById(restaurantId);

            if (restaurant == null)
            {
                throw new("Restaurant not found");
            }

            var orders = await _orderRepository.GetUnserved(restaurantId);

            var unservedOrderDtos = _mapper.Map<ICollection<UnservedOrderDto>>(orders);

            return new GetUnservedOrdersResultDto() {Orders = unservedOrderDtos};
        }
    }
}