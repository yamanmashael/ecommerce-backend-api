using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class AccountRepository : IAccountRepository
    {


        public readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context)
        {
            _context = context; 
        }


        public async Task RegisterAsync(User user)
        {

            if (!_context.Roles.Any())
            {
                _context.Roles.AddRange(
                    new Role { Name = "Admin" },
                    new Role { Name = "User" }
                );
                await _context.SaveChangesAsync();
            }


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == "User");

            var userrole = new UserRole
            {
                UserId=user.Id,
                RoleId=role.Id
            };
            await _context.UserRoles.AddAsync(userrole);
            await _context.SaveChangesAsync();

        }


        public async Task<bool> ExistsAsync(string Email)
        {
            return await _context.Users.AnyAsync(e=>e.Email == Email);    
        }



        public async Task<User> GetByEmailAsync(string Email)
        {
            var user = await _context.Users.Include(r => r.UserRoles).ThenInclude(d => d.Role).FirstOrDefaultAsync(e=>e.Email==Email);
            return user;
        }


        public async Task PasswordToken(string Email)
        {
            var user =await _context.Users.FirstOrDefaultAsync(e=>e.Email==Email);

            if (user != null)
            {
             
                    var token = Guid.NewGuid().ToString();
                    user.ResetToken = token;
                    user.ResetTokenExpires = DateTime.Now.AddMinutes(30);
                    await _context.SaveChangesAsync();
                
            }

        }

        public async Task<User> GetByResetTokenAsync(string Token)
        {
           var user = await _context.Users.FirstOrDefaultAsync(x=>x.ResetToken==Token);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {

          _context.Users.Update(user);
         await _context.SaveChangesAsync();


        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(x=>x.UserRoles).ThenInclude(r=>r.Role).FirstOrDefaultAsync(c=>c.Id==id);    
        }

        public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
           _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();  
        }

        public async Task RemoveRefreshTokenAsync(int userId)
        {
           var refreshtoken= await _context.RefreshTokens.FirstOrDefaultAsync(x=>x.UserId==userId);    
         
            if(refreshtoken!=null)
            {

                _context.RefreshTokens.Remove(refreshtoken);
                await _context.SaveChangesAsync();  
            }
        }

        public async Task<RefreshToken> GetByRefreshTokenAsync(string refreshToken)
        {
          return await _context.RefreshTokens.Include(u=>u.User).ThenInclude(ur=>ur.UserRoles).ThenInclude(r=>r.Role).FirstOrDefaultAsync(x=>x.Token == refreshToken);   
        }
    }
}
