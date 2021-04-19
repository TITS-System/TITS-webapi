namespace Models.Dtos
{
    public class ChangeAccountDataDto
    {
        public long WorkerId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }
    }
}