using BusinessLogicLayer.DTOs;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ProductItemService : IProductItemService
    {
        private readonly IProductItemRepository _repository;
      
        public ProductItemService(IProductItemRepository repository)
        {
            _repository = repository;
        
        }

        public async Task<IEnumerable<ProductItemDto>> GetProductItemsAsync(int id)
        {
            var productItems = await _repository.GetProductItemsByIdAsync(id);

            return   productItems.Select(x => new ProductItemDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ColorId = x.ColorId,
                ColorName = x.Color?.ColorName, 
                Price = x.Price, 
                SalesPrice =x.SalesPrice,
                product_code = x.product_code
            });

        }


        public async Task<ProductItemDto> GetProductItemByIdAsync(int id)
        {
            var productItem = await _repository.GetProductItemByIdAsync(id);
            if (productItem == null)
            {
                return null;
            }

            return new ProductItemDto
            {
                Id = productItem.Id,
                ProductId = productItem.ProductId,
                ColorId = productItem.ColorId,
                ColorName = productItem.Color?.ColorName,
                Price = productItem.Price,
                SalesPrice = productItem.SalesPrice,
                product_code = productItem.product_code
            };
        }

        public async Task AddProductItemAsync(CreateProductItemDto dto)
        {
            var productItem = new ProductItem
            {
                ProductId = dto.ProductId,
                ColorId = dto.ColorId,
                Price = dto.Price,
                SalesPrice = dto.SalesPrice,
                product_code = dto.product_code,
                CreatedDate=DateTime.Now

            };

           await _repository.AddProductItemAsync(productItem);
           
        }

        public async Task UpdateProductItemAsync(UpdateProductItemDto dto)
        {
            var productItem = await _repository.GetProductItemByIdAsync(dto.Id);
           

            productItem.ColorId = dto.ColorId;
            productItem.Price = dto.Price;
            productItem.SalesPrice = dto.SalesPrice;
            productItem.product_code = dto.product_code;

            await _repository.UpdateProductItemAsync(productItem);
        }

        public async Task DeleteProductItemAsync(int id)
        {
             await _repository.DeleteProductItemAsync(id);
        }



    }
}
