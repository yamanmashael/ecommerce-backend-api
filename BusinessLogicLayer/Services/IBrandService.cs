using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public  interface IBrandService
    {
        //Task<IEnumerable<BrandDto>> GetAllAsync(RequestModel requestModel);
        Task<IEnumerable<BrandDto>> GetAllAsync();
        Task<BrandDto> GetByIdAsync(int id);
        Task CreateAsync(CreateBrandDto createBrandDto);   
        Task UpdateAsync(int id, UpdateBrandDto updateBrandDto);
        Task DeleteAsync(int id);
        Task<int> SerchCountAsync(string? search);

        Task<int> CountAsync();
    }
}
