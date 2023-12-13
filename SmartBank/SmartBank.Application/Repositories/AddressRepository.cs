using SmartBank.BLL.Interfaces.IRepositories;
using SmartBank.DAL.Data;
using SmartBank.DAL.Models;

namespace SmartBank.BLL.Repositories
{
    public class AddressRepository: IAddressRepository
    {
        private readonly SmartBankDBContext _smartBankDBContext;

        public AddressRepository(SmartBankDBContext smartBankDBContext)
        {
            _smartBankDBContext = smartBankDBContext;
        }

        public async Task<Address> AddAddress(Address address)
        {
            await _smartBankDBContext.Address.AddAsync(address);
            await _smartBankDBContext.SaveChangesAsync();
            return address;
        }

        public async Task ConnectAddressToUser(User user, Address address)
        {
            address.User = user;
            await _smartBankDBContext.SaveChangesAsync();
        }
    }
}
