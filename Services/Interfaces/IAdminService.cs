using Psinder.Data;
using Psinder.Helpers;

namespace Psinder.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<object>> GetUsersWithRolesAsync();
        Task EditUserRolesAsync(int userId, string roles);
        Task<PagedResult<User>> GetUsersAsync(PageQuery query);
    }
}
