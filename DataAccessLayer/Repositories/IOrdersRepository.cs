using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public  interface IOrdersRepository
    {
        Task<IQueryable<Orders>> GetOrdersDashboardAsync(int? OrderId, int? Status, string? PaymentMethod, string? ShippingCompany, DateTime? StartDate, DateTime? EndDate);
        Task<IEnumerable<Orders>> GetOrdersAsync(int userId);

        Task<Orders> GetOrderByIdAsync(int OrderId);
        Task<Orders> GetOrderDetailsAsync(int OrderId);
        Task AddOrdersAsync(Orders orders);

        Task UpdateOrdersAsync(Orders orders);
    }
}
