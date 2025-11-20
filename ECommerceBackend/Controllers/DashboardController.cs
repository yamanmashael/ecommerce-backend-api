using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {


        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("sales")]
        public async Task<IActionResult> GetSales([FromQuery] string periodType, [FromQuery] DateTime startDate)
        {
            var data = await _dashboardService.GetSalesDataAsync(periodType, startDate);
            return Ok(data);
        }


        [HttpGet("customer")]
        public async Task<IActionResult> GetCustomerCount()
        {
            var data = await _dashboardService.NewCustomersCountAsync();    
            
            return Ok(data);
        }

        [HttpGet("order")]
        public async Task<IActionResult> GetOrderStatus()
        {
            var data = await _dashboardService.OrderStatusCountsAsync();
            return Ok(data);
        }


        [HttpGet("product")]
        public async Task<IActionResult> PopularProduct()
        {
            var data = await _dashboardService.PopularProductsAsync();
            return Ok(data);
        }




    }
}
