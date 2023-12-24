using SmartBank.BLL.Dtos.ExpenseDtos;

namespace SmartBank.BLL.Dtos.AccountDtos
{
    public class AccountDetailsDto
    {
        public int Id { get; set; }
        public decimal AmountOfMoney { get; set; }
        public string Currency { get; set; } = null!;
        public DateTime DateIn { get; set; }
        public string CardNumber { get; set; } = null!;
        public List<ExpenseDto> Expenses { get; set; } = new List<ExpenseDto>();
        public int? UserId { get; set; }
    }
}
