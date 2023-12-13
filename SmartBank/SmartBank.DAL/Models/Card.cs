namespace SmartBank.DAL.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;
        public DateTime DateIn { get; set; }
        public ICollection<Expense> Expense { get; set; } = new List<Expense>();
    }
}
