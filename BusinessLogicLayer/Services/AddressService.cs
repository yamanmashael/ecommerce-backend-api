using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class AddressService : IAddressService
    {


        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<IEnumerable<AddressDto>> GetAllAddressesAsync(int userId)
        {
            var addresses = await _addressRepository.GetAllAddressesAsync(userId);
            if (addresses == null)
            {
                return null;
            }
            return addresses.Select(a => new AddressDto
            {
                Id = a.Id,
                FullName = a.FulName, 
                PhoneNumber = a.PhoneNumber,
                City = a.City,
                FullAddress = a.FullAddress
               
            });
        }

        public async Task<AddressDto> GetAddressByIdAsync(int id, int userId)
        {
            var address = await _addressRepository.GetAddressByIdAsync(id, userId);
            if (address == null)
            {
                return null;
            }

            return new AddressDto
            {
                Id = address.Id,
                FullName = address.FulName, 
                PhoneNumber = address.PhoneNumber,
                City = address.City,
                FullAddress = address.FullAddress
            };
        }

        public async Task<bool> CreateAddressAsync(int userId, CreateAddressDto createAddressDto)
        {
            var address = new Address
            {
                UserId = userId,
                FulName = createAddressDto.FullName, 
                PhoneNumber = createAddressDto.PhoneNumber,
                City = createAddressDto.City,
                FullAddress = createAddressDto.FullAddress
            };

            await _addressRepository.AddAddressAsync(address);

            return true;
        }

        public async Task<bool> UpdateAddressAsync(int userId, UpdateAddressDto updateAddressDto)
        {
            var existingAddress = await _addressRepository.GetAddressByIdAsync(updateAddressDto.Id, userId);
            if (existingAddress == null)
            {
                return false;
            }

            existingAddress.FulName = updateAddressDto.FullName; 
            existingAddress.PhoneNumber = updateAddressDto.PhoneNumber;
            existingAddress.City = updateAddressDto.City;
            existingAddress.FullAddress = updateAddressDto.FullAddress;

            await _addressRepository.UpdateAddressAsync(existingAddress);
            return true;
        }

        public async Task<bool> DeleteAddressAsync(int id, int userId)
        {
            var addressExists = await _addressRepository.AddressExistsAsync(id, userId);
            if (!addressExists)
            {
                return false;
            }

            await _addressRepository.DeleteAddressAsync(id, userId);
            return true;
        }


    }
}
