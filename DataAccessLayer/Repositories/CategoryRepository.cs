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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Category.Include(c => c.Gender).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoryByGender(int genderId)
        {   
            return await _context.Category.Include(g => g.Gender).Where(x => x.GenderId == genderId).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Category.Include(c => c.Gender).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Category.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
        }

    
    }

}
