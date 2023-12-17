using Psinder.Data;

namespace Psinder.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<object>> GetUsersWithRolesAsync();
        Task EditUserRolesAsync(int userId, string roles);
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
