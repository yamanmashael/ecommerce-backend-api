using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAllAddressesAsync(int userId);
        Task<Address> GetAddressByIdAsync(int id, int userId);
        Task AddAddressAsync(Address address);
        Task UpdateAddressAsync(Address address);
        Task DeleteAddressAsync(int id, int userId);
        Task<bool> AddressExistsAsync(int id, int userId);
    }
}
