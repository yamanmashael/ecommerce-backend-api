using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IOrdersService
    {
        Task<ResponseOrder<IEnumerable<OrderDto>>> GetOrdersDashboard(RequestOrder requestOrder ); 
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync(int UserId);
        Task<IEnumerable<OrderDetailsDto>> GetOrderDatailsAsync(int UserId);
        Task CreateOrdersAsync(int AdressId, int UserId);
        Task CancelOrderAsync(int OrderId);

        Task UpdateShippingAsync(UpdateShippingDto updateShipping);
        Task UpdatePaymentStatusAsync(UpdatePaymentStatusDto updatePaymentStatusDto);
        Task UpdateOrderStatusAsync(UpdateStatusDto orderStatus);
    }
}
