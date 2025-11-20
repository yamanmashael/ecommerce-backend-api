using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers([FromQuery] RequestUserDto request)
        {
            var response = await _service.GetUsersAsync(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _service.GetUserByIdAsync(id);
                return Ok(new ResponseModel<UserDto> { Success = true, Data = response });
            }
            catch (Exception ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new ResponseModel<object> { Success = false, ErrorMassage = error });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            try
            {
                await _service.CreateUserAsync(dto);
                return Ok(new ResponseModel<object> { Success = true });
            }
            catch (Exception ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new ResponseModel<object> { Success = false, ErrorMassage = error });
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
        {
            try
            {
                await _service.UpdateUserAsync(dto);
                return Ok(new ResponseModel<object> { Success = true });
            }
            catch (Exception ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new ResponseModel<object> { Success = false, ErrorMassage = error });
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteUserAsync(id);
                return Ok(new ResponseModel<object> { Success = true });
            }
            catch (Exception ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new ResponseModel<object> { Success = false, ErrorMassage = error });
            }
        }

        [HttpGet("Roles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var roles = await _service.GetRolesAsync();
                return Ok(new ResponseModel<IEnumerable<RoleDto>> { Success = true, Data = roles });
            }
            catch (Exception ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new ResponseModel<object> { Success = false, ErrorMassage = error });
            }
        }

        [HttpGet("user_Role/{UserId}")]
        public async Task<ActionResult> GetAllUserRole(int UserId)
        {
            try
            {
                var response = await _service.GetUserRolesAsync(UserId);
                return Ok(new ResponseModel<IEnumerable<UserRoleDto>> { Success = true, Data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<object> { Success = false, ErrorMassage = "An unexpected error occurred: " + ex.Message });
            }
        }

        [HttpPost("User_Role")]
        public async Task<IActionResult> CreateUserRole([FromBody] CreateUserRoleDto dto)
        {
            try
            {
                await _service.CreateUserRoleAsync(dto);
                return Ok(new ResponseModel<object> { Success = true });
            }
            catch (Exception ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new ResponseModel<object> { Success = false, ErrorMassage = error });
            }
        }

        [HttpDelete("User_Role/{id}")]
        public async Task<IActionResult> DeleteUserRole(int id)
        {
            try
            {
                await _service.DeleteUserRoleAsync(id);
                return Ok(new ResponseModel<object> { Success = true });
            }
            catch (Exception ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new ResponseModel<object> { Success = false, ErrorMassage = error });
            }
        }
    }
}
