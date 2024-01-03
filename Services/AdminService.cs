using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Psinder.Data;
using Psinder.Dtos.ShelterDtos;
using Psinder.Exceptions;
using Psinder.Helpers;
using Psinder.Repositories;
using Psinder.Repositories.Interfaces;
using Psinder.Services.Interfaces;
using System.Linq.Expressions;

namespace Psinder.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _uow;
        public AdminService(UserManager<User> userManager, IUnitOfWork uow)
        {
            _userManager = userManager;
            _uow = uow;
        }
        public async Task EditUserRolesAsync(int userId, string roles)
        {
            if (string.IsNullOrEmpty(roles))
            {
                throw new BadRequestException("You must select at least one role");
            }

            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new NotFoundException("User doesn't exist");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
            {
                throw new BadRequestException("Failed to add to roles");
            }

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
            {
                throw new BadRequestException("Failed to remove from roles");
            }
        }

        public async Task<PagedResult<User>> GetUsersAsync(PageQuery query)
        {
            var pagination = await _uow.UserRepository.GetUsersAsync(query);

            var result = new PagedResult<User>(pagination.Data, pagination.TotalCount, query.PageSize, query.PageNumber);

            return result;
        }

        public async Task<IEnumerable<object>> GetUsersWithRolesAsync()
        {
            var users = await _userManager.Users
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                })
                .ToListAsync();

            return users;
        }
    }
}
