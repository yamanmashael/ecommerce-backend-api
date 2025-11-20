using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public  interface IProductService
    {

        Task<List<string>> GetSearchSuggestionsAsync(string query);

        Task<IEnumerable<ProductFiltreDto>> GetPopularProductsAsync();
        
        Task<IEnumerable<ProductFiltreDto>> GetNewProductsAsync();

        Task<FiltreDto> FilterdProductsAsync(SearchFilterDto dto);

        Task<ResponseModel<IEnumerable<ProductDto>>> GetProductsAsync(RequestProduct request );

        Task<ProductDetailsDto> GetProductDatailesByIdAsync(int id);

        Task<ProductDto> GetByIdAsync(int id);
        Task AddProductAsync(CreateProductDto dto);

        Task UpdateProductAsync(UpdateProductDto updateProductDto);
        Task DeleteProductAsync(int id);
    }
}
