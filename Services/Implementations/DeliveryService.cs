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
    public class DeliveryService : IDeliveryService
    {
        private IOrderRepository _orderRepository;
        private IDeliveryRepository _deliveryRepository;
        private IWorkerAccountRepository _workerAccountRepository;
        private ILatLngRepository _latLngRepository;

        private IMapper _mapper;

        public DeliveryService(IOrderRepository orderRepository, IDeliveryRepository deliveryRepository, IWorkerAccountRepository workerAccountRepository, ILatLngRepository latLngRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _deliveryRepository = deliveryRepository;
            _workerAccountRepository = workerAccountRepository;
            _latLngRepository = latLngRepository;
            _mapper = mapper;
        }

        public async Task BeginDelivery(BeginDeliveryDto beginDeliveryDto)
        {
            var order = await _orderRepository.GetById(beginDeliveryDto.OrderId);

            if (order == null)
            {
                throw new("Order not found");
            }

            var orderDeliveries = await _deliveryRepository.GetByOrderId(order.Id);

            if (orderDeliveries.Any(d => d.Status == DeliveryStatus.InProgress))
            {
                throw new("Delivery for this order is already in progress");
            }

            var workerAccount = await _workerAccountRepository.GetById(beginDeliveryDto.CourierId);

            if (workerAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            var delivery = new Delivery()
            {
                CourierAccount = workerAccount,
                Order = order,
                Status = DeliveryStatus.InProgress,
                StartTime = DateTime.Now
            };

            await _deliveryRepository.Insert(delivery);
        }

        public async Task<LatLngsDto> GetDeliveryLocations(long deliveryId)
        {
            var delivery = await _deliveryRepository.GetById(deliveryId);

            if (delivery == null)
            {
                throw new("Delivery no found");
            }

            var latLngs = await _latLngRepository.GetLocations(deliveryId);

            var latLngsDto = new LatLngsDto(_mapper.Map<ICollection<LatLngDto>>(latLngs));
            return latLngsDto;
        }

        public async Task AddDeliveryLocation(AddDeliveryLocationDto addDeliveryLocationDto)
        {
            var delivery = await _deliveryRepository.GetById(addDeliveryLocationDto.DeliveryId);

            if (delivery == null)
            {
                throw new("Delivery no found");
            }

            LatLng latLng = _mapper.Map<LatLng>(addDeliveryLocationDto.LatLngDto);
            latLng.DeliveryId = delivery.Id;
            await _latLngRepository.Insert(latLng);
        }

        public async Task FinishDelivery(long deliveryId)
        {
            var delivery = await _deliveryRepository.GetById(deliveryId);

            if (delivery == null)
            {
                throw new("Delivery no found");
            }

            delivery.Status = DeliveryStatus.Finished;
            delivery.EndTime = DateTime.Now;
            await _deliveryRepository.Update(delivery);
        }

        public async Task CancelDelivery(long deliveryId)
        {
            var delivery = await _deliveryRepository.GetById(deliveryId);

            if (delivery == null)
            {
                throw new("Delivery no found");
            }

            delivery.Status = DeliveryStatus.Canceled;
            delivery.EndTime = DateTime.Now;
            await _deliveryRepository.Update(delivery);
        }

        public async Task<DeliveriesDto> GetByCourierAndDate(GetByCourierAndDateDto getByCourierAndDateDto)
        {
            var workerAccount = await _workerAccountRepository.GetById(getByCourierAndDateDto.CourierId);

            if (workerAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            var deliveries = await _deliveryRepository.GetByCourierIdAndDate(
                getByCourierAndDateDto.CourierId,
                getByCourierAndDateDto.StartTime,
                getByCourierAndDateDto.EndTime
            );

            var deliveryDtos = deliveries.Select(d => new DeliveryDto(d.OrderId, d.CourierAccountId)).ToList();

            return new DeliveriesDto(deliveryDtos);
        }
    }
}