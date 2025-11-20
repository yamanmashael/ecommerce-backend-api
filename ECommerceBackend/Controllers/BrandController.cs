using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _service;
        public BrandController(IBrandService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var brand = await _service.GetAllAsync();

                return Ok(new ResponseModel<IEnumerable<BrandDto>>
                {
                    Success = true,
                    Data = brand
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<IEnumerable<BrandDto>>
                {
                    Success = false,
                    ErrorMassage = "Unexpected error occurred: " + ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var brand = await _service.GetByIdAsync(id);

                if (brand == null)
                {
                    return BadRequest(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "No data found"
                    });
                }

                return Ok(new ResponseModel<BrandDto>
                {
                    Success = true,
                    Data = brand
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<object>
                {
                    Success = false,
                    ErrorMassage = "Unexpected error occurred: " + ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateBrandDto dto)
        {
            try
            {
                await _service.CreateAsync(dto);

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
                    ErrorMassage = "Unexpected error occurred: " + ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBrandDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);

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
                    ErrorMassage = "Unexpected error occurred: " + ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);

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
                    ErrorMassage = "Unexpected error occurred: " + ex.Message
                });
            }
        }
    }
}
