using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IOrderStatusRepository
    {
        Task<IEnumerable<OrderStatus>> GetAllAsync();
        Task<OrderStatus> GetByIdAsync(int id);
        Task AddAsync(OrderStatus orderStatus);
        Task UpdateAsync(OrderStatus orderStatus);
        Task DeleteAsync(int id);
    }

}
