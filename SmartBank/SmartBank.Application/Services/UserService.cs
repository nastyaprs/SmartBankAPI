using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartBank.BLL.Dtos;
using SmartBank.BLL.Dtos.UserDtos;
using SmartBank.BLL.Helper.Constants;
using SmartBank.BLL.Interfaces.IRepositories;
using SmartBank.BLL.Interfaces.IServices;
using SmartBank.DAL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartBank.BLL.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IAddressRepository addressRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _configuration = configuration;
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
    }
}
