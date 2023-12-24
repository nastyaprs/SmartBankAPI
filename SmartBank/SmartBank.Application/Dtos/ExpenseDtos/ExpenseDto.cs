namespace SmartBank.BLL.Dtos.ExpenseDtos
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public DateTime DateIn { get; set; }
        public decimal Money { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
