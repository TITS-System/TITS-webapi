namespace Models.DTOs.Misc
{
    public class CreatedDto
    {
        public long Id { get; set; }

        public CreatedDto(long id)
        {
            Id = id;
        }
    }
}