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
    public class CategoryItemRepository : ICategoryItemRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryItem>> GetAllAsync()
        {
            return await _context.CategoryItem.Include(c => c.Category).ToListAsync();
        }
        public async Task<IEnumerable<CategoryItem>> GetCategoryItemsByCategory(int categoryId)
        {
            return await _context.CategoryItem.Include(c => c.Category).Where(x=>x.CategoryId== categoryId).ToListAsync();
        }

        public async Task<CategoryItem> GetByIdAsync(int id)
        {
            return await _context.CategoryItem.Include(c => c.Category)
                                               .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(CategoryItem item)
        {
            _context.CategoryItem.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryItem item)
        {
            _context.CategoryItem.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CategoryItem item)
        {
            _context.CategoryItem.Remove(item);
            await _context.SaveChangesAsync();
        }


    }

}
