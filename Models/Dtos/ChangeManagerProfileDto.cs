#nullable enable
namespace Models.Dtos
{
    public class ChangeManagerProfileDto
    {
        public long ManagerId { get; set; }

        public string Login { get; set; }
        
        public string Password { get; set; }
        
        public string Username { get; set; }
    }
}