#nullable enable
namespace Models.Dtos
{
    public class ChangeCourierProfileDto
    {
        public long CourierId { get; set; }

        public string Login { get; set; }
        
        public string Password { get; set; }
        
        public string Username { get; set; }
    }
}