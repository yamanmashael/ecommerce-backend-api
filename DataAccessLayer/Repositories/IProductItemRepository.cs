using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public  interface IProductItemRepository
    {
        Task<IEnumerable<ProductItem>> GetProductItemsByIdAsync(int ProductId);

        Task<ProductItem> GetProductItemByIdAsync(int id);
        Task AddProductItemAsync(ProductItem productItem);
        Task UpdateProductItemAsync(ProductItem productItem);
        Task DeleteProductItemAsync(int id);

    }
}
