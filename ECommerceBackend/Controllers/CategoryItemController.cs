using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryItemController : ControllerBase
    {
        private readonly ICategoryItemService _service;

        public CategoryItemController(ICategoryItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var items = await _service.GetAllAsync();
                return Ok(new ResponseModel<IEnumerable<CategoryItemDto>> { Success = true, Data = items });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<object> { Success = false, ErrorMassage = "An unexpected error occurred: " + ex.Message });
            }
        }

        [HttpGet("CategoryItemByCategory/{CategoryId}")]
        public async Task<ActionResult> GetCategoryItemByCategory(int CategoryId)
        {
            try
            {
                var items = await _service.GetCategoryItemsByCategory(CategoryId);
                return Ok(new ResponseModel<IEnumerable<CategoryItemDto>> { Success = true, Data = items });
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
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                    return NotFound(new ResponseModel<object> { Success = false, ErrorMassage = "No data found" });

                return Ok(new ResponseModel<CategoryItemDto> { Success = true, Data = item });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<object> { Success = false, ErrorMassage = "An unexpected error occurred: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCategoryItemDto dto)
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
        public async Task<ActionResult> Update(int id, [FromBody] UpdateCategoryItemDto dto)
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
