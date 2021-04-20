using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Models.Dtos;
using Services.Abstractions;

namespace Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private IRestaurantRepository _restaurantRepository;
        private ICourierAccountRepository _courierAccountRepository;
        private IMapper _mapper;

        public RestaurantService(IRestaurantRepository restaurantRepository, ICourierAccountRepository courierAccountRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _courierAccountRepository = courierAccountRepository;
            _mapper = mapper;
        }

        public async Task<GetCouriersResultDto> GetCouriers(long restaurantId)
        {
            var restaurant = await _restaurantRepository.GetById(restaurantId);

            if (restaurant == null)
            {
                throw new("Restaurant not found");
            }

            var courierAccounts = await _courierAccountRepository.GetByRestaurant(restaurantId);

            var workerAccountDtos = _mapper.Map<ICollection<WorkerAccountDto>>(courierAccounts);
            return new GetCouriersResultDto(workerAccountDtos);
        }

        public async Task<RestaurantDto> GetInfo(long restaurantId)
        {
            var restaurant = await _restaurantRepository.GetById(restaurantId);
            
            if (restaurant == null)
            {
                throw new("Restaurant not found");
            }

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }
    }
}