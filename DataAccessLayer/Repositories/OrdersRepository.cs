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
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ApplicationDbContext _context;

        public OrdersRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IQueryable<Orders>> GetOrdersDashboardAsync(
            int? OrderId,
            int? Status,
            string? PaymentMethod,
            string? ShippingCompany,
            DateTime? StartDate,
            DateTime? EndDate)
        {
            var query = _context.Orders.Include(s=>s.OrderStatus).AsQueryable();

            if (OrderId.HasValue)
                query = query.Where(x => x.Id == OrderId.Value);

            if (Status.HasValue)
                query = query.Where(x => x.OrderStatusId == Status);

            if (!string.IsNullOrEmpty(PaymentMethod))
                query = query.Where(x => x.PaymentMethod == PaymentMethod);

            if (!string.IsNullOrEmpty(ShippingCompany))
                query = query.Where(x => x.ShippingCompany == ShippingCompany);

            if (StartDate.HasValue && EndDate.HasValue)
            {
               
                query = query.Where(x => x.OrderDate >= StartDate.Value && x.OrderDate <= EndDate.Value);
            }
            else if (StartDate.HasValue)
            {
               
                query = query.Where(x => x.OrderDate >= StartDate.Value);
            }
            else if (EndDate.HasValue)
            {
              
                query = query.Where(x => x.OrderDate <= EndDate.Value);
            }

          
            return await Task.FromResult(query); 
        }

        public async Task<Orders> GetOrderByIdAsync(int OrderId)
        {
            return await _context.Orders.Include(u=>u.User).Include(d => d.OrderDetails).Include(s => s.OrderStatus).FirstOrDefaultAsync(x => x.Id == OrderId);
        }



        public async Task<IEnumerable<Orders>> GetOrdersAsync(int userId)
        {
            return await _context.Orders.Include(d => d.OrderDetails).Include(s => s.OrderStatus).Where(d => d.UserId == userId).ToListAsync();
        }



        public async Task<Orders> GetOrderDetailsAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Size)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.ProductItem)
                        .ThenInclude(pi => pi.Product)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.ProductItem)
                        .ThenInclude(pi => pi.ProductImages)
                 .Include(s=>s.OrderStatus)
                 .Include(u=>u.User)
                      .ThenInclude(ua=>ua.Address)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }


        public async Task AddOrdersAsync(Orders orders)
        {
            _context.Orders.Add(orders);
            await _context.SaveChangesAsync();      
        }



        public async Task UpdateOrdersAsync(Orders orders)
        {
            _context.Orders.Update(orders);
            await _context.SaveChangesAsync();
        }


    }
}
