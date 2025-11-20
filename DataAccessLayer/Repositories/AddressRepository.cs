using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context; 

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetAllAddressesAsync(int userId)
        {
            var adress = await _context.Address
                                 .Where(a => a.UserId == userId)
                                 .ToListAsync();
            return adress;  
        }

        public async Task<Address> GetAddressByIdAsync(int id, int userId)
        {
            return await _context.Address
                                 .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
        }

        public async Task AddAddressAsync(Address address)
        {
             _context.Address.Add(address);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAddressAsync(Address address)
        {
            _context.Address.Update(address);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(int id, int userId)
        {
            var address = await GetAddressByIdAsync(id, userId);
            if (address != null)
            {
                _context.Address.Remove(address);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> AddressExistsAsync(int id, int userId)
        {
            return await _context.Address.AnyAsync(e => e.Id == id && e.UserId == userId);
        }
    }
}
