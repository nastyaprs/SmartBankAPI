using Microsoft.EntityFrameworkCore;
using SmartBank.DAL.Data;
using SmartBank.DAL.Interfaces;
using SmartBank.DAL.Models;

namespace SmartBank.DAL.Repositories
{
    public class CardRepository: ICardRepository
    {
        private readonly SmartBankDBContext _smartBankDBContext;

        public CardRepository(SmartBankDBContext smartBankDBContext)
        {
            _smartBankDBContext = smartBankDBContext;
        }

        public Card AddCard (Card card)
        {
            _smartBankDBContext.Card.Add(card);
            _smartBankDBContext.SaveChanges();

            return card;
        }

        public bool CardNumberExists(string number)
        {
            return _smartBankDBContext.Card.Any(c => c.Number == number);
        }

        public Card GetCardById(int id)
        {
            return _smartBankDBContext.Card
                .Include(c=> c.Account)
                .Include(c=> c.Expense)
                .First(c => c.Id == id);
        }
    }
}
