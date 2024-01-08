using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psinder.Dtos.UserDtos;
using Psinder.Services.Interfaces;

namespace Psinder.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDto dto)
        {
            await _accountService.RegisterUserAsync(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDto dto)
        {
            var result = await _accountService.LoginUserAsync(dto);
            return Ok(result);
        }
    }
}
