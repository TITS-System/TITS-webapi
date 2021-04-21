namespace Models.Dtos
{
    public class OrderDto
    {
        public string Content { get; set; }

        public string AddressString { get; set; }
        
        public string AddressAdditional { get; set; }

        public LatLngDto Destination { get; set; }
    }
}