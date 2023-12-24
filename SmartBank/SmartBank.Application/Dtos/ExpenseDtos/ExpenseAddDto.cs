namespace SmartBank.BLL.Dtos.ExpenseDtos
{
    public class ExpenseAddDto
    {
        public int CategoryId { get; set; }
        public int CardId { get; set; }
        public decimal Money { get; set; }
    }
}
