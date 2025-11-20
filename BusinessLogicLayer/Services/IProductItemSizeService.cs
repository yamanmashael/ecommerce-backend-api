using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IProductItemSizeService
    {
        Task<IEnumerable<ProductItemSizeDto>> GetProductItemSizeByProductId(int ProductItemId);
        Task<ProductItemSizeDto> GetProductItemSizeByIdAsync(int id);
        Task AddProductItemSizeAsync(CreateProductItemSizeDto dto);
        Task UpdateProductItemSizeAsync(UpdateProductItemSizeDto dto);
        Task DeleteProductItemSizeAsync(int id);

    }
}
