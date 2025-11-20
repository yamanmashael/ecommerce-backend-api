using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ProductItemSizeService : IProductItemSizeService
    {
        private readonly  IProductItemSizeRepository _repository;

        public ProductItemSizeService(IProductItemSizeRepository repository)
        {
            _repository = repository;

        }

        public async Task<IEnumerable<ProductItemSizeDto>> GetProductItemSizeByProductId(int ProductItemId)
        {

                var productItems = await _repository.GetProductItemSizesByIdAsync(ProductItemId);

                return productItems.Select(x => new ProductItemSizeDto
                {
                    Id=x.Id,
                    PrductItemId=x.PrductItemId,
                    Stock=x.Stock,
                    SizeId=x.SizeId,
                    SizeName=x.Size.SizeNmae
                });

        }


        public async Task<ProductItemSizeDto> GetProductItemSizeByIdAsync(int id)
        {
            var productItemSize = await _repository.GetProductItemSizeByIdAsync(id);
            if (productItemSize == null)
            {
                return null;
            }

            return new ProductItemSizeDto
            {
                Id = productItemSize.Id,
                PrductItemId = productItemSize.PrductItemId,
                Stock = productItemSize.Stock,
                SizeId = productItemSize.SizeId,
                SizeName = productItemSize.Size?.SizeNmae
            };
        }

        public async Task AddProductItemSizeAsync(CreateProductItemSizeDto dto)
        {
            var productItemSize = new ProductItemSize
            {
                PrductItemId = dto.PrductItemId,
                Stock = dto.Stock,
                SizeId = dto.SizeId
            };

            await _repository.AddProductItemSizeAsync(productItemSize);
        }

        public async Task UpdateProductItemSizeAsync(UpdateProductItemSizeDto dto)
        {
            var productItemSize = await _repository.GetProductItemSizeByIdAsync(dto.Id);
            if (productItemSize == null)
            {
                // You might want to throw an exception or handle this case.
                return;
            }

            productItemSize.Stock = dto.Stock;
            productItemSize.SizeId = dto.SizeId;

            await _repository.UpdateProductItemSizeAsync(productItemSize);
        }

        public async Task DeleteProductItemSizeAsync(int id)
        {
            await _repository.DeleteProductItemSizeAsync(id);
        }



    }
}
