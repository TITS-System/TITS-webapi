namespace Models.DTOs
{
    public class BeginWorkSessionResultDto
    {
        public long WorkSessionId { get; set; }

        public BeginWorkSessionResultDto(long workSessionId)
        {
            WorkSessionId = workSessionId;
        }
    }
}