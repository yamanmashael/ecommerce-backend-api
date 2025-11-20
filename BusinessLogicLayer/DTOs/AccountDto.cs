using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{



    public class ForgotPasswordDto
    {
        public string Email { get; set; }
    }


    public  class AccountDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }


    public class UpdateProfile
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }


    public class UpdatePasswordDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

    }



    public class ResetPasswordDto
    {
        public string Token { get; set; }
        public string Password { get; set; }

    }


    public class LoginResponseDto
    {
        public string Token { get; set; }
        public long TokenExpired { get; set; }
        public string RefreshToken { get; set; }
        public string Role { get; set; }

    }

    public class GoogleLoginDto
    {
        public string IdToken { get; set; }
    }


    public class LoginDto
    {
        public String UserName { get; set; }
        public String Password { get; set; }
    }



    public class GoogleLoginRequest
    {
        public string IdToken { get; set; }

    }
















}
