namespace SmartBank.DAL.Models
{
    public class Account
    {
        public int Id { get; set; }
        public decimal AmountOfMoney { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public DateTime DateIn { get; set; }
        public Card? Card { get; set; }
    }
}
