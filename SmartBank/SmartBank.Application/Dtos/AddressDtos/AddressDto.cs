using SmartBank.DAL.Models;

namespace SmartBank.BLL.Dtos.AddressDtos
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
    }
}
