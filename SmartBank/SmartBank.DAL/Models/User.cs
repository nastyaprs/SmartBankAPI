namespace SmartBank.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FathersName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string PassportNumber { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        public int AddressId { get; set; } 
        public Address Address { get; set; } = null!;
        public DateTime DateIn { get; set; }
        public ICollection<Account> Account { get; set; } = new List<Account>();
        public ICollection<Category>? Category { get; set; } = new List<Category>();
        public ICollection<Report> Report { get; set; } = new List<Report>();
    }
}
