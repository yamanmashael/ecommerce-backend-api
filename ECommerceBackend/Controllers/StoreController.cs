//using BusinessLogicLayer.DTOs;
//using BusinessLogicLayer.Services;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace ECommerceBackend.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StoreController : ControllerBase
//    {



//        private readonly IStoreService _service;
//        private readonly IWebHostEnvironment _env;

//        public StoreController(IStoreService service, IWebHostEnvironment env)
//        {
//            _env = env;
//            _service = service;
//        }

//        [HttpPost("Stores")]
//        public async Task<IActionResult> GetStores(ProductSearchDto searchDto)
//        {
//            try
//            {
//                var store = await _service.GetStoresAsync(searchDto);
//                return Ok(new BaseResponseModel { Success = true, Data = store });
//            }
//            catch (KeyNotFoundException ex)
//            {
//                return NotFound(new BaseResponseModel { Success = false, Data = ex.Message });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new BaseResponseModel { Success = false, ErrorMassage = ex.Message });
//            }

//        }



//        [HttpGet("GetStorById")]
//        public async Task<IActionResult> GetStorById(int id)
//        {
//            try
//            {
//                var product = await _service.GetStoreByIdAsync(id);
//                return Ok(new BaseResponseModel { Success = true, Data = product });

//            }
//            catch (KeyNotFoundException ex)
//            {
//                return NotFound(new BaseResponseModel { Data = ex.Message, Success = false });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new BaseResponseModel { Success = false, ErrorMassage = ex.Message });

//            }

//        }








//    }
//}
