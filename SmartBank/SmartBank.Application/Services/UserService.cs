using BCrypt.Net;
using SmartBank.BLL.Dtos;
using SmartBank.BLL.Dtos.UserDtos;
using SmartBank.BLL.Interfaces.IRepositories;
using SmartBank.BLL.Interfaces.IServices;
using SmartBank.DAL.Models;

namespace SmartBank.BLL.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;

        public UserService(IUserRepository userRepository, IAddressRepository addressRepository)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;

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
                DateIn = DateTime.Now
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
    }
}
