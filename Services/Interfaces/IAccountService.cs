using Psinder.Dtos.UserDtos;

namespace Psinder.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> LoginUserAsync(LoginUserDto loginUserDto);
        Task RegisterUserAsync(RegisterUserDto registerUserDto);
    }
}
