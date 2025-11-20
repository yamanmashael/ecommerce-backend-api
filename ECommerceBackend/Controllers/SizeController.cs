using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _service;

        public SizeController(ISizeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var sizes = await _service.GetAllAsync();
                return Ok(new ResponseModel<IEnumerable<SizeDto>> { Success = true, Data = sizes });
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
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var size = await _service.GetByIdAsync(id);
                return Ok(new ResponseModel<SizeDto> { Success = true, Data = size });
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
        public async Task<ActionResult> Create([FromBody] CreateSizeDto dto)
        {
            try
            {
                await _service.CreateAsync(dto);
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
        public async Task<ActionResult> Update(int id, [FromBody] UpdateSizeDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return NotFound(new BaseResponseModel { Success = false });
                }

                await _service.UpdateAsync(dto);
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
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
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
