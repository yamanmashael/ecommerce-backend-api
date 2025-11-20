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
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _repository;

        public SizeService(ISizeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SizeDto>> GetAllAsync()
        {
            var sizes = await _repository.GetAllAsync();
            return sizes.Select(s => new SizeDto
            {
                Id = s.Id,
                SizeNmae = s.SizeNmae
            });
        }

        public async Task<SizeDto> GetByIdAsync(int id)
        {
            var size = await _repository.GetByIdAsync(id);
            if (size == null) return null;

            return new SizeDto
            {
                Id = size.Id,
                SizeNmae = size.SizeNmae
            };
        }

        public async Task CreateAsync(CreateSizeDto dto)
        {
            var size = new Size
            {
                SizeNmae = dto.SizeNmae
            };

            await _repository.AddAsync(size);
        }

        public async Task UpdateAsync(UpdateSizeDto dto)
        {
            var size = await _repository.GetByIdAsync(dto.Id);
            size.SizeNmae = dto.SizeNmae;
            await _repository.UpdateAsync(size);
        }

        public async Task DeleteAsync(int id)
        {
          

            await _repository.DeleteAsync(id);
        }
    }

}
