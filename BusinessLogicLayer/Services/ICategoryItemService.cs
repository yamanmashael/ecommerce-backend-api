using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface ICategoryItemService
    {
        Task<IEnumerable<CategoryItemDto>> GetAllAsync();

        Task<IEnumerable<CategoryItemDto>> GetCategoryItemsByCategory(int categoryId);
        Task<CategoryItemDto> GetByIdAsync(int id);
        Task CreateAsync(CreateCategoryItemDto dto);
        Task UpdateAsync(int id, UpdateCategoryItemDto dto);
        Task DeleteAsync(int id);
    }

}
