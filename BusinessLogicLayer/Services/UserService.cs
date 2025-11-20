using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<ResponseModel<IEnumerable<UserDto>>> GetUsersAsync(RequestUserDto request)
        {
            try
            {
                var query = await _repo.GetUsersAsync(request.SearchTerm, request.RoleId);

                var queddry = await _repo.GetUsers();

                var totalCount = await query.CountAsync();
                int totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

                if (request.PageNumber > totalPages)
                    request.PageNumber = totalPages > 0 ? totalPages : 1;

                var users = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var items = users.Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Surname=u.Surname,
                    CreatedAt=u.CreatedAt,
                    Email = u.Email,
                    EmailConfirmed = u.EmailConfirmed,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                }).ToList();

                return new ResponseModel<IEnumerable<UserDto>>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    Success = true,
                    Data = items
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<IEnumerable<UserDto>>
                {
                    Success = false,
                    ErrorMassage = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
           
                var user = await _repo.GetByIdAsync(id);
                
                return new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    CreatedAt = user.CreatedAt,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    Roles = user.UserRoles.Select(r => r.Role.Name).ToList()
                };

              
           
        }

        public async Task CreateUserAsync(CreateUserDto dto)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Surname=dto.Surname,
                    CreatedAt=DateTime.Now,
                    Password = hashedPassword,
                    EmailConfirmed = false
              };

                await _repo.CreateAsync(user);

        }

        public async Task UpdateUserAsync(UpdateUserDto dto)
        {
            var user = await _repo.GetByIdAsync(dto.Id);

            user.Surname = dto.Surname;
            user.Name = dto.Name;
            user.EmailConfirmed = dto.EmailConfirmed;
     

                await _repo.UpdateAsync(user);

           
        }

        public async Task DeleteUserAsync(int id)
        {
           
                await _repo.DeleteAsync(id);
        
        }

        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
           
                var roles = await _repo.GetRolesAsync();
                return roles.Select(r => new RoleDto { Id = r.Id, Name = r.Name }).ToList();

        }


        public async Task<IEnumerable<UserRoleDto>> GetUserRolesAsync(int UserId)
        {
            var UserRoles = await _repo.GetUserRolesAsync(UserId);

            return UserRoles.Select(r => new UserRoleDto { Id = r.Id, RoleName = r.Role.Name , Email = r.User.Email }).ToList();

        }


        public async Task CreateUserRoleAsync(CreateUserRoleDto dto)
        {

            var userRole = await _repo.GetUserRolesAsync(dto.UserId);

            if (!userRole.Any(s => s.RoleId == dto.RoleId))
            {
                var userrole = new UserRole
                {
                    UserId = dto.UserId, 
                    RoleId = dto.RoleId
                };

                await _repo.CreateUserRolesAsync(userrole);
            }

        }



        public async Task DeleteUserRoleAsync(int id)
        {

            await _repo.DeleteUserRolesAsync(id);

        }




    }
}
