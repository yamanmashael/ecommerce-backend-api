using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface ICategoryItemRepository
    {
        Task<IEnumerable<CategoryItem>> GetAllAsync();
        Task<IEnumerable<CategoryItem>> GetCategoryItemsByCategory(int categoryId);

        Task<CategoryItem> GetByIdAsync(int id);
        Task AddAsync(CategoryItem item);
        Task UpdateAsync(CategoryItem item);
        Task DeleteAsync(CategoryItem item);
    }

}
