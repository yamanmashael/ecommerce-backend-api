using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class ProductItemRepository : IProductItemRepository
    {

        private readonly ApplicationDbContext _context;
        public ProductItemRepository(ApplicationDbContext context)
        { 
            _context = context;
        }

        public async Task<IEnumerable<ProductItem>> GetProductItemsByIdAsync(int productId)
        {
            var productItems = await _context.ProductItem
                .Include(c => c.Color)
                .Where(a => a.ProductId == productId) 
                .ToListAsync();

            return productItems;
        }


        public async Task<ProductItem> GetProductItemByIdAsync(int id)
        {
            return await _context.ProductItem
                .Include(c => c.Color)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProductItemAsync(ProductItem productItem)
        {
            await _context.ProductItem.AddAsync(productItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductItemAsync(ProductItem productItem)
        {
            _context.ProductItem.Update(productItem);
             await _context.SaveChangesAsync();
        }

        public async Task DeleteProductItemAsync(int id)
        {
            var productItem = await _context.ProductItem.FindAsync(id);
        

            _context.ProductItem.Remove(productItem);
             await _context.SaveChangesAsync();
        }



    }
}
