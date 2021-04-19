namespace Models.Dtos
{
    public class UnservedOrderDto
    {
        public long Id { get; set; }

        public string Content { get; set; }

        public string AddressString { get; set; }

        public string AddressAdditional { get; set; }
    }
}