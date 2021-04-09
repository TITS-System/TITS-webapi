namespace Models.Db
{
    public class Address
    {
        public long Id { get; set; }

        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public long House { get; set; }
        public long Building { get; set; }

        public string ZipCode { get; set; }
    }
}