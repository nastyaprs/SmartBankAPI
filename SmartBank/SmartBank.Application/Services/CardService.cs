using SmartBank.BLL.Interfaces;
using SmartBank.DAL.Interfaces;
using SmartBank.DAL.Models;

namespace SmartBank.BLL.Services
{
    public class CardService: ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public Card CreateNewCard(Account account)
        {
            var card = new Card
            {
                Number = GenerateNewCardNumber(),
                AccountId = account.Id,
                Account = account,
                DateIn = DateTime.Now
            };

            return _cardRepository.AddCard(card);
        }

        private string GenerateNewCardNumber()
        {
            var number = Guid.NewGuid().ToString();

            while (_cardRepository.CardNumberExists(number))
            {
                number = Guid.NewGuid().ToString();
            }

            return number;
        }
    }
}
