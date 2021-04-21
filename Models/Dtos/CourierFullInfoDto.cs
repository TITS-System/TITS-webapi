namespace Models.Dtos
{
    public class CourierFullInfoDto
    {
        public long Id { get; set; }
        
        public string Login { get; set; }

        public string Username { get; set; }

        public bool IsOnWork { get; set; }
    }
}