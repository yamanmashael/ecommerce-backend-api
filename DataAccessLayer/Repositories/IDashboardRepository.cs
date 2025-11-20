using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IDashboardRepository
    {

        Task<IEnumerable<Orders>> GetOrderSalesAsync(DateTime Date);
        Task<IEnumerable<Orders>> GetOrderStatusAsync();
        Task<int> NewCustomersAsync();
        Task<List<(string Status, int Count)>> GetOrderStatusSummaryAsync();
        Task<IEnumerable<(Product Product, int TotalQuantitySold, decimal TotalPrice, string? ImageFilename)>> GetPopularProductsAsync();    


    }
}
