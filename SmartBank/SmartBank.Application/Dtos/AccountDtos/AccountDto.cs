namespace SmartBank.BLL.Dtos.AccountDtos
{
    public class AccountDto
    {
        public int Id { get; set; }
        public decimal AmountOfMoney { get; set; }
        public string Currency { get; set; } = null!;
        public DateTime DateIn { get; set; }
    }
}
