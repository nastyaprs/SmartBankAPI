using SmartBank.BLL.Dtos.UserDtos;

namespace SmartBank.BLL.Interfaces.IServices
{
    public interface IUserService
    {
        Task Register(NewUserDto newUserDto);
        bool UserExists(string email);
    }
}
