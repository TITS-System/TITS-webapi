namespace Models.DTOs.Misc
{
    public class TokenDto
    {
        public TokenDto(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}