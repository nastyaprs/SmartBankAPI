namespace SmartBank.BLL.Dtos.ExpenseDtos
{
    public class ExpenseAddDto
    {
        public int CategoryId { get; set; }
        public int AccountId { get; set; }
        public decimal Money { get; set; }
    }
}
