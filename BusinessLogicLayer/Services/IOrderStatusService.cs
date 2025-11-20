using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IOrderStatusService
    {
        Task<IEnumerable<OrderStatusDto>> GetAllAsync();
        Task<OrderStatusDto> GetByIdAsync(int id);
        Task<OrderStatusDto> CreateAsync(CreateOrderStatusDto dto);
        Task<OrderStatusDto> UpdateAsync(UpdateOrderStatusDto dto);
        Task<bool> DeleteAsync(int id);
    }

}
