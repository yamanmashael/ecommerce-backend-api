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
    public  class PhotosRepository :IPhotosRepository
    {

        private readonly ApplicationDbContext _context;

        public PhotosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductImage>> GetByProductItemIdAsync(int productItemId)
        {
            return await _context.ProductImage
                .Where(p => p.ProductItemId == productItemId)
                .ToListAsync();
        }

    }
}
