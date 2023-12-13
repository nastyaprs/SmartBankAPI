namespace SmartBank.DAL.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public bool IsDefault { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; } = null;
        public ICollection<Expense> Expense { get; set; } = new List<Expense>();
    }
}
