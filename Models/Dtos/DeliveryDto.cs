namespace Models.Dtos
{
    public class DeliveryDto
    {
        public long Id { get; set; }
        
        public long OrderId { get; set; }

        public long CourierId { get; set; }

        public DeliveryDto(long orderId, long courierId)
        {
            OrderId = orderId;
            CourierId = courierId;
        }
    }
}