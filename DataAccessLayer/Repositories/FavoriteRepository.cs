using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {

        private ApplicationDbContext _context;
        public FavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Favorites>> GetUserFavoritsAsync(int userId)
        {
            var favorites = await _context.Favorites
                .Include(pi => pi.ProductItem).ThenInclude(p => p.Product).ThenInclude(b => b.Brand)
                .Include(pi => pi.ProductItem).ThenInclude(i => i.ProductImages)
                .Include(pi => pi.ProductItem).ThenInclude(c=>c.Color)
                .Where(x => x.UserId == userId).ToListAsync();

            return favorites;
        }

        public async Task CreateFavoriteAsync(int productItrmId, int UserId)
        {
            var existingFavorite = await _context.Favorites
              .FirstOrDefaultAsync(x => x.ProductItemId == productItrmId && x.UserId == UserId);

            if (existingFavorite == null)
            {
                var newFavorite = new Favorites
                {
                    UserId = UserId,
                    ProductItemId = productItrmId
                };

                await _context.Favorites.AddAsync(newFavorite);
                await _context.SaveChangesAsync();
            }





        }

        public async Task DeleteFavoriteAsync(int favoriteId)
        {
            var favorite = await _context.Favorites.Where(x => x.Id == favoriteId).FirstOrDefaultAsync();

            _context.Favorites.Remove(favorite);
            _context.SaveChanges();

        }




    }
}
