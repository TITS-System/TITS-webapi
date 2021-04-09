namespace Models.DTOs.Misc
{
    public class TimeDto
    {
        public long Time { get; set; }

        public TimeDto(long time)
        {
            Time = time;
        }
    }
}