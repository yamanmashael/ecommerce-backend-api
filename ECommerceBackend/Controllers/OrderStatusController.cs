using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusService _service;

        public OrderStatusController(IOrderStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<OrderStatusDto>>>> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(new ResponseModel<IEnumerable<OrderStatusDto>>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<IEnumerable<OrderStatusDto>>
                {
                    Success = false,
                    ErrorMassage = $"An unexpected error occurred: {ex.Message}",
                    Data = Enumerable.Empty<OrderStatusDto>()
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<OrderStatusDto>>> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new ResponseModel<OrderStatusDto>
                    {
                        Success = false,
                        ErrorMassage = "Status not found"
                    });
                }

                return Ok(new ResponseModel<OrderStatusDto>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<OrderStatusDto>
                {
                    Success = false,
                    ErrorMassage = $"An unexpected error occurred: {ex.Message}"
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<OrderStatusDto>>> Create([FromBody] CreateOrderStatusDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseModel<OrderStatusDto>
                    {
                        Success = false,
                        ErrorMassage = "Invalid input data"
                    });
                }

                var result = await _service.CreateAsync(dto);

                return Ok(new ResponseModel<OrderStatusDto>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<OrderStatusDto>
                {
                    Success = false,
                    ErrorMassage = $"An unexpected error occurred: {ex.Message}"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<OrderStatusDto>>> Update(int id, [FromBody] UpdateOrderStatusDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest(new ResponseModel<OrderStatusDto>
                    {
                        Success = false,
                        ErrorMassage = "Order ID mismatch"
                    });
                }

                var result = await _service.UpdateAsync(dto);
                if (result == null)
                {
                    return NotFound(new ResponseModel<OrderStatusDto>
                    {
                        Success = false,
                        ErrorMassage = "Status not found"
                    });
                }

                return Ok(new ResponseModel<OrderStatusDto>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<OrderStatusDto>
                {
                    Success = false,
                    ErrorMassage = $"An unexpected error occurred: {ex.Message}"
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<object>>> Delete(int id)
        {
            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success)
                {
                    return NotFound(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "Status not found"
                    });
                }

                return Ok(new ResponseModel<object>
                {
                    Success = true,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<object>
                {
                    Success = false,
                    ErrorMassage = $"An unexpected error occurred: {ex.Message}"
                });
            }
        }
    }
}
