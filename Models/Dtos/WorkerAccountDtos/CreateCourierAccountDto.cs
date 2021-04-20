namespace Models.DTOs.WorkerAccountDtos
{
    public class CreateCourierAccountDto
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public long RestaurantId { get; set; }
    }
}