//using BusinessLogicLayer.DTOs;
//using DataAccessLayer.Entities;
//using DataAccessLayer.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BusinessLogicLayer.Services
//{
//    public  class StoreService:IStoreService
//    {
//        private readonly IProductRepository _repository;
//        private readonly ICategoryRepository _categoryRepository;
//        private readonly IBrandRepository _brandRepository;
//        public StoreService(IProductRepository repository, ICategoryRepository categoryRepository, IBrandRepository brandRepository)
//        {
//            _repository = repository;
//            _categoryRepository = categoryRepository;
//            _brandRepository = brandRepository;
//        }


//        public async Task<IEnumerable<StoreDto>> GetStoresAsync(ProductSearchDto searchDto )
//        {
//            var products = await _repository.SearchProductsAsync(
//                  searchDto.SearchText,
//                  searchDto.CategoryId,
//                  searchDto.BrandId,
//                  searchDto.MinPrice,
//                  searchDto.MaxPrice
//              );

//            if (products == null)
//            {
//                throw new KeyNotFoundException("لا يوجد منتجات ");
//            }

//            return products.Select(p => new StoreDto
//            {
//                Id = p.Id,
//                Name = p.Name,
//                Description = p.Description,
//                ImageUrl = p.ProductImages
//                   .OrderBy(img => img.Id)
//                 .FirstOrDefault()?.ImageUrl,
//                Price = p.Price,
//                Category = p.Category.Name,
//                Brand = p.Brand.Name,
                

//            }).ToList();


//        }





//        public async Task<StoreDto> GetStoreByIdAsync(int id)
//        {
//            var product = await _repository.GetByIdAsync(id);
//            if (product == null)
//            {
//                throw new KeyNotFoundException("المنتج غير موجود");
//            }

//            return new StoreDto
//            {
//                Id = product.Id,
//                Name = product.Name,
//                Description = product.Description,
//                Price = product.Price,
//                ProductImages = product.ProductImages.Select(c => new ProductImagesDto
//                {
//                    ImageUrl = c.ImageUrl,
//                }).ToList(),
//                Category = product.Category.Name,
//                Brand = product.Brand.Name
//            };
//        }



//    }
//}
