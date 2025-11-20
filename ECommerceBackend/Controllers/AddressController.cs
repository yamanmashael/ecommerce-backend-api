using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        protected int GetUserId()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(id);
            return userId;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAddresses()
        {
            try
            {
                var addresses = await _addressService.GetAllAddressesAsync(GetUserId());

                if (addresses == null)
                {
                    return BadRequest(new ResponseModel<IEnumerable<AddressDto>>
                    {
                        Success = false,
                        ErrorMassage = "No data found"
                    });
                }

                return Ok(new ResponseModel<IEnumerable<AddressDto>>
                {
                    Success = true,
                    Data = addresses
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<IEnumerable<AddressDto>>
                {
                    Success = false,
                    ErrorMassage = "Unexpected error occurred: " + ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAddressById(int id)
        {
            try
            {
                var address = await _addressService.GetAddressByIdAsync(id, GetUserId());

                if (address == null)
                {
                    return BadRequest(new ResponseModel<AddressDto>
                    {
                        Success = false,
                        ErrorMassage = "No data found"
                    });
                }

                return Ok(new ResponseModel<AddressDto>
                {
                    Success = true,
                    Data = address
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<AddressDto>
                {
                    Success = false,
                    ErrorMassage = "Unexpected error occurred: " + ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAddress([FromBody] CreateAddressDto createAddressDto)
        {
            try
            {
                var newAddress = await _addressService.CreateAddressAsync(GetUserId(), createAddressDto);

                if (!newAddress)
                {
                    return BadRequest(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "Error during creation"
                    });
                }

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

        [HttpPut]
        public async Task<IActionResult> PutAddress([FromBody] UpdateAddressDto updateAddressDto)
        {
            try
            {
                var result = await _addressService.UpdateAddressAsync(GetUserId(), updateAddressDto);

                if (!result)
                {
                    return BadRequest(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "Error during update"
                    });
                }

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
        public async Task<IActionResult> DeleteAddress(int id)
        {
            try
            {
                var result = await _addressService.DeleteAddressAsync(id, GetUserId());

                if (!result)
                {
                    return BadRequest(new ResponseModel<object>
                    {
                        Success = false,
                        ErrorMassage = "Error during deletion"
                    });
                }

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
