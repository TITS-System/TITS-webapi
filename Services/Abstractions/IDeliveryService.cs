using System;
using System.Threading.Tasks;
using Models.Dtos;
using Models.DTOs.Misc;

namespace Services.Abstractions
{
    public interface IDeliveryService
    {
        Task<CreatedDto> BeginDelivery(BeginDeliveryDto beginDeliveryDto);

        Task<LatLngsDto> GetDeliveryLocations(long deliveryId);
        
        Task AddDeliveryLocation(AddDeliveryLocationDto addDeliveryLocationDto);
        
        Task FinishDelivery(long deliveryId);
        
        Task CancelDelivery(long deliveryId);

        Task<DeliveriesDto> GetAllByCourierAndDate(GetByCourierAndDateDto getByCourierAndDateDto);
        
        Task<DeliveriesDto> GetInProgressByCourier(long courierId);


        // TODO: Get Courier Open Deliveries
    }
}