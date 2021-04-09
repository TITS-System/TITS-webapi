namespace Models.DTOs.Requests
{
    public class CreateIngredientDto
    {
        public string Title { get; set; }

        public int Weight { get; set; }

        public long ProductId { get; set; }
    }
}