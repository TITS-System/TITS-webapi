using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.Verbatims;
using Models.Db;
using Models.Dtos;
using Models.Enums;
using Services.Abstractions;

namespace Services.Implementations
{
    public class StatsService : IStatsService
    {
        private ICourierAccountRepository _courierAccountRepository;
        private IDeliveryRepository _deliveryRepository;
        private IOrderRepository _orderRepository;
        private ILatLngRepository _latLngRepository;

        private IMapper _mapper;

        public StatsService(ICourierAccountRepository courierAccountRepository, IDeliveryRepository deliveryRepository, IOrderRepository orderRepository, ILatLngRepository latLngRepository, IMapper mapper)
        {
            _courierAccountRepository = courierAccountRepository;
            _deliveryRepository = deliveryRepository;
            _orderRepository = orderRepository;
            _latLngRepository = latLngRepository;
            _mapper = mapper;
        }

        public async Task<StatsDto> BuildStats(long courierId)
        {
            var courierAccount = await _courierAccountRepository.GetById(courierId);

            if (courierAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            var paths = new List<ICollection<LatLngDto>>();

            var deliveries = await _deliveryRepository.GetByCourierIdAndDate(courierId, DateTime.Today, DateTime.Today.AddDays(1));

            var finishedDeliveriesCount = deliveries.Count(d => d.Status == DeliveryStatus.Finished);
            var canceledDeliveriesCount = deliveries.Count(d => d.Status == DeliveryStatus.Canceled);

            var totalDeliveryDistance = 0f;
            var averageDistance = 0f;

            var deliveryDistances = new List<float>();

            foreach (var delivery in deliveries)
            {
                var latLngs = await _latLngRepository.GetAllByDelivery(delivery.Id);

                latLngs = latLngs.ToList();

                var deliveryDistance = 0f;

                for (int i = 0; i < latLngs.Count - 1; i++)
                {
                    deliveryDistance += GetDistance(latLngs.ElementAt(i), latLngs.ElementAt(i + 1));
                }

                var latLngDtos = _mapper.Map<ICollection<LatLngDto>>(latLngs);

                paths.Add(latLngDtos);
                deliveryDistances.Add(deliveryDistance);
            }

            if (deliveryDistances.Count > 0)
            {
                totalDeliveryDistance = deliveryDistances.Sum();
                averageDistance = deliveryDistances.Average();
            }

            var canceledTimes = deliveries
                .Where(d => d.Status == DeliveryStatus.Canceled)
                .Select(d => d.EndTime.Value - d.StartTime)
                .Select(ts => (float)ts.TotalSeconds);

            var finishedTimes = deliveries
                .Where(d => d.Status == DeliveryStatus.Finished)
                .Select(d => d.EndTime.Value - d.StartTime)
                .Select(ts => (float)ts.TotalSeconds);

            var inProgressTimes = deliveries
                .Where(d => d.Status == DeliveryStatus.InProgress)
                .Select(d => DateTime.Now - d.StartTime)
                .Select(ts => (float)ts.TotalSeconds);

            var averageDeliveryTime = 0f;
            var totalDeliveryTime = 0f;
            var averageSpeed = 0f;
            if (inProgressTimes.Any() || canceledTimes.Any() || finishedTimes.Any())
            {
                averageDeliveryTime += inProgressTimes.Concat(canceledTimes).Concat(finishedTimes).Average();

                totalDeliveryTime += inProgressTimes.Concat(canceledTimes).Concat(finishedTimes).Sum();
            }

            if (totalDeliveryTime != 0)
            {
                averageSpeed = totalDeliveryDistance / totalDeliveryTime;
            }

            var statDto = new StatsDto()
            {
                AverageSpeed = averageSpeed,
                AverageDeliveryTime = averageDeliveryTime,
                AverageDistance = averageDistance,
                Paths = paths,
                TotalDistance = totalDeliveryDistance,
                CanceledDeliveriesCount = canceledDeliveriesCount,
                FinishedDeliveriesCount = finishedDeliveriesCount
            };

            return statDto;
        }

        private float GetDistance(LatLng ll1, LatLng ll2)
        {
            float earthRadius = 3958.75f;

            float latDiff = DegreeToRadian(ll2.Lat - ll1.Lat);
            float lngDiff = DegreeToRadian(ll2.Lng - ll1.Lng);
            float a = MathF.Sin(latDiff / 2) * MathF.Sin(latDiff / 2) +
                      MathF.Cos(DegreeToRadian(ll1.Lat)) * MathF.Cos(DegreeToRadian(ll2.Lat)) *
                      MathF.Sin(lngDiff / 2) * MathF.Sin(lngDiff / 2);
            float c = 2 * MathF.Atan2(MathF.Sqrt(a), MathF.Sqrt(1 - a));
            float distance = earthRadius * c;

            int meterConversion = 1609;

            return distance * meterConversion;
        }

        private float RadianToDegree(float angle)
        {
            return angle * (180.0f / MathF.PI);
        }

        private float DegreeToRadian(float angle)
        {
            return MathF.PI * angle / 180.0f;
        }
    }
}