using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Identity.Client;
using System.Net;
using Google.Apis.Auth;
using Microsoft.Win32;


namespace BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _repository; 
        private readonly EmailService _emailService;    
        public AccountService(IConfiguration configuration, IAccountRepository repository, EmailService emailService)
        {
            _configuration = configuration; 
            _repository = repository;   
            _emailService = emailService;   
        }




        public async Task<bool> UpdatePasswordAsync(int userId, UpdatePasswordDto updatePasswordDto)
        {
            var user = await _repository.GetUserByIdAsync(userId);

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(updatePasswordDto.CurrentPassword, user.Password);
            if (isPasswordValid == true)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(updatePasswordDto.NewPassword);
                user.Password = hashedPassword;

                await _repository.UpdateUserAsync(user);
                return true;

            }
            return false;
        }



        public async Task<bool> ConfirmEmailAsync(string Email, string token)
        {
            var user = await _repository.GetByEmailAsync(Email);
             if(user != null && user.EmailConfirmationToken != token)
            {
                return false;
            }
                
            user.EmailConfirmed = true;
            user.EmailConfirmationToken = null;

            await _repository.UpdateUserAsync(user);
            return true;
        }




        public async Task<LoginResponseDto> GoogleLoginAsync(GoogleJsonWebSignature.Payload payload)
        {
            var existing = await _repository.ExistsAsync(payload.Email);

            if (existing == false)
            {
                var password = Guid.NewGuid().ToString();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var register = new User
                {
                    Email = payload.Email,
                    Name = payload.Name,
                    Surname = payload.FamilyName,
                    CreatedAt = DateTime.Now,
                    Password = hashedPassword,
                    EmailConfirmed=true
                };

                var subject = "Your Account Password";
                var body = $"Hello {register.Name},\n\n Your account has been successfully created.\n\nHere is your password: {password}";
                await _emailService.SendEmail(payload.Email, subject, body);
           
                await _repository.RegisterAsync(register);

            }

            var user = await _repository.GetByEmailAsync(payload.Email);

            var token = GenerateJWToken(user, isRefreshToken: false);
            var refreshToken = GenerateJWToken(user, isRefreshToken: true);

            await _repository.RemoveRefreshTokenAsync(user.Id);
            await _repository.AddRefreshTokenAsync(new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id
            });

            return new LoginResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                TokenExpired = DateTimeOffset.UtcNow.AddMinutes(1).ToUnixTimeSeconds()
                 //TokenExpired = DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds()


            };
        }








        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {



            var user = await _repository.GetByEmailAsync(loginDto.UserName);

            if (user == null) { return null; }
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password)) { return null; }
            if (!user.EmailConfirmed) { return null; }
         
            var token = GenerateJWToken(user , isRefreshToken:false );
            var RefreshToken = GenerateJWToken(user , isRefreshToken:true);
            await _repository.RemoveRefreshTokenAsync(user.Id);
            var refreshtoken = new RefreshToken
            {
                Token = RefreshToken,
                UserId = user.Id,   
            };
            await _repository.AddRefreshTokenAsync(refreshtoken);

            return new LoginResponseDto
            {
                Token= token,
                RefreshToken= RefreshToken,
                TokenExpired = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds(),
                Role= user.UserRoles.FirstOrDefault()?.Role.Name 

                //TokenExpired = DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds()

            };
        }




        public async Task<LoginResponseDto> LoginByRefreshToken(string refreshToken)
        {

            var refreshtoken = await _repository.GetByRefreshTokenAsync(refreshToken);
            if (refreshtoken == null)
            {
                return null;
            }
     
            var newtoken = GenerateJWToken(refreshtoken.User, isRefreshToken: false);
            var newRefreshToken = GenerateJWToken(refreshtoken.User , isRefreshToken: true);

            await _repository.RemoveRefreshTokenAsync(refreshtoken.UserId);
            var addrefreshtoken = new RefreshToken
            {
                Token = newRefreshToken,
                UserId = refreshtoken.UserId
            };
            await _repository.AddRefreshTokenAsync(addrefreshtoken);

            return new LoginResponseDto
            {
                Token = newtoken,
                RefreshToken = newRefreshToken,
                TokenExpired = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds()
                //TokenExpired = DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds()
            };
        }




        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {

            var existing = await _repository.ExistsAsync(registerDto.Email);

            if (existing == true) { return false; }
         
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
                var user = new User
                {
                    Name = registerDto.Name,
                    Email = registerDto.Email,
                    Password = hashedPassword,
                    EmailConfirmationToken= Guid.NewGuid().ToString(),
                    EmailConfirmed=false,
                    Surname = registerDto.Surname,
                    CreatedAt = DateTime.Now,

                };
                await _repository.RegisterAsync(user);

                var subject = "Account verification";
            var confirmationLink = $"https://yaman-store-htbme5b5frefghag.indonesiacentral-01.azurewebsites.net/confirmemail?token={user.EmailConfirmationToken}&email={user.Email}";
            await _emailService.SendEmail(registerDto.Email, subject, confirmationLink);

            return true;
        }






        public async Task<bool> UpdateProfileAsync(int userId, UpdateProfile updatePrfile)
        {

            var user = await _repository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.Name = updatePrfile.Name;
            user.Surname = updatePrfile.Surname;
            user.Email = updatePrfile.Email;

            await _repository.UpdateUserAsync(user);

            return true;
        }







        public async  Task<BaseResponseModel> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _repository.GetByEmailAsync(forgotPasswordDto.Email);

            if (user == null)
            {
                return new BaseResponseModel
                {
                    Success = false,
                    ErrorMassage = "No user found with this email address."
                };
            }

            if (user.ResetToken != null && user.ResetTokenExpires > DateTime.Now)
            {
                return new BaseResponseModel
                {
                    Success = false,
                    ErrorMassage = "A password reset link has already been sent. Please check your email."
                };
            }

            if (user.ResetToken == null && user.ResetTokenExpires < DateTime.Now)
            {
                await _repository.PasswordToken(forgotPasswordDto.Email);
            }

            var subject = "Password Reset Request";
            var resetLink = $"https://yaman-store-htbme5b5frefghag.indonesiacentral-01.azurewebsites.net/resetpassword?token={user.ResetToken}";
            var body = $"Hello {user.Name},\n\nPlease reset your password by clicking the link below:\n{resetLink}";

            await _emailService.SendEmail(forgotPasswordDto.Email, subject, body);

            return new BaseResponseModel
            {
                Success = true
             
            };

        }



        public async  Task<BaseResponseModel> ResetPassworAsync(ResetPasswordDto resetPasswordDto )
        {
            var user = await _repository.GetByResetTokenAsync(resetPasswordDto.Token);
            if (user == null)
            {
                return new BaseResponseModel
                {
                    Success = false,
                    ErrorMassage = "The reset token is invalid or has expired."
                };
            }


            user.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.Password);


            await _repository.UpdateUserAsync(user);

            return new BaseResponseModel
            {
                Success = true

            };

        }







        public async Task<AccountDto> GetUserByIdAsync(int id)
        {
            var getuser =await _repository.GetUserByIdAsync(id);

            var user = new AccountDto
            {
                Name = getuser.Name,
                Surname=getuser.Surname,
                Email = getuser.Email
            };

            return user;
        }





        private string GenerateJWToken(User user, bool isRefreshToken)
        {
            List<Claim> claims = new List<Claim>()
            {
                  new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),

            };

            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }

            string srectKey;

            if (isRefreshToken == true)
            {
                srectKey = _configuration["JwtSettings:RefreshTokenSecret"]!;
            }
            else
            {
                srectKey = _configuration["JwtSettings:Key"]!;
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(srectKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              issuer: _configuration["JwtSettings:Issuer"],
              audience: _configuration["JwtSettings:Audience"],
              claims: claims,
              expires: DateTime.Now.AddHours(isRefreshToken ? 24 * 7 : 1),
              //expires: DateTime.Now.AddMinutes(isRefreshToken ? 24 * 60 * 7 : 5),

              signingCredentials: creds
              );


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    
    }
}




