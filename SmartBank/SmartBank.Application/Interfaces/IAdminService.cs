using SmartBank.BLL.Dtos.UserDtos;

namespace SmartBank.BLL.Interfaces
{
    public interface IAdminService
    {
        List<FullUserDto> GetUnverifiedUsers();
        void VerifyUser(int userId);
    }
}
