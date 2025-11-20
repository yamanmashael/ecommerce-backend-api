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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        public ProductService(IProductRepository repository, ICategoryRepository categoryRepository, IBrandRepository brandRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
        }

        public async Task<List<string>> GetSearchSuggestionsAsync(string query)
        {
            return await _repository.GetSearchSuggestionsAsync(query);
        }


        public async Task<FiltreDto> FilterdProductsAsync(SearchFilterDto dto)
        {
            var products = await _repository.FilterProductsAsync(
                dto.GenderIds,
                dto.CategoryIds,
                dto.CategoryItemIds,
                dto.BrandIds,
                dto.SizeIds,
                dto.ColorIds,
                dto.MinPrice,
                dto.MaxPrice,
                dto.Keyword);

            var product= products.Select(x => new ProductFiltreDto
            {
                ProductId = x.Product.Id,
                ProductItemId=x.Id,
                BarndName = x.Product.Brand.BarndName,
                ProductName = x.Product.ProductName,
                ColorName = x.Color.ColorName,
                SalesPrice = x.SalesPrice,
                productFiltrImagees = x.ProductImages.Select(img => new ProductFiltrImageeDto
                {
                    ImageFilename = "https://yamanstore.blob.core.windows.net/product-photos/" + img.ImageFilename
                }).ToList()
            });

            var categoriItems = products
                  .Select(x => new CategoryItemFiltrDto
                  {
                      Id = x.Product.CategoryItem.Id,
                      CategoryItemName = x.Product.CategoryItem.CategoryItemName
                  })
                  .DistinctBy(c => c.Id)  
                  .ToList();

            var categorys = products
                  .Select(x => new CategoryFiltrDto
                  {
                      Id = x.Product.CategoryItem.Category.Id,
                      CategoryName = x.Product.CategoryItem.Category.CategoryName
                  })
                  .DistinctBy(c => c.Id) 
                  .ToList();



            return new FiltreDto
            {
                CategoryItemFiltrDto = categoriItems,
                productFiltreDtos= product,
                categoryFiltrDtos=categorys
            
            };

        }



        public async Task<IEnumerable<ProductFiltreDto>> GetPopularProductsAsync()
        {
            var products = await _repository.GetPopularProductsAsync();

       

            var items = products.Select(x => new ProductFiltreDto
            {
                ProductId = x.ProductId,
                ProductItemId = x.Id,
                BarndName = x.Product.Brand.BarndName,
                ProductName = x.Product.ProductName,
                ColorName = x.Color.ColorName,
                SalesPrice = x.SalesPrice,
                productFiltrImagees = x.ProductImages.Select(img => new ProductFiltrImageeDto
                {
                    ImageFilename = "https://yamanstore.blob.core.windows.net/product-photos/" + img.ImageFilename
                }).ToList()
            });

            return items;
        }



        public async Task<IEnumerable<ProductFiltreDto>> GetNewProductsAsync()
        {
            var products = await _repository.GetNewProductsAsync();

            var Y = products;

            var items = products.Select(x => new ProductFiltreDto
            {
                ProductId = x.ProductId,
                ProductItemId = x.Id,
                BarndName = x.Product.Brand.BarndName,
                ProductName = x.Product.ProductName,
                ColorName = x.Color.ColorName,
                SalesPrice = x.SalesPrice,
                productFiltrImagees = x.ProductImages.Select(img => new ProductFiltrImageeDto
                {
                    ImageFilename = "https://yamanstore.blob.core.windows.net/product-photos/" + img.ImageFilename
                }).ToList()
            });

            return items;
        }







        public async Task<ResponseModel<IEnumerable<ProductDto>>> GetProductsAsync(RequestProduct request)
        {
            try
            {
                var query = await _repository.GetProductsAsync(
                request.genderId,
                request.categoryId,
                request.categoryItemId,
                request.BrandId,
                request.search
            );


                var totalCount = await query.CountAsync();



                int totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            if (request.PageNumber > totalPages)
                request.PageNumber = totalPages > 0 ? totalPages : 1;

            var products = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

              var items = products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    BrandName = p.Brand.BarndName,
                    GenderName = p.CategoryItem.Category.Gender.GenderName,
                    CategoryName = p.CategoryItem.Category.CategoryName,
                    CategoryItemName = p.CategoryItem.CategoryItemName,
                    ProductName = p.ProductName,
                    ImageUrl= "https://yamanstore.blob.core.windows.net/product-photos/" + p.ProductItems
                    .SelectMany(i => i.ProductImages)
                    .OrderBy(img => img.Id)
                    .Select(img => img.ImageFilename)
                    .FirstOrDefault()

              }).ToList();

            return new ResponseModel<IEnumerable<ProductDto>>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Success = true,
                Data = items
            };
            }
            catch (Exception ex)
            {
                return new ResponseModel<IEnumerable<ProductDto>>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalCount = 0,
                    TotalPages = 0,
                    Success = false,
                    ErrorMassage = ex.Message,
                    Data = null
                };
            }

        }

        public async Task<ProductDetailsDto> GetProductDatailesByIdAsync(int id)
        {
            var product = await _repository.GetProductDatailesByIdAsync(id);

            if (product == null)
            {
                return null; 
            }

            var productDto = new ProductDetailsDto
            {
                Id= product.Id,
                Name = product.ProductName,
                Description = product.ProductDescription,
                BrandName = product.Brand?.BarndName,
                ProductItems = product.ProductItems.Select(pi => new ProductItemDetailsDto
                {
                    Id = pi.Id,
                    Price = pi.Price,
                    SalesPrice = pi.SalesPrice,
                    ColorName = pi.Color?.ColorName,
                    ProductCode = pi.product_code,
                    ImageFilenames = pi.ProductImages.Select(img => $"https://yamanstore.blob.core.windows.net/product-photos/{img.ImageFilename}").ToList(),
                    Sizes = pi.ProductItemSizes.Select(pis => new ProductSizeDetailsDto
                    {
                        SizeId = pis.SizeId,
                        SizeName = pis.Size?.SizeNmae,
                        Stock = pis.Stock
                    }).ToList()
                }).ToList()
            };

            return productDto;
        }



        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);


            return new ProductDto
            {
                Id = product.Id,
                ProductName=product.ProductName,
                ProductDescription=product.ProductDescription,  
                CategoryItemId= product.CategoryItemId,
                CategoryItemName= product.CategoryItem.CategoryItemName,
                CategoryId = product.CategoryItem.CategoryId,
                CategoryName=product.CategoryItem.Category.CategoryName,
                GenderId=product.CategoryItem.Category.GenderId,
                GenderName=product.CategoryItem.Category.Gender.GenderName,
                BrandId=product.BrandId,
                BrandName=product.Brand.BarndName

            };
        }
   




        public async Task AddProductAsync(CreateProductDto dto)
        {

         
            var product = new Product()
            {
                BrandId= dto.BrandId,
                CategoryItemId= dto.CategoryItemId,
                ProductName= dto.ProductName,
                ProductDescription= dto.ProductDescription,
            };

            await _repository.AddASync(product);

        }


        public async Task UpdateProductAsync(UpdateProductDto dto)
        {

            var product = await _repository.GetByIdAsync(dto.Id);


            product.BrandId = dto.BrandId;
            product.CategoryItemId = dto.CategoryItemId;
            product.ProductName = dto.ProductName;
            product.ProductDescription = dto.ProductDescription;
            

            await _repository.UpdateAsync(product);

        }



        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteAsync(id);
           
        }

    }
}
