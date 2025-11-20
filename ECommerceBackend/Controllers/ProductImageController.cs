using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService _service;

        public ProductImagesController(IProductImageService service)
        {
            _service = service;
        }

        [HttpGet("{productItemId}")]
        public async Task<ActionResult> GetByProductItemId(int productItemId)
        {
            try
            {
                var images = await _service.GetImagesByProductItemIdAsync(productItemId);
                return Ok(new ResponseModel<IEnumerable<ProductImageDto>> { Success = true, Data = images });
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
        public async Task<ActionResult> Add([FromForm] ProductImageCreateDto dto)
        {
            try
            {
                var productItemId = await _service.AddImageAsync(dto);
                return Ok(new ResponseModel<int> { Success = true, Data = productItemId });
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
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var productItemId = await _service.DeleteImageAsync(id);
                return Ok(new ResponseModel<int> { Success = true, Data = productItemId });
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
