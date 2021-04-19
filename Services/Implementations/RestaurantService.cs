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
        private IWorkerAccountRepository _workerAccountRepository;
        private IMapper _mapper;

        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper, IWorkerAccountRepository workerAccountRepository)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _workerAccountRepository = workerAccountRepository;
        }

        public async Task<GetCouriersResultDto> GetCouriers(long restaurantId)
        {
            var restaurant = await _restaurantRepository.GetById(restaurantId);

            if (restaurant == null)
            {
                throw new("Restaurant not found");
            }

            var couriers = await _workerAccountRepository.GetByRestaurant(restaurantId);

            var workerAccountDtos = _mapper.Map<ICollection<WorkerAccountDto>>(couriers);
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