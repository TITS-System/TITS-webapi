using System;

namespace Models.Dtos
{
    public class CourierMessageDto
    {
        public DateTime CreationDateTime { get; set; }
        public string Content { get; set; }
        public bool IsFromCourier { get; set; }
    }
}