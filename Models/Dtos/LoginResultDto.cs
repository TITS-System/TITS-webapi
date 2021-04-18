namespace Models.Dtos
{
    public class LoginResultDto
    {
        public long WorkerId { get; set; }

        public string AuthToken { get; set; }

        public LoginResultDto(long workerId, string authToken)
        {
            WorkerId = workerId;
            AuthToken = authToken;
        }
    }
}