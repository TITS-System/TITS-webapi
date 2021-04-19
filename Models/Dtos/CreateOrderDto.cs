namespace Models.Dtos
{
    public class CreateOrderDto
    {
        public long RestaurantId { get; set; }

        public string Content { get; set; }

        public string AddressString { get; set; }
        
        public string AddressAdditional { get; set; }

        public LatLngDto Destination { get; set; }

        public CreateOrderDto(long restaurantId, string content, string addressString, string addressAdditional, LatLngDto destination)
        {
            RestaurantId = restaurantId;
            Content = content;
            AddressString = addressString;
            AddressAdditional = addressAdditional;
            Destination = destination;
        }
    }
}