using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public  interface IProductRepository
    {

        Task<List<string>> GetSearchSuggestionsAsync(string query);

        Task<IEnumerable<ProductItem>> FilterProductsAsync(
                 List<int>? genderId,
                List<int>? categoryId,
                List<int>? categoryItemId,
                List<int>? brandId,
                List<int>? sizeId,
                List<int>? colorId,
                decimal? minPrice,
                decimal? maxPrice,
                string? keyword);



        Task<IQueryable<Product>> GetProductsAsync(
            int? genderId,
            int? categoryId,
            int? categoryItemId,
            int? BrandId,
            string? search
        );

        Task<IEnumerable<ProductItem>> GetPopularProductsAsync();
        Task<IEnumerable<ProductItem>> GetNewProductsAsync();

        Task<Product> GetProductDatailesByIdAsync(int id);
        Task<Product> GetByIdAsync(int id);

        Task AddASync(Product product);
        Task UpdateAsync(Product product);

        Task DeleteAsync(int id);

    }
}
