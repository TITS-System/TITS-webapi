namespace Models.DTOs.Requests
{
    public class LoginDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}