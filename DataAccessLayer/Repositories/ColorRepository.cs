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
    public class ColorRepository : IColorRepository
    {
        private readonly ApplicationDbContext _context;

        public ColorRepository(ApplicationDbContext context)
        {
            _context = context;
        }   

        public async Task<IEnumerable<Color>> GetAllAsync()
        {
            return await _context.Color.ToListAsync();
        }

        public async Task<Color> GetByIdAsync(int id)
        {
            return await _context.Color.FindAsync(id);
        }

        public async Task AddAsync(Color color)
        {
            _context.Color.Add(color);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Color color)
        {
            _context.Color.Update(color);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var color =await _context.Color.FindAsync(id);  
            _context.Color.Remove(color);
            await _context.SaveChangesAsync();
        }
    }


}

