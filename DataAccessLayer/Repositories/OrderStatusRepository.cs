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
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderStatus>> GetAllAsync()
        {
            return await _context.OrderStatus.ToListAsync();
        }

        public async Task<OrderStatus> GetByIdAsync(int id)
        {
            return await _context.OrderStatus.FindAsync(id);
        }

        public async Task AddAsync(OrderStatus orderStatus)
        {
            await _context.OrderStatus.AddAsync(orderStatus);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderStatus orderStatus)
        {
            _context.OrderStatus.Update(orderStatus);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var orderStatus = await _context.OrderStatus.FindAsync(id);
            if (orderStatus != null)
            {
                _context.OrderStatus.Remove(orderStatus);
                await _context.SaveChangesAsync();
            }
        }
    }

}
