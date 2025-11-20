using Azure.Core;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrdersService _service;
        public OrdersController(IOrdersService service)
        {
            _service = service;
        }


        protected int UserId()
        {
            var Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(Id);
            return userId;
        }




        [HttpGet("admin")]
        public async Task<ActionResult> GetOrderDashboard([FromQuery] RequestOrder requestOrder)
        {
            try
            {

                var products = await _service.GetOrdersDashboard(requestOrder); 
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<object> { Success = false, ErrorMassage = "حدث خطأ غير متوقع: " + ex.Message });
            }
        }

  

  
        [HttpGet("order")]
        public async Task<ActionResult> GetOrderByUserId()
        {
            try
            {
               var response=await _service.GetAllOrdersAsync(UserId()); 

                return Ok(new ResponseModel<IEnumerable<OrderDto>>
                {
                    Success = true,
                   Data= response

                });
            }
            catch (Exception ex)
            {
                var detailedError = ex.InnerException?.Message ?? ex.Message;
              

                return BadRequest(new ResponseModel<object>
                {
                    Success = false,
                    ErrorMassage = detailedError
                });
            }
        }


        [HttpGet("orderdatails/{orderId}")]
        public async Task<ActionResult> GetDatails(int orderId)
        {
            try
            {
                var response = await _service.GetOrderDatailsAsync(orderId);

                return Ok(new ResponseModel<IEnumerable<OrderDetailsDto>>
                {
                    Success = true,
                    Data = response

                });
            }
            catch (Exception ex)
            {
                var detailedError = ex.InnerException?.Message ?? ex.Message;


                return BadRequest(new ResponseModel<object>
                {
                    Success = false,
                    ErrorMassage = detailedError
                });
            }
        }


        [HttpPost("cancelOrder")]
        public async Task<ActionResult> CancelOrder([FromBody] int OrderId)
        {
            try
            {
                await _service.CancelOrderAsync(OrderId);

                return Ok(new ResponseModel<object>
                {
                    Success = true
                });
            }

            catch (Exception ex)
            {
                var detailedError = ex.InnerException?.Message ?? ex.Message;


                return BadRequest(new ResponseModel<object>
                {
                    Success = false,
                    ErrorMassage = detailedError
                });
            }
        }



        [HttpPost]
        public async Task<ActionResult> Create([FromBody] int AdressId)
        {
            try
            {
                await _service.CreateOrdersAsync(AdressId, UserId());

                return Ok(new ResponseModel<object>
                {
                    Success = true
                });
            }

            catch (Exception ex)
            {
                var detailedError = ex.InnerException?.Message ?? ex.Message;
              

                return BadRequest(new ResponseModel<object>
                {
                    Success = false,
                    ErrorMassage = detailedError
                });
            }
        }


        [HttpPut("UpdateShipping/{id}")]
        public async Task<ActionResult<ResponseModel<object>>> UpdateShipping(int id, [FromBody] UpdateShippingDto dto)
        {
            try
            {
                if (id != dto.OrderId)
                {
                    return BadRequest(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "معرف الطلب غير متطابق"
                    });
                }

                 await    _service.UpdateShippingAsync(dto);

                return Ok(new ResponseModel<object>
                {
                    Success = true,
                });
            }
            catch (Exception ex)
            {
                var detailedError = ex.InnerException?.Message ?? ex.Message;


                return BadRequest(new ResponseModel<object>
                {
                    Success = false,
                    ErrorMassage = detailedError
                });
            }
        }


        [HttpPut("UpdatePaymentS/{id}")]
        public async Task<ActionResult<ResponseModel<object>>> UpdatePaymentStatu(int id, [FromBody] UpdatePaymentStatusDto dto)
        {
            try
            {
                if (id != dto.OrderId)
                {
                    return BadRequest(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "معرف الطلب غير متطابق"
                    });
                }

                await _service.UpdatePaymentStatusAsync (dto);

                return Ok(new ResponseModel<object>
                {
                    Success = true,
                });
            }
            catch (Exception ex)
            {
                var detailedError = ex.InnerException?.Message ?? ex.Message;


                return BadRequest(new ResponseModel<object>
                {
                    Success = false,
                    ErrorMassage = detailedError
                });
            }
        }


        [HttpPut("UpdateStatus/{id}")]
        public async Task<ActionResult<ResponseModel<object>>> UpdateStatus(int id, [FromBody] UpdateStatusDto dto)
        {
            try
            {
                if (id != dto.OrderId)
                {
                    return BadRequest(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "معرف الطلب غير متطابق"
                    });
                }

                await _service.UpdateOrderStatusAsync(dto);

                return Ok(new ResponseModel<object>
                {
                    Success = true,
                });
            }
            catch (Exception ex)
            {
                var detailedError = ex.InnerException?.Message ?? ex.Message;


                return BadRequest(new ResponseModel<object>
                {
                    Success = false,
                    ErrorMassage = detailedError
                });
            }
        }










    }
}
