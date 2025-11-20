
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenderController : ControllerBase
    {
        private readonly IGenderService _service;

        public GenderController(IGenderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var genders = await _service.GetAllAsync();
                return Ok(new ResponseModel<IEnumerable<GenderDto>> { Success = true, Data = genders });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<object> { Success = false, ErrorMassage = "An unexpected error occurred: " + ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var gender = await _service.GetByIdAsync(id);
                if (gender == null)
                    return NotFound(new ResponseModel<object> { Success = false, ErrorMassage = "No data found" });

                return Ok(new ResponseModel<GenderDto> { Success = true, Data = gender });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<object> { Success = false, ErrorMassage = "An unexpected error occurred: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateGenderDto dto)
        {
            try
            {
                await _service.CreateAsync(dto);
                return Ok(new ResponseModel<object> { Success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<object> { Success = false, ErrorMassage = "An unexpected error occurred: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateGenderDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return Ok(new ResponseModel<object> { Success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<object> { Success = false, ErrorMassage = "An unexpected error occurred: " + ex.Message });
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
                return StatusCode(500, new ResponseModel<object> { Success = false, ErrorMassage = "An unexpected error occurred: " + ex.Message });
            }
        }
    }
}

