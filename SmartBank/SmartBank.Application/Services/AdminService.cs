using SmartBank.BLL.Dtos.AddressDtos;
using SmartBank.BLL.Dtos.UserDtos;
using SmartBank.BLL.Helper.Enums;
using SmartBank.BLL.Interfaces;
using SmartBank.BLL.Interfaces.IRepositories;
using SmartBank.DAL.Models;

namespace SmartBank.BLL.Services
{
    public class AdminService: IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountService _accountService;
        private readonly ICardService _cardService;

        public AdminService(IUserRepository userRepository, IAccountService accountService, ICardService cardService)
        {
            _accountService = accountService;
            _userRepository = userRepository;
            _cardService = cardService;
        }

        public List<FullUserDto> GetUnverifiedUsers()
        {
            var usersRawList = _userRepository.GetUnverifiedUsers();

            List<FullUserDto> usersList = new List<FullUserDto>();
            foreach(var user  in usersRawList)
            {
                var userDto = new FullUserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FathersName = user.FathersName,
                    DateOfBirth = user.DateOfBirth,
                    PassportNumber = user.PassportNumber,
                    Phone = user.Phone,
                    Email = user.Email,
                    IsVerified = user.IsVerified,
                    DateIn = user.DateIn,
                    Address = new AddressDto
                    {
                        Id = user.Address.Id,
                        Country = user.Address.Country,
                        City = user.Address.City,
                        AddressLine = user.Address.AddressLine
                    }
                };

                usersList.Add(userDto);
            }

            return usersList;
        }

        public void VerifyUser(int userId)
        {
            var user = _userRepository.GetUserById(userId);

            user.IsVerified = true;

            var account = _accountService.CreateNewAccount(nameof(CurrencyEnum.UAH), user);

            var card = _cardService.CreateNewCard(account);

            account.Card = card;

            user.Account = new List<Account>() { account };

            _userRepository.SaveChanges();
        }
    }
}
