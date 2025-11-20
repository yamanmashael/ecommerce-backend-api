using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public  interface IAccountRepository
    {
        Task RegisterAsync(User user);
        Task AddRefreshTokenAsync(RefreshToken refreshToken);
        Task RemoveRefreshTokenAsync(int userId);
        Task<RefreshToken> GetByRefreshTokenAsync(String refreshToken); 

        Task<bool> ExistsAsync(string Email);
        Task<User> GetByEmailAsync(string Email);
        Task PasswordToken(string Email);
        Task<User> GetByResetTokenAsync(string Token);
        Task UpdateUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);

    }
}
