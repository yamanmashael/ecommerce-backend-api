using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public  interface IDashboardService
    {

        Task<IEnumerable<SalesSummaryDto>> GetSalesDataAsync(string periodType, DateTime startDate);
        Task<IEnumerable<OrderStatusDashboardDto>> OrderStatusCountsAsync();
        Task<int> NewCustomersCountAsync();
       Task<IEnumerable<ProductDahboardDto>> PopularProductsAsync();

    }
}
