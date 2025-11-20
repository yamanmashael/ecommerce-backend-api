using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IUserService
    {
        Task<ResponseModel<IEnumerable<UserDto>>> GetUsersAsync(RequestUserDto request);
        Task<UserDto> GetUserByIdAsync(int id);
        Task CreateUserAsync(CreateUserDto dto);
        Task UpdateUserAsync(UpdateUserDto dto);
        Task DeleteUserAsync(int id);

        Task<IEnumerable<RoleDto>> GetRolesAsync();

        Task<IEnumerable<UserRoleDto>> GetUserRolesAsync(int UserId);
        Task CreateUserRoleAsync(CreateUserRoleDto dto);
        Task DeleteUserRoleAsync(int id);
    }
}
