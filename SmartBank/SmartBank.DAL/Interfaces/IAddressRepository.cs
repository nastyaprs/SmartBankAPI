using SmartBank.DAL.Models;
namespace SmartBank.BLL.Interfaces.IRepositories
{
    public interface IAddressRepository
    {
        Task<Address> AddAddress(Address address);
        Task ConnectAddressToUser(User user, Address address);
    }
}
