using System;

namespace Models.Dtos
{
    public class GetByCourierAndDateDto
    {
        public long CourierId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public GetByCourierAndDateDto(long courierId, DateTime startTime, DateTime endTime)
        {
            CourierId = courierId;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}