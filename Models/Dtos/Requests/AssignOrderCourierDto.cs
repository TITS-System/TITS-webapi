namespace Models.Dtos.Requests
{
    public class AssignOrderCourierDto
    {
        public long OrderId { get; set; }

        public long CourierId { get; set; }
    }
}