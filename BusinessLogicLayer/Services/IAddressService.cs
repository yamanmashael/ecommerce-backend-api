using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDto>> GetAllAddressesAsync(int userId);
        Task<AddressDto> GetAddressByIdAsync(int id, int userId);
        Task<bool> CreateAddressAsync(int userId, CreateAddressDto createAddressDto);
        Task<bool> UpdateAddressAsync(int userId, UpdateAddressDto updateAddressDto);
        Task<bool> DeleteAddressAsync(int id, int userId);
    }
}
