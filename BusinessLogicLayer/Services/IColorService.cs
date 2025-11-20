using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IColorService
    {
        Task<IEnumerable<ColorDto>> GetAllAsync();
        Task<ColorDto> GetByIdAsync(int id);
        Task CreateAsync(CreateColorDto dto);
        Task UpdateAsync(UpdateColorDto dto);
        Task DeleteAsync(int id);
    }

}
