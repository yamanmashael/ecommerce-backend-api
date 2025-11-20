using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemSizeController : ControllerBase
    {
        private readonly IProductItemSizeService _service;

        public ProductItemSizeController(IProductItemSizeService service)
        {
            _service = service;
        }

        [HttpGet("{productItemId}")]
        public async Task<ActionResult> GetProductItemSizesByProductItemId(int productItemId)
        {
            try
            {
                var productItemSizes = await _service.GetProductItemSizeByProductId(productItemId);
                return Ok(new ResponseModel<IEnumerable<ProductItemSizeDto>>
                {
                    Success = true,
                    Data = productItemSizes
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<IEnumerable<ProductItemSizeDto>>
                {
                    Success = false,
                    ErrorMassage = "An unexpected error occurred: " + ex.Message
                });
            }
        }

        [HttpGet("single/{id}")]
        public async Task<ActionResult> GetProductItemSizeById(int id)
        {
            try
            {
                var productItemSize = await _service.GetProductItemSizeByIdAsync(id);
                if (productItemSize == null)
                {
                    return NotFound(new ResponseModel<ProductItemSizeDto>
                    {
                        Success = false,
                        ErrorMassage = "Product size not found."
                    });
                }

                return Ok(new ResponseModel<ProductItemSizeDto>
                {
                    Success = true,
                    Data = productItemSize
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<ProductItemSizeDto>
                {
                    Success = false,
                    ErrorMassage = "An unexpected error occurred: " + ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductItemSize([FromBody] CreateProductItemSizeDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "Invalid input data."
                    });
                }

                await _service.AddProductItemSizeAsync(dto);

                return Ok(new ResponseModel<ProductItemSizeDto>
                {
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<object>
                {
                    Success = false,
                    ErrorMassage = "An unexpected error occurred: " + ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProductItemSize(int id, [FromBody] UpdateProductItemSizeDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "Product size ID mismatch."
                    });
                }

                await _service.UpdateProductItemSizeAsync(dto);

                return Ok(new ResponseModel<object>
                {
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<object>
                {
                    Success = false,
                    ErrorMassage = "An unexpected error occurred: " + ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductItemSize(int id)
        {
            try
            {
                await _service.DeleteProductItemSizeAsync(id);
                return Ok(new ResponseModel<object>
                {
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<object>
                {
                    Success = false,
                    ErrorMassage = "An unexpected error occurred: " + ex.Message
                });
            }
        }
    }
}
