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
    public class DashboardRepository : IDashboardRepository
    {

        private readonly ApplicationDbContext _context;

        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Orders>> GetOrderSalesAsync(DateTime Date)
        {
            var order = await _context.Orders.Where(x => x.OrderDate >= Date && x.PaymentStatus == true).ToListAsync();
            return order;   
        }


        public async Task<IEnumerable<Orders>> GetOrderStatusAsync()
        {
            return await _context.Orders.Include(x => x.OrderStatus).ToListAsync();
        }

        public async Task<int> NewCustomersAsync()
        {
            return await _context.Users
             .Where(u => u.CreatedAt.Date == DateTime.UtcNow.Date).CountAsync();
        }


        
        public async Task<List<(string Status, int Count)>> GetOrderStatusSummaryAsync()
        {
            return await _context.Orders
                .GroupBy(o => o.OrderStatus.StatusName)
                .Select(g => new ValueTuple<string, int>(g.Key, g.Count()))
                .ToListAsync();
        }


        public async Task<IEnumerable<(Product Product, int TotalQuantitySold, decimal TotalPrice, string? ImageFilename)>> GetPopularProductsAsync()
        {
            var query = await _context.OrderDetails
                .Include(od => od.ProductItem)
                    .ThenInclude(pi => pi.Product)
                .Include(od => od.ProductItem)
                    .ThenInclude(pi => pi.ProductImages)
                .GroupBy(od => od.ProductItem.Product)
                .Select(g => new
                {
                    Product = g.Key,
                    TotalQuantitySold = g.Sum(x => x.Quantity),
                    TotalPrice = g.Sum(x => x.Quantity * x.Price),
                    ProductImages = g.SelectMany(x => x.ProductItem.ProductImages) 
                })
                .OrderByDescending(x => x.TotalQuantitySold)
                .Take(10)
                .ToListAsync();

            return query.Select(x => (
                x.Product,
                x.TotalQuantitySold,
                x.TotalPrice,
                x.ProductImages.OrderBy(img => img.Id).Select(img => img.ImageFilename).FirstOrDefault()
            ));
        }




    }
}
