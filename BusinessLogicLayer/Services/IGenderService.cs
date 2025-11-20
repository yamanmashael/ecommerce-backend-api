using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IGenderService
    {
        Task<IEnumerable<GenderDto>> GetAllAsync();
        Task<GenderDto> GetByIdAsync(int id);
        Task CreateAsync(CreateGenderDto dto);
        Task UpdateAsync(int id, UpdateGenderDto dto);
        Task DeleteAsync(int id);
    }
}
