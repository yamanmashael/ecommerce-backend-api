using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var categories = await _service.GetAllAsync();
                return Ok(new ResponseModel<IEnumerable<CategoryDto>> { Success = true, Data = categories });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<object> { Success = false, ErrorMassage = "An unexpected error occurred: " + ex.Message });
            }
        }

        [HttpGet("categoriesByGender/{genderId}")]
        public async Task<ActionResult> GetCategoryByGender(int genderId)
        {
            try
            {
                var categories = await _service.GetCategoryByGender(genderId);
                return Ok(new ResponseModel<IEnumerable<CategoryDto>> { Success = true, Data = categories });
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
                var category = await _service.GetByIdAsync(id);
                if (category == null)
                    return NotFound(new ResponseModel<object> { Success = false, ErrorMassage = "No data found" });

                return Ok(new ResponseModel<CategoryDto> { Success = true, Data = category });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<object> { Success = false, ErrorMassage = "An unexpected error occurred: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCategoryDto dto)
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
        public async Task<ActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
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
