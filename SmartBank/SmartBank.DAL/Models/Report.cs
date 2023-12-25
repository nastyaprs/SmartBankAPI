namespace SmartBank.DAL.Models
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime DateIn { get; set; }
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
