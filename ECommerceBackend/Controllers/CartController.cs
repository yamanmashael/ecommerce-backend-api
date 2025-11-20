using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
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
        [HttpPost("migrate")]
        public async Task<IActionResult> MigrateGuestCartToUserCart([FromBody] CartMigrationRequest request)
        {
            if (request == null || request.Items == null || !request.Items.Any())
                return BadRequest("No items found to migrate.");

            int  userId = GetUserId();
            if (userId == null)
                return Unauthorized("User not authenticated.");

            var cartmagration = await _service.MigrateCart(request, userId);

            if (cartmagration == false)
            {
                return BadRequest("No migration.");

            }


            return Ok(new ResponseModel<string>
            {
                Success = true,
                Data = "Cart migrated successfully."
            });
        }




        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetCart()
        {
            try
            {
                var cart = await _service.GetCartItemsAsync(GetUserId());
                return Ok(new ResponseModel<CartDto>
                {
                    Success = true,
                    Data = cart
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<CartDto>
                {
                    Success = false,
                    ErrorMassage = "An unexpected error occurred: " + ex.Message
                });
            }
        }

        [HttpPost("guest-cart")]
        public async Task<IActionResult> GetGuestCart([FromBody] List<LocalCartItem> localCarts)
        {
            try
            {
                var cart = await _service.GetGuestCart(localCarts);
                return Ok(new ResponseModel<CartDto>
                {
                    Success = true,
                    Data = cart
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<CartDto>
                {
                    Success = false,
                    ErrorMassage = "An unexpected error occurred: " + ex.Message
                });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddItemToCart(AddToCartDto addToCartDto)
        {
            try
            {
                var result = await _service.AddItemToCartAsync(GetUserId(), addToCartDto);
                if (!result)
                {
                    return Ok(new ResponseModel<object> { Success = false });
                }
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

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateItemQuantity(UpdateCartItemQuantityDto quantityDto)
        {
            try
            {
                bool response = await _service.UpdateCartItemQuantityAsync(quantityDto);
                if (!response)
                {
                    return Ok(new ResponseModel<object> { Success = false });
                }
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

        [Authorize]
        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> DeleteCartItem(int cartItemId)
        {
            try
            {
                await _service.DeleteCartItemAsync(cartItemId);
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

        [Authorize]
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCartItems(int cartId)
        {
            try
            {
                await _service.ClearCartItemAsync(cartId);
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

        [HttpPost("stock")]
        public async Task<IActionResult> CheckStockCart(CheckStoc checkStoc)
        {
            try
            {
                int result = await _service.CheckStockAsync(checkStoc);
                return Ok(new ResponseModel<object>
                {
                    Success = true,
                    Data = result
                });
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

        [Authorize]
        [HttpGet("quantity")]
        public async Task<ActionResult> GetCartQuantity()
        {
            int cart = await _service.CartQuantityAsync(GetUserId());
            return Ok(new ResponseModel<int>
            {
                Success = true,
                Data = cart
            });
        }
    }
}
