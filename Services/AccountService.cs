using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Psinder.Data;
using Psinder.Dtos.UserDtos;
using Psinder.Exceptions;
using Psinder.Services.Interfaces;
using System.Text;

namespace Psinder.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        public AccountService(IMapper mapper, UserManager<User> userManager, ITokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<string> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginUserDto.Email);

            if (user == null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);

            if (!result)
            {
                throw new BadRequestException("Invalid username or password");
            }

            return await _tokenService.CreateToken(user);
        }

        public async Task RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            if (await UserExists(registerUserDto.Email))
            {
                throw new BadRequestException("Email is taken");
            }
            else if( registerUserDto.Password != registerUserDto.ConfirmPassword )
            {
                throw new BadRequestException("Password and Confirm Password must be the same");
            }

            var user = _mapper.Map<User>(registerUserDto);
            user.UserName = registerUserDto.Email;

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (!result.Succeeded)
            {
                StringBuilder stringBuilder = new StringBuilder();

                foreach (var error in result.Errors)
                {
                    stringBuilder.Append(error.Description);
                    stringBuilder.Append(" ");
                }
                var str = stringBuilder.ToString();
                throw new BadRequestException(str);
            }

            await _userManager.AddToRoleAsync(user, "User");

        }

        private async Task<bool> UserExists(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.NormalizedEmail == email.ToUpper());
        }
    }
}
