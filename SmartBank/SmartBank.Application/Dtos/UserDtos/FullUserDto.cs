using SmartBank.BLL.Dtos.AddressDtos;

namespace SmartBank.BLL.Dtos.UserDtos
{
    public class FullUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FathersName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string PassportNumber { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsVerified { get; set; }
        public AddressDto Address { get; set; } = null!;
        public DateTime DateIn { get; set; }
    }
}
