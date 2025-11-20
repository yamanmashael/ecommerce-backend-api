using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemController : ControllerBase
    {
        private readonly IProductItemService _service;

        public ProductItemController(IProductItemService service)
        {
            _service = service;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult> GetProductItemsByProductId(int productId)
        {
            try
            {
                var productItems = await _service.GetProductItemsAsync(productId);
                return Ok(new ResponseModel<IEnumerable<ProductItemDto>>
                {
                    Success = true,
                    Data = productItems
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<IEnumerable<ProductItemDto>>
                {
                    Success = false,
                    ErrorMassage = "An unexpected error occurred: " + ex.Message
                });
            }
        }

        [HttpGet("single/{id}")]
        public async Task<ActionResult> GetProductItemById(int id)
        {
            try
            {
                var productItem = await _service.GetProductItemByIdAsync(id);
                if (productItem == null)
                {
                    return NotFound(new ResponseModel<ProductItemDto>
                    {
                        Success = false,
                        ErrorMassage = "Product item not found."
                    });
                }

                return Ok(new ResponseModel<ProductItemDto>
                {
                    Success = true,
                    Data = productItem
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<ProductItemDto>
                {
                    Success = false,
                    ErrorMassage = "An unexpected error occurred: " + ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductItem([FromBody] CreateProductItemDto dto)
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

                await _service.AddProductItemAsync(dto);
                return Ok(new ResponseModel<ProductItemDto>
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
        public async Task<ActionResult> UpdateProductItem(int id, [FromBody] UpdateProductItemDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "Product ID mismatch."
                    });
                }

                await _service.UpdateProductItemAsync(dto);
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
        public async Task<ActionResult> DeleteProductItem(int id)
        {
            try
            {
                await _service.DeleteProductItemAsync(id);
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
