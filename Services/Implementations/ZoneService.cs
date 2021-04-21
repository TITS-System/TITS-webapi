using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Models.Db;
using Models.Dtos;
using Services.Abstractions;

namespace Services.Implementations
{
    public class ZoneService : IZoneService
    {
        private IRestaurantRepository _restaurantRepository;
        private ILatLngRepository _latLngRepository;
        private IZoneRepository _zoneRepository;
        private IMapper _mapper;

        public ZoneService(IRestaurantRepository restaurantRepository, IMapper mapper, ILatLngRepository latLngRepository, IZoneRepository zoneRepository)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _latLngRepository = latLngRepository;
            _zoneRepository = zoneRepository;
        }

        public async Task<LatLngsDto> GetRestaurantZone(long restaurantId)
        {
            var restaurant = await _restaurantRepository.GetById(restaurantId);
            
            if (restaurant == null)
            {
                throw new("Restaurant not found");
            }

            // ReSharper disable once PossibleInvalidOperationException
            var latLngs = await _latLngRepository.GetAllByZone(restaurant.ZoneId.Value);

            var latLngDtos = _mapper.Map<ICollection<LatLngDto>>(latLngs);

            return new LatLngsDto(latLngDtos);
        }

        public async Task SetRestaurantZone(SetRestaurantZoneDto setRestaurantZoneDto)
        {
            var restaurant = await _restaurantRepository.GetById(setRestaurantZoneDto.RestaurantId);
            
            if (restaurant == null)
            {
                throw new("Restaurant not found");
            }

            // Unbind Current Zone 
            
            // ReSharper disable once PossibleInvalidOperationException
            var currentZone = await _zoneRepository.GetById(restaurant.ZoneId.Value);

            currentZone.RestaurantId = null;

            await _zoneRepository.Update(currentZone);
            
            // Create New Zone

            Zone zone = new Zone()
            {
                RestaurantId = restaurant.Id
            };

            await _zoneRepository.Insert(zone);

            var latLngs = _mapper.Map<ICollection<LatLng>>(setRestaurantZoneDto.LatLngs);
            
            // Bind all latlngs
            foreach (var latLng in latLngs)
            {
                latLng.ZoneId = zone.Id;
            }
            
            await _latLngRepository.Insert(latLngs);

            // Bind restaurant to zone
            restaurant.ZoneId = zone.Id;
            await _restaurantRepository.Update(restaurant);
        }
    }
}