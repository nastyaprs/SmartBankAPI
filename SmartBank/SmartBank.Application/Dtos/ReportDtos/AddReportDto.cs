namespace SmartBank.BLL.Dtos.ReportDtos
{
    public class AddReportDto
    {
        public int UserId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
