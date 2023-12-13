using SmartBank.DAL.Models;

namespace SmartBank.BLL.Dtos.UserDtos
{
    public class NewUserDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FathersName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string PassportNumber { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = string.Empty;
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
    }
}
