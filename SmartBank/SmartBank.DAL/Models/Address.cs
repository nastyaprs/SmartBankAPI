namespace SmartBank.DAL.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
        public User? User { get; set; }
    }
}
