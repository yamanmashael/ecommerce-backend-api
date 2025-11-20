using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IFavoriteService
    {
        Task<IEnumerable<FavoriteDto>> GetFavoritesAsync(int UserId);
        Task CreateFavoriteAsync(int productItemId, int UserId);
        Task DeleteFavoriteAsync(int favoriteId);
    }
}
