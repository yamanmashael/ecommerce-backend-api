using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _repository;
        public FavoriteService(IFavoriteRepository repository)
        {
            _repository = repository;
        }





        public async Task<IEnumerable<FavoriteDto>> GetFavoritesAsync(int UserId)
        {
            var favorites = await _repository.GetUserFavoritsAsync(UserId);


            return favorites.Select(x => new FavoriteDto
            {
               FavoriteId=x.Id,
               productId=x.ProductItem.ProductId,
               ProductItemId=x.ProductItemId,
               ProductName=x.ProductItem.Product.ProductName,
               ColorName=x.ProductItem.Color.ColorName,
               BarndName=x.ProductItem.Product.Brand.BarndName,
               SalesPrice=x.ProductItem.SalesPrice,
                favoriteImageeDtos = x.ProductItem.ProductImages.Select(img => new ProductFavoriteImageeDto
                {
                    ImageFilename = "https://yamanstore.blob.core.windows.net/product-photos/" + img.ImageFilename
                }).ToList()

            }).ToList();
        }




        public async Task CreateFavoriteAsync(int productItemId, int UserId)
        {
            await _repository.CreateFavoriteAsync(productItemId, UserId);

        }




        public async Task DeleteFavoriteAsync(int favoriteId)
        {
            await _repository.DeleteFavoriteAsync(favoriteId);

        }


    }
}

