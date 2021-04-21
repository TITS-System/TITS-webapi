using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.Verbatims;
using Models.Db;
using Models.Dtos;
using Models.DTOs.Misc;
using Models.Enums;
using Services.Abstractions;

namespace Services.Implementations
{
    public class DeliveryService : IDeliveryService
    {
        private IOrderRepository _orderRepository;
        private IDeliveryRepository _deliveryRepository;
        private ICourierAccountRepository _courierAccountRepository;
        private ILatLngRepository _latLngRepository;

        private IMapper _mapper;

        public DeliveryService(IOrderRepository orderRepository, IDeliveryRepository deliveryRepository, ICourierAccountRepository courierAccountRepository, ILatLngRepository latLngRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _deliveryRepository = deliveryRepository;
            _courierAccountRepository = courierAccountRepository;
            _latLngRepository = latLngRepository;
            _mapper = mapper;
        }
        
        public async Task<CreatedDto> BeginDelivery(BeginDeliveryDto beginDeliveryDto)
        {
            var order = await _orderRepository.GetById(beginDeliveryDto.OrderId);

            if (order == null)
            {
                throw new("Order not found");
            }

            var orderDeliveries = await _deliveryRepository.GetByOrderId(order.Id);

            if (orderDeliveries.Any(d => d.Status == DeliveryStatus.InProgress || d.Status == DeliveryStatus.Finished))
            {
                throw new("Delivery for this order is already in progress or finished");
            }

            var courierAccount = await _courierAccountRepository.GetById(beginDeliveryDto.CourierId);

            if (courierAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            var delivery = new Delivery()
            {
                CourierAccount = courierAccount,
                Order = order,
                Status = DeliveryStatus.InProgress,
                StartTime = DateTime.Now
            };

            await _deliveryRepository.Insert(delivery);
            
            // TODO: Notify

            return new CreatedDto(delivery.Id);
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
            
            // It can't be null, it would be a system data consistency error

            LatLng latLng = _mapper.Map<LatLng>(addDeliveryLocationDto.LatLngDto);
            latLng.DeliveryId = delivery.Id;
            await _latLngRepository.Insert(latLng);

            // Save courier last location
            var courierAccount = await _courierAccountRepository.GetById(delivery.CourierAccountId);
            courierAccount.LastLatLngId = latLng.Id;
            await _courierAccountRepository.Update(courierAccount);
        }

        public async Task FinishDelivery(long deliveryId)
        {
            var delivery = await _deliveryRepository.GetById(deliveryId);

            if (delivery == null)
            {
                throw new("Delivery no found");
            }

            if (delivery.Status == DeliveryStatus.Finished)
            {
                throw new("Delivery is already finished");
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

            if (delivery.Status == DeliveryStatus.Canceled)
            {
                throw new("Delivery is already canceled");
            }

            delivery.Status = DeliveryStatus.Canceled;
            delivery.EndTime = DateTime.Now;
            await _deliveryRepository.Update(delivery);
        }

        public async Task<DeliveriesDto> GetAllByCourierAndDate(GetByCourierAndDateDto getByCourierAndDateDto)
        {
            var courierAccount = await _courierAccountRepository.GetById(getByCourierAndDateDto.CourierId);

            if (courierAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            var deliveries = await _deliveryRepository.GetByCourierIdAndDate(
                getByCourierAndDateDto.CourierId,
                getByCourierAndDateDto.StartTime,
                getByCourierAndDateDto.EndTime
            );

            var deliveryDtos = _mapper.Map<ICollection<DeliveryDto>>(deliveries);

            return new DeliveriesDto(deliveryDtos);
        }

        public async Task<DeliveriesDto> GetInProgressByCourier(long courierId)
        {
            var courierAccount = await _courierAccountRepository.GetById(courierId);

            if (courierAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            var deliveries = await _deliveryRepository.GetInProgressByCourier(courierId);

            var deliveryDtos = _mapper.Map<ICollection<DeliveryDto>>(deliveries);

            return new DeliveriesDto(deliveryDtos);
        }
    }
}