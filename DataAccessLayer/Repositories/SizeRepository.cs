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
    public class SizeRepository : ISizeRepository
    {
        private readonly ApplicationDbContext _context;

        public SizeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Size>> GetAllAsync()
        {
            return await _context.Size.ToListAsync();
        }

        public async Task<Size> GetByIdAsync(int id)
        {
            return await _context.Size.FindAsync(id);
        }

        public async Task AddAsync(Size size)
        {
            _context.Size.Add(size);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Size size)
        {
            _context.Size.Update(size);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var size = await _context.Size.FindAsync(id);
            _context.Size.Remove(size);
            await _context.SaveChangesAsync();
        }
    }

}
