using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Psinder.Data;
using Psinder.Helpers;
using Psinder.Services.Interfaces;

namespace Psinder.Controllers
{
    [ApiController]
    [Route("api/admin")]
    //[Authorize(Policy = "RequiredAdminRole")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        [Route("users-with-roles")]
        public async Task<ActionResult<object>> GetUsersWithRoles()
        {
            var result = await _adminService.GetUsersWithRolesAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("edit-roles/{userId}")]
        public async Task<ActionResult> EditRoles([FromRoute] int userId, [FromQuery] string roles)
        {
            await _adminService.EditUserRolesAsync(userId, roles);
            return Ok();
        }

        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<object>> GetUsers([FromQuery] PageQuery query)
        {
            var result = await _adminService.GetUsersAsync(query);
            return Ok(result);
        }
    }
}
