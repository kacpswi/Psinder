using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Psinder.Data;
using Psinder.Exceptions;
using Psinder.Services.Interfaces;

namespace Psinder.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;
        public AdminService(UserManager<User> userManager)
        {
            _userManager = userManager;
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

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
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
