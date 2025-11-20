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
    public class CategoryItemService : ICategoryItemService
    {
        private readonly ICategoryItemRepository _repository;

        public CategoryItemService(ICategoryItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryItemDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();

            return items.Select(i => new CategoryItemDto
            {
                Id = i.Id,
                CategoryItemName = i.CategoryItemName,
                CategoryItemDescription = i.CategoryItemDescription,
                CategoryId = i.CategoryId,
                CategoryName = i.Category?.CategoryName
            });
        }

        public async Task<IEnumerable<CategoryItemDto>> GetCategoryItemsByCategory(int categoryId)
        {
            var items = await _repository.GetCategoryItemsByCategory(categoryId);

            return items.Select(i => new CategoryItemDto
            {
                Id = i.Id,
                CategoryItemName = i.CategoryItemName,
                CategoryItemDescription = i.CategoryItemDescription,
                CategoryId = i.CategoryId,
                CategoryName = i.Category?.CategoryName
            });
        }

        public async Task<CategoryItemDto> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return null;

            return new CategoryItemDto
            {
                Id = item.Id,
                CategoryItemName = item.CategoryItemName,
                CategoryItemDescription = item.CategoryItemDescription,
                CategoryId = item.CategoryId,
                CategoryName = item.Category?.CategoryName
            };
        }

        public async Task CreateAsync(CreateCategoryItemDto dto)
        {
            var item = new CategoryItem
            {
                CategoryItemName = dto.CategoryItemName,
                CategoryItemDescription = dto.CategoryItemDescription,
                CategoryId = dto.CategoryId
            };

            await _repository.AddAsync(item);
        }

        public async Task UpdateAsync(int id, UpdateCategoryItemDto dto)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) throw new Exception("CategoryItem not found");

            item.CategoryItemName = dto.CategoryItemName;
            item.CategoryItemDescription = dto.CategoryItemDescription;
            item.CategoryId = dto.CategoryId;

            await _repository.UpdateAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) throw new Exception("CategoryItem not found");

            await _repository.DeleteAsync(item);
        }


    }

}
