namespace Models.Dtos
{
    public class CreateOrderDto
    {
        public long RestaurantId { get; set; }

        public string Content { get; set; }

        public LatLngDto Destination { get; set; }

        public CreateOrderDto(long restaurantId, string content, LatLngDto destination)
        {
            RestaurantId = restaurantId;
            Content = content;
            Destination = destination;
        }
    }
}