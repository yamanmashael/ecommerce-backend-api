using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public  interface IFavoriteRepository
    {
        Task<IEnumerable<Favorites>> GetUserFavoritsAsync(int userId);
        Task CreateFavoriteAsync(int productItemId, int UserId);
        Task DeleteFavoriteAsync(int favoriteId);


    }
}
