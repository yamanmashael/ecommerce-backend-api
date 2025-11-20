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
    public class ProductItemSizeRepository : IProductItemSizeRepository
    {

        private readonly ApplicationDbContext _context;
        public ProductItemSizeRepository(ApplicationDbContext context)
        {
            _context = context;
        } 

        public async Task<IEnumerable<ProductItemSize>> GetProductItemSizesByIdAsync(int ProductItemId)
        {
            var productItemsize = await _context.ProductItemSize
                    .Include(s => s.Size)
                    .Where(x => x.PrductItemId == ProductItemId).ToListAsync();
            
            return productItemsize;
        }


        public async Task<ProductItemSize> GetProductItemSizeByIdAsync(int id)
        {
            return await _context.ProductItemSize
                .Include(s => s.Size)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProductItemSizeAsync(ProductItemSize productItemSize)
        {
            await _context.ProductItemSize.AddAsync(productItemSize);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductItemSizeAsync(ProductItemSize productItemSize)
        {
            _context.ProductItemSize.Update(productItemSize);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductItemSizeAsync(int id)
        {
            var productItemSize = await _context.ProductItemSize.FindAsync(id);
           

            _context.ProductItemSize.Remove(productItemSize);
            await _context.SaveChangesAsync();
        }




    }
}
