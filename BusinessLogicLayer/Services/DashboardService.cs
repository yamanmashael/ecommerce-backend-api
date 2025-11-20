using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class DashboardService : IDashboardService
    {

        private readonly IDashboardRepository _dashboardRepository; 

        public DashboardService(IDashboardRepository dashboardRepository)
        {
           _dashboardRepository = dashboardRepository;  
        }


        public async Task<IEnumerable<SalesSummaryDto>> GetSalesDataAsync(string periodType, DateTime startDate)
        {
            var orders = await _dashboardRepository.GetOrderSalesAsync(startDate);

            IEnumerable<SalesSummaryDto> result;

            switch (periodType.ToLower())
            {
                case "daily": 
                    result = orders
                        .GroupBy(o => o.OrderDate.Date)
                        .Select(g => new SalesSummaryDto
                        {
                            Period = g.Key.ToString("yyyy-MM-dd"),
                            OrdersCount = g.Count(),
                            TotalSales = g.Sum(x => x.TotalPrice) 
                        })
                        .OrderBy(x => x.Period);
                    break;

                case "monthly":
                    result = orders
                        .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                        .Select(g => new SalesSummaryDto
                        {
                            Period = $"{g.Key.Year}-{g.Key.Month:D2}",
                            OrdersCount = g.Count(),
                            TotalSales = g.Sum(x => x.TotalPrice)
                        })
                        .OrderBy(x => x.Period);
                    break;

                case "yearly":
                    result = orders
                        .GroupBy(o => o.OrderDate.Year)
                        .Select(g => new SalesSummaryDto
                        {
                            Period = g.Key.ToString(),
                            OrdersCount = g.Count(),
                            TotalSales = g.Sum(x => x.TotalPrice)
                        })
                        .OrderBy(x => x.Period);
                    break;

                default:
                    throw new ArgumentException("Invalid period type. Use 'daily', 'monthly', or 'yearly'.");
            }

            return result;
        }


        public async Task<IEnumerable<OrderStatusDashboardDto>> OrderStatusCountsAsync()
        {
            var orderStatus = await _dashboardRepository.GetOrderStatusSummaryAsync();

            var dashboardData = orderStatus.Select(item => new OrderStatusDashboardDto
            {
                StatusName = item.Status, 
                OrderStatusCount = item.Count 
            });

            return dashboardData;
        }


        public async Task<int> NewCustomersCountAsync()
        {
            return await _dashboardRepository.NewCustomersAsync();
        }





        public async Task<IEnumerable<ProductDahboardDto>> PopularProductsAsync()
        {
            var products = await _dashboardRepository.GetPopularProductsAsync();

            return products.Select(x => new ProductDahboardDto
            {
                Id = x.Product.Id,
                ProductName = x.Product.ProductName,
                ImageUrl = x.ImageFilename != null
                    ? "https://yamanstore.blob.core.windows.net/product-photos/" + x.ImageFilename
                    : null,
                TotalQuantity = x.TotalQuantitySold,
                TotalPrice = x.TotalPrice
            });
        }








    }
}
