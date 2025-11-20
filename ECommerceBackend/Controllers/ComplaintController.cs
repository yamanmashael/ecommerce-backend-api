using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {




        private readonly EmailService _emailService;

        public ComplaintController(EmailService emailService)
        {
           _emailService = emailService;
        }





        [HttpPost]
        public async Task<ActionResult> ComplaintForm(ComplaintFormDto formDto)
        {
            try
            {
                 var emailBody = $@"l
                تم استلام نموذج شكوى جديد من العميل:

                الاسم الكامل: {formDto.FullName}
                البريد الإلكتروني: {formDto.Email}
                رقم الهاتف: {formDto.PhoneNumber}
                نوع الرسالة: {formDto.Complaint}
                رقم الطلب: {(string.IsNullOrWhiteSpace(formDto.OrderNo) ? "غير محدد" : formDto.OrderNo)}

                نص الرسالة:
                {formDto.Body}
                ";

                string subject = $"رسالة جديدة من {formDto.FullName} - {formDto.Complaint}";

                // إرسال البريد (إلى الإدارة مثلاً)
                await _emailService.SendEmail("yamanmashael2020@gmail.com", subject, emailBody);

                // إرسال نسخة للعميل (اختياري)
                await _emailService.SendEmail(formDto.Email, "تم استلام رسالتك", "شكرًا لتواصلك معنا، سيتم الرد عليك قريبًا.");

                return Ok(new ResponseModel<object>
                {
                    Success = true,
               


                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "حدث خطأ أثناء إرسال النموذج", error = ex.Message });
            }



        }





    }
}
