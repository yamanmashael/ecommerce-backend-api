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
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;
        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {

            var brands = await _context.Brand.ToListAsync();
            return brands;
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            return await _context.Brand.FindAsync(id);
        }

        public async Task AddAsync(Brand brand)
        {
            _context.Brand.Add(brand);
            await _context.SaveChangesAsync();
           
        }

        public async Task UpdateAsync(Brand brand)
        {
            _context.Brand.Update(brand);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Brand brand)
        {
            _context.Brand.Remove(brand);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Brand.CountAsync();   
        }

        public async Task<int> SerchCountAsync(string? search)
        {
            var query = _context.Brand.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b => b.BarndName.Contains(search));
            }

            return await query.CountAsync();
        }

    
    }
}
