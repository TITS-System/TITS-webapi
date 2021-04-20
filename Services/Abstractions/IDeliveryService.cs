using System;
using System.Threading.Tasks;
using Models.Dtos;

namespace Services.Abstractions
{
    public interface IDeliveryService
    {
        Task BeginDelivery(BeginDeliveryDto beginDeliveryDto);

        Task<LatLngsDto> GetDeliveryLocations(long deliveryId);
        
        Task AddDeliveryLocation(AddDeliveryLocationDto addDeliveryLocationDto);
        
        Task FinishDelivery(long deliveryId);
        
        Task CancelDelivery(long deliveryId);

        Task<DeliveriesDto> GetByCourierAndDate(GetByCourierAndDateDto getByCourierAndDateDto);
        
        // TODO: Get Courier Open Deliveries
    }
}