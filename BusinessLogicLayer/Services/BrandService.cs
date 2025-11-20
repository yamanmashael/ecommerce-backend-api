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


    public class BrandService : IBrandService
    {

        private readonly IBrandRepository _repository;
        public BrandService(IBrandRepository repository)
        {
            _repository = repository;
        }



        public async Task<IEnumerable<BrandDto>> GetAllAsync()
        {
            var brands = await _repository.GetAllAsync();
            var dtos = brands.Select(b => new BrandDto
            {
                Id = b.Id,
                BarndName = b.BarndName,
                BarndDescription = b.BarndDescription
            });
            return dtos;

        }

        public async Task<BrandDto> GetByIdAsync(int id)
        {
            var brand = await _repository.GetByIdAsync(id);
            if (brand == null) return null;

            return new BrandDto
            {
                Id = brand.Id,
                BarndName = brand.BarndName,
                BarndDescription = brand.BarndDescription
            };
        }

        public async Task CreateAsync(CreateBrandDto dto)
        {

            var brand = new Brand
            {
                BarndName = dto.BarndName,
                BarndDescription = dto.BarndDescription
            };

            await _repository.AddAsync(brand);
        }


        public async Task UpdateAsync(int id, UpdateBrandDto dto)
        {

            var brand = await _repository.GetByIdAsync(id);

            brand.BarndName = dto.BarndName;
            brand.BarndDescription = dto.BarndDescription;

            await _repository.UpdateAsync(brand);
        }

        public async Task DeleteAsync(int id)
        {
            var brand = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(brand);
        }

        public Task<int> CountAsync()
        {
            return _repository.CountAsync();    
        }

        public async Task<int> SerchCountAsync(string? search)
        {
            return await _repository.SerchCountAsync(search);
        }

    }
}
