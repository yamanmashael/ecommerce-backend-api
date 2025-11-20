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
    public class GenderRepository : IGenderRepository
    {
        private readonly ApplicationDbContext _context;

        public GenderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Gender>> GetAllAsync()
        {
            return await _context.Gender.ToListAsync();
        }

        public async Task<Gender> GetByIdAsync(int id)
        {
            return await _context.Gender.FindAsync(id);
        }

        public async Task AddAsync(Gender gender)
        {
            _context.Gender.Add(gender);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Gender gender)
        {
            _context.Gender.Update(gender);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Gender gender)
        {
            _context.Gender.Remove(gender);
            await _context.SaveChangesAsync();
        }
    }

}
