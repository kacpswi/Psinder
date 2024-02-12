using Psinder.Data;
using Psinder.Dtos.UserDtos;
using Psinder.Helpers;

namespace Psinder.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<Pagination<User>> GetUsersAsync(PageQuery query);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
    }
}
