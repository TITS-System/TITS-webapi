namespace Models.Dtos
{
    public class DeliveryDto
    {
        public long Id { get; set; }
        
        public long OrderId { get; set; }

        public long CourierId { get; set; }
        
        public string StartTime { get; set; }
        
        public string EndTime { get; set; }
    }
}