using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IUserRepository
    {
        Task<IQueryable<User>> GetUsersAsync(string searchTerm, int? roleId);
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetByIdAsync(int id);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);

        Task<List<Role>> GetRolesAsync();

        Task<List<UserRole>> GetUserRolesAsync(int UserId);
        Task CreateUserRolesAsync(UserRole userRole);
        Task DeleteUserRolesAsync(int Id);

    }


}
