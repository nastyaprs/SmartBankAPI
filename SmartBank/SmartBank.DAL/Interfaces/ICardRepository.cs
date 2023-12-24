using SmartBank.DAL.Models;

namespace SmartBank.DAL.Interfaces
{
    public interface ICardRepository
    {
        Card AddCard(Card card);
        bool CardNumberExists(string number);
        Card GetCardById(int id);
    }
}
