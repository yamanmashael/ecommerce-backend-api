using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IProductImageRepository
    {
        Task<IEnumerable<ProductImage>> GetByProductItemIdAsync(int productItemId);
        Task<ProductImage> GetByIdAsync(int id);
        Task<ProductImage> AddAsync(ProductImage image);
        Task DeleteAsync(ProductImage image);
        Task SaveChangesAsync();
    }
}
