using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service, IWebHostEnvironment env)
        {
            _service = service;
        }

        [HttpGet("PopularProduct")]
        public async Task<IActionResult> GetPopularProducts()
        {
            try
            {
                var data = await _service.GetPopularProductsAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                var detailedError = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(detailedError);
            }
        }

        [HttpGet("NewProduct")]
        public async Task<IActionResult> GetNewProducts()
        {
            try
            {
                var data = await _service.GetNewProductsAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                var detailedError = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(detailedError);
            }
        }

        [HttpGet("SearchSuggestions")]
        public async Task<IActionResult> GetSearchSuggestions([FromQuery] string query)
        {
            var suggestions = await _service.GetSearchSuggestionsAsync(query);
            return Ok(suggestions);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] SearchFilterDto filter)
        {
            var result = await _service.FilterdProductsAsync(filter);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] RequestProduct request)
        {
            try
            {
                var products = await _service.GetProductsAsync(request);
                return Ok(products);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _service.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "Product not found."
                    });
                }

                return Ok(new ResponseModel<ProductDto>
                {
                    Success = true,
                    Data = product
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

        [HttpGet("Product/{id}")]
        public async Task<IActionResult> GetProductDetailsById(int id)
        {
            try
            {
                var product = await _service.GetProductDatailesByIdAsync(id);
                if (product == null)
                {
                    return NotFound(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "Product not found."
                    });
                }

                return Ok(new ResponseModel<ProductDetailsDto>
                {
                    Success = true,
                    Data = product
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

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProductDto dto)
        {
            try
            {
                await _service.AddProductAsync(dto);
                return Ok(new ResponseModel<object> { Success = true });
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
        public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return NotFound(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "Error during update."
                    });
                }

                await _service.UpdateProductAsync(dto);
                return Ok(new ResponseModel<object> { Success = true });
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteProductAsync(id);
                return Ok(new ResponseModel<object> { Success = true });
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
