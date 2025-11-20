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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IGenderRepository _genderRepository;

        public CategoryService(ICategoryRepository repository, IGenderRepository genderRepository)
        {
            _repository = repository;
            _genderRepository = genderRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                GenderId = c.GenderId,
                GenderName = c.Gender?.GenderName
            });
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoryByGender(int genderId)
        {
            var categories = await _repository.GetCategoryByGender(genderId);

            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                GenderId = c.GenderId,
                GenderName = c.Gender?.GenderName
            });

        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                GenderId = category.GenderId,
                GenderName = category.Gender?.GenderName
            };
        }

        public async Task CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
                GenderId = dto.GenderId
            };

            await _repository.AddAsync(category);
        }

        public async Task UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) throw new Exception("Category not found");

            category.CategoryName = dto.CategoryName;
            category.GenderId = dto.GenderId;

            await _repository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) throw new Exception("Category not found");

            await _repository.DeleteAsync(category);
        }

   
    }

}
