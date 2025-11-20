using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public  interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();

        Task<IEnumerable<CategoryDto>> GetCategoryByGender(int genderId);
        Task<CategoryDto> GetByIdAsync(int id);

        Task CreateAsync(CreateCategoryDto dto);
        Task UpdateAsync(int id, UpdateCategoryDto dto);
        Task DeleteAsync(int id);


    }
}
