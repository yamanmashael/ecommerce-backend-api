using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<User>> GetUsers()
        {
            var query = await  _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();

            return query;
        }

        public async Task<IQueryable<User>> GetUsersAsync(string searchTerm, int? roleId)
        {
            var query = _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(u => u.Name.Contains(searchTerm) || u.Email.Contains(searchTerm));

            if (roleId.HasValue)
                query = query.Where(u => u.UserRoles.Any(r => r.RoleId == roleId.Value));

            return await Task.FromResult(query);
        }



        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task CreateAsync(User user)
        {

          
            if (!  _context.Users.Any(x => x.Email == user.Email))
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
   

        
        }

        public async Task UpdateAsync(User user)
        {

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new Exception("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<List<UserRole>> GetUserRolesAsync(int UserId)
        {
            var userRoles = await _context.UserRoles
                .Include(u => u.User)
                .Include(r => r.Role)
                .Where(s => s.UserId == UserId)
                .ToListAsync();

            return userRoles;
        }

        public async Task CreateUserRolesAsync(UserRole userRole)
        {



             _context.UserRoles.Add(userRole);    
            await _context.SaveChangesAsync();  
        }

        public async Task DeleteUserRolesAsync(int Id)
        {
           var userrole = await _context.UserRoles.FindAsync(Id);   
            _context.UserRoles.Remove(userrole);    
            await _context.SaveChangesAsync();  
        }

    }
}
