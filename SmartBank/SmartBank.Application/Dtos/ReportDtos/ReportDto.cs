namespace SmartBank.BLL.Dtos.ReportDtos
{
    public class ReportDto
    {
        public int Id { get; set; }
        public DateTime DateIn { get; set; }
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
