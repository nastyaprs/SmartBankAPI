using SmartBank.BLL.Dtos.UserDtos;

namespace SmartBank.BLL.Interfaces
{
    public interface IUserService
    {
        Task Register(NewUserDto newUserDto);
        bool UserExists(string email);
        string Login(UserLoginDto userLoginDto);
    }
}
