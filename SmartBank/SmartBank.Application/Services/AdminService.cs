using SmartBank.BLL.Dtos.AddressDtos;
using SmartBank.BLL.Dtos.UserDtos;
using SmartBank.BLL.Interfaces;
using SmartBank.BLL.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.BLL.Services
{
    public class AdminService: IAdminService
    {
        private readonly IUserRepository _userRepository;

        public AdminService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

            _userRepository.SaveChanges();
        }
    }
}
