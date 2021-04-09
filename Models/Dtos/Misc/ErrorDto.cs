namespace Models.DTOs.Misc
{
    public class ErrorDto
    {
        public ErrorDto(string error)
        {
            Error = error;
        }

        public string Error { get; set; }
    }
}