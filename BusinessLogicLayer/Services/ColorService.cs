using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _repository;

        public ColorService(IColorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ColorDto>> GetAllAsync()
        {
            var colors = await _repository.GetAllAsync();
            return colors.Select(c => new ColorDto
            {
                Id = c.Id,
                ColorName = c.ColorName
            });
        }

        public async Task<ColorDto> GetByIdAsync(int id)
        {
            var color = await _repository.GetByIdAsync(id);
            if (color == null) return null;

            return new ColorDto
            {
                Id = color.Id,
                ColorName = color.ColorName
            };
        }

        public async Task CreateAsync(CreateColorDto dto)
        {
            var color = new Color
            {
                ColorName = dto.ColorName
            };

            await _repository.AddAsync(color);
        }

        public async Task UpdateAsync( UpdateColorDto dto)
        {
            var color = await _repository.GetByIdAsync(dto.Id);
            

            color.ColorName = dto.ColorName;
            await _repository.UpdateAsync(color);
        }

        public async Task DeleteAsync(int id)
        {
          

            await _repository.DeleteAsync(id);
        }
    }

}
