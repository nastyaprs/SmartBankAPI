﻿using SmartBank.BLL.Dtos.CategoryDtos;
using SmartBank.BLL.Dtos.UserDtos;
using SmartBank.DAL.Models;
using System.Security.Claims;

namespace SmartBank.BLL.Interfaces
{
    public interface IUserService
    {
        Task Register(NewUserDto newUserDto);
        bool UserExists(string email);
        string Login(UserLoginDto userLoginDto);
        FullUserDto GetUserProfile(ClaimsIdentity identity);
        List<CategoryDto> GetCategoriesForUser(int userId);
        void AddNewUsersCategory(int userId, string categoryName);
        void CreateNewAccountWithCard(int userId, int currencyId);
        bool AddExpense(int categoryId, int accountId, decimal money);
        User GetUserByToken(ClaimsIdentity identity);
    }
}
