using SmartBank.DAL.Models;

namespace SmartBank.BLL.Interfaces
{
    public interface ICardService
    {
        Card CreateNewCard(Account account);
    }
}
