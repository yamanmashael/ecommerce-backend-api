using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface ISizeService
    {
        Task<IEnumerable<SizeDto>> GetAllAsync();
        Task<SizeDto> GetByIdAsync(int id);
        Task CreateAsync(CreateSizeDto dto);
        Task UpdateAsync(UpdateSizeDto dto);
        Task DeleteAsync(int id);
    }

}
