﻿using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartBank.BLL.Dtos;
using SmartBank.BLL.Dtos.AddressDtos;
using SmartBank.BLL.Dtos.CategoryDtos;
using SmartBank.BLL.Dtos.UserDtos;
using SmartBank.BLL.Helper.Constants;
using SmartBank.BLL.Helper.Enums;
using SmartBank.BLL.Interfaces;
using SmartBank.BLL.Interfaces.IRepositories;
using SmartBank.DAL.Interfaces;
using SmartBank.DAL.Models;
using SmartBank.DAL.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace SmartBank.BLL.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IConfiguration _configuration;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccountService _accountService;
        private readonly ICardService _cardService;
        private readonly IExpenseRepository _expenseRepository;

        public UserService(IUserRepository userRepository, 
            IAddressRepository addressRepository, IConfiguration configuration, 
            ICategoryRepository categoryRepository, IAccountService accountService, 
            ICardService cardService, IExpenseRepository expenseRepository)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _configuration = configuration;
            _categoryRepository = categoryRepository;
            _accountService = accountService;
            _cardService = cardService;
            _expenseRepository = expenseRepository;
        }

        public async Task Register(NewUserDto newUserDto)
        {
            var address = new Address
            {
                Country = newUserDto.Country,
                City = newUserDto.City,
                AddressLine = newUserDto.AddressLine
            };

            var addedAddress = await _addressRepository.AddAddress(address);

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(newUserDto.Password);

            var user = new User
            {
                FirstName = newUserDto.FirstName,
                LastName = newUserDto.LastName,
                FathersName = newUserDto.FathersName,
                Phone = newUserDto.Phone,
                Email = newUserDto.Email,
                DateOfBirth = newUserDto.DateOfBirth,
                PassportNumber = newUserDto.PassportNumber,
                Address = addedAddress,
                AddressId = addedAddress.Id,
                PasswordHash = passwordHash,
                DateIn = DateTime.Now,
                Role = Roles.User
            };

            await _userRepository.AddUser(user);
        }

        public bool UserExists(string email)
        {
            var userExists = _userRepository.GetUserByEmail(email);

            if (userExists == null)
                return false;

            return true;
        }

        public string Login(UserLoginDto userLoginDto)
        {
            var user = _userRepository.GetUserByEmail(userLoginDto.Email);

            if (user == null)
                return ErrorMessages.UserNotFound;

            if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
                return ErrorMessages.WrongPassword;

            var token = GenerateToken(user);

            return token;

        }

        private string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.LastName} {user.FirstName} {user.FathersName}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public FullUserDto GetUserProfile(ClaimsIdentity identity)
        {
            var user = GetUserByToken(identity);

            return new FullUserDto
            {
                Id = user!.Id,
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
        }

        public List<CategoryDto> GetCategoriesForUser(int userId)
        {
            var defaultCategories = _categoryRepository.GetDefaultCategories();

            var usersCategories = _categoryRepository.GetUserCategories(userId);

            var categories = new List<CategoryDto>();
            foreach(var category in defaultCategories)
            {
                var dto = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.CategoryName
                };

                categories.Add(dto);
            };

            foreach (var category in usersCategories)
            {
                var dto = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.CategoryName
                };

                categories.Add(dto);
            };

            return categories;
        }

        public void AddNewUsersCategory(int  userId, string categoryName)
        {
            var user = _userRepository.GetUserById(userId);

            var category = new Category
            {
                CategoryName = categoryName,
                UserId = userId,
                User = user
            };

            _categoryRepository.AddCategory(category);
        }

        public void CreateNewAccountWithCard(int userId, int currencyId)
        {
            var user = _userRepository.GetUserById(userId);

            int currencyValue = currencyId = 1;

            CurrencyEnum currency = (CurrencyEnum)currencyId;

            var account = _accountService.CreateNewAccount(currency.ToString(), user);

            var card = _cardService.CreateNewCard(account);

            account.Card = card;

            user.Account = new List<Account>() { account };

            _userRepository.SaveChanges();
        }

        public bool AddExpense(int categoryId, int accountId, decimal money)
        {
            var account = _accountService.GetAccountDetails(accountId);

            var card = _cardService.GetCardById(account.CardId);

            if (card.Account.AmountOfMoney < money)
            {
                return false;
            }

            var expense = new Expense()
            {
                DateIn = DateTime.Now,
                CardId = card.Id,
                Card = card,
                Money = money
            };

            var category = _categoryRepository.GetCategoryById(categoryId);

            if (category != null)
            {
                expense.CategoryId = category.Id;
                expense.Category = category;
            }
            else
            {
                category = _categoryRepository.GetUndefinedCategory();

                expense.CategoryId = category.Id;
                expense.Category = category;
            }

            card.Account.AmountOfMoney -= money;

            expense = _expenseRepository.AddExpense(expense);

            card.Expense.Add(expense);

            _userRepository.SaveChanges();

            return true;
        }

        public User GetUserByToken(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var userEmail = claims.FirstOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", StringComparison.OrdinalIgnoreCase))?.Value;

            var user = _userRepository.GetUserByEmail(userEmail);

            return user;
        }
    }
}
