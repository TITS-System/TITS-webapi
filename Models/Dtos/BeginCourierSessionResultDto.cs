namespace Models.DTOs
{
    public class BeginCourierSessionResultDto
    {
        public long WorkSessionId { get; set; }

        public BeginCourierSessionResultDto(long workSessionId)
        {
            WorkSessionId = workSessionId;
        }
    }
}