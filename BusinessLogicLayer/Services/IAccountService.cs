using BusinessLogicLayer.DTOs;
using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public  interface IAccountService
    {
        Task<bool> UpdatePasswordAsync(int userId, UpdatePasswordDto updatePasswordDto);
        Task<bool> ConfirmEmailAsync(string Email, String token);
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        Task<bool> RegisterAsync(RegisterDto registerDto);
        Task<bool> UpdateProfileAsync(int userId, UpdateProfile updatePrfile);
        Task<AccountDto> GetUserByIdAsync(int id);
        Task<LoginResponseDto> LoginByRefreshToken(string refreshToken);
        Task<LoginResponseDto> GoogleLoginAsync(GoogleJsonWebSignature.Payload payload);


        Task<BaseResponseModel> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<BaseResponseModel> ResetPassworAsync(ResetPasswordDto resetPasswordDto);




    }
}
