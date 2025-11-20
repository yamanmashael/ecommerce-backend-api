using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotosService _service;

        public PhotosController(IPhotosService service)
        {
            _service = service;
        }

        [HttpGet("{productItemId}")]
        public async Task<ActionResult> GetByProductItemId(int productItemId)
        {
            try
            {
                var images = await _service.GetPhotosByProductItemIdAsync(productItemId);
                return Ok(new ResponseModel<IEnumerable<PhotosDto>> { Success = true, Data = images });
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
