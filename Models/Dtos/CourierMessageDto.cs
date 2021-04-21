namespace Models.Dtos
{
    public class CourierMessageDto
    {
        public string CreationDateTime { get; set; }
        public string Content { get; set; }
        public bool IsFromCourier { get; set; }
    }
}