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
    public class StockRepository : IStockRepository
    {

        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductItemSize> GetStockByIdAsync(int ProductItemSizeId)
        {
           return await _context.ProductItemSize.FirstOrDefaultAsync(x=>x.Id==ProductItemSizeId);    
        }

        public async Task<ProductItemSize> GetStockByPZAsync(int ProductItemId, int SizeId)
        {
            return await _context.ProductItemSize.FirstOrDefaultAsync(x => x.PrductItemId == ProductItemId && x.SizeId==SizeId);

        }

        public async Task UpdateStockAsync(ProductItemSize stock)
        {
           _context.ProductItemSize.Update(stock);
           await _context.SaveChangesAsync();   
        }
    }
}
