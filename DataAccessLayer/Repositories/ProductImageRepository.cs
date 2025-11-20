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

    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductImage>> GetByProductItemIdAsync(int productItemId)
        {
            return await _context.ProductImages
                .Where(p => p.ProductItemId == productItemId)
                .ToListAsync();
        }

        public async Task<ProductImage> GetByIdAsync(int id)
        {
            return await _context.ProductImages.FindAsync(id);
        }

        public async Task<ProductImage> AddAsync(ProductImage image)
        {
            await _context.ProductImages.AddAsync(image);
            return image;
        }

        public async Task DeleteAsync(ProductImage image)
        {
            _context.ProductImages.Remove(image);

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }


}
