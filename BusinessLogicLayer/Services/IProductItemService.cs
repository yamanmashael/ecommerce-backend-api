using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public  interface IProductItemService
    {
        Task<IEnumerable<ProductItemDto>> GetProductItemsAsync(int id);
        Task<ProductItemDto> GetProductItemByIdAsync(int id);
        Task AddProductItemAsync(CreateProductItemDto dto);
        Task UpdateProductItemAsync(UpdateProductItemDto dto);
        Task DeleteProductItemAsync(int id);
    }
}
