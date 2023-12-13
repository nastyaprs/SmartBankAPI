namespace SmartBank.DAL.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public DateTime DateIn { get; set; }
        public bool IsTransfer { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public int CardId { get; set; }
        public Card Card { get; set; } = null!;
    }
}
