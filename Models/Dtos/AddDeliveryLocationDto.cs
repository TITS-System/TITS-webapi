namespace Models.Dtos
{
    public class AddDeliveryLocationDto
    {
        public long DeliveryId { get; set; }

        public LatLngDto LatLngDto { get; set; }

        public AddDeliveryLocationDto(long deliveryId, LatLngDto latLngDto)
        {
            DeliveryId = deliveryId;
            LatLngDto = latLngDto;
        }
    }
}