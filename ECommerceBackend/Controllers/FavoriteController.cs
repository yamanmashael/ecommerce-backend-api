using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _service;
        public FavoriteController(IFavoriteService service)
        {
            _service = service;  
        }

        protected int UserId()
        {
            var Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(Id);
            return userId;
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorite()
        {
            try
            {
                var favorites = await _service.GetFavoritesAsync(UserId()); 
                return Ok(new BaseResponseModel { Success = true, Data = favorites });

            }
   
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponseModel { Success = false, ErrorMassage = ex.Message });

            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateFavorite(int productItemId)
        {
            try
            {
                await _service.CreateFavoriteAsync(productItemId, UserId());

                return Ok(new BaseResponseModel { Success = true });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponseModel { Success = false, ErrorMassage = ex.Message });

            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFavorite(int favoriteId)
        {

            try
            {
                 await _service.DeleteFavoriteAsync(favoriteId);

                return Ok(new BaseResponseModel { Success = true });

            }
         
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponseModel { Success = false, ErrorMassage = ex.Message });

            }
        }


    }
}
