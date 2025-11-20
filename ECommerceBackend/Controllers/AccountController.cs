using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace ECommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        public AccountController(IAccountService service)
        {
            _service = service;
        }

        protected int GetUserId()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(id);
            return userId;
        }

        [Authorize]
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDto model)
        {
            try
            {
                var result = await _service.UpdatePasswordAsync(GetUserId(), model);

                if (!result)
                {
                    return BadRequest(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "Invalid data submission"
                    });
                }

                return Ok(new ResponseModel<object> { Success = true });
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

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            try
            {
                var result = await _service.ConfirmEmailAsync(email, token);

                if (!result)
                {
                    return BadRequest(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "Invalid token or user"
                    });
                }

                return Ok(new ResponseModel<object> { Success = true });
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

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto dto)
        {
            if (string.IsNullOrEmpty(dto.IdToken))
                return BadRequest(new { ErrorMassage = "Token is missing" });

            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken);
                var result = await _service.GoogleLoginAsync(payload);

                return Ok(new ResponseModel<LoginResponseDto>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<LoginResponseDto>
                {
                    Success = false,
                    ErrorMassage = "Invalid Google token: " + ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var result = await _service.LoginAsync(loginDto);

                if (result == null)
                {
                    return BadRequest(new ResponseModel<LoginResponseDto>
                    {
                        Success = false,
                        ErrorMassage = "Invalid username, password, or account not confirmed"
                    });
                }

                return Ok(new ResponseModel<LoginResponseDto>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<LoginResponseDto>
                {
                    Success = false,
                    ErrorMassage = "Unexpected error occurred: " + ex.Message
                });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var result = await _service.RegisterAsync(registerDto);

                if (!result)
                {
                    return BadRequest(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "User already exists"
                    });
                }

                return Ok(new ResponseModel<object> { Success = true });
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

        [HttpPost("loginbyrefreshtoken")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDto>> LoginByRefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            if (string.IsNullOrEmpty(request?.RefreshToken))
            {
                return BadRequest("Refresh token is required.");
            }

            var response = await _service.LoginByRefreshToken(request.RefreshToken);

            if (response == null)
            {
                return Unauthorized("Invalid or expired refresh token.");
            }

            return Ok(response);
        }

        [Authorize]
        [HttpPut("updateprofile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfile updateProfile)
        {
            try
            {
                var result = await _service.UpdateProfileAsync(GetUserId(), updateProfile);

                if (!result)
                {
                    return BadRequest(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "Profile update failed"
                    });
                }

                return Ok(new ResponseModel<object> { Success = true });
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

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserById()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    throw new Exception("User not authenticated");
                }

                int id = GetUserId();
                var result = await _service.GetUserByIdAsync(id);

                if (result == null)
                {
                    return BadRequest(new ResponseModel<AccountDto>
                    {
                        Success = false,
                        ErrorMassage = "User not found or account not confirmed"
                    });
                }

                return Ok(new ResponseModel<AccountDto>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<AccountDto>
                {
                    Success = false,
                    ErrorMassage = "Unexpected error occurred: " + ex.Message
                });
            }
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var result = await _service.ForgotPasswordAsync(forgotPasswordDto);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var result = await _service.ResetPassworAsync(resetPasswordDto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
