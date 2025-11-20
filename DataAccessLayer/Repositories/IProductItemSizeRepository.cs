using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IProductItemSizeRepository
    {
       Task<IEnumerable<ProductItemSize>> GetProductItemSizesByIdAsync(int ProductItemId);
        Task<ProductItemSize> GetProductItemSizeByIdAsync(int id);
        Task AddProductItemSizeAsync(ProductItemSize productItemSize);
        Task UpdateProductItemSizeAsync(ProductItemSize productItemSize);
        Task DeleteProductItemSizeAsync(int id);

    }
}
