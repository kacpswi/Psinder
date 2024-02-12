using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psinder.Data;
using Psinder.Dtos.ShelterDtos;
using Psinder.Extensions;
using Psinder.Helpers;
using Psinder.Services.Interfaces;

namespace Psinder.Controllers
{
    [ApiController]
    [Route("api/shelter")]
    [Authorize]
    public class ShelterController : ControllerBase
    {
        private readonly IShelterService _shelterService;
        public ShelterController(IShelterService shelterService)
        {
            _shelterService = shelterService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ShelterDto>> GetShelter([FromRoute]int id)
        {
            var result = await _shelterService.GetByIdAsync(id);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PagedResult<ShelterDto>>> GetAll([FromQuery]PageQuery query)
        {
            var result = await _shelterService.GetAllAsync(query);
            return Ok(result);
        }

        [Authorize( Roles = "ShelterOwner")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateShelterDto dto)
        {
            var id = await _shelterService.AddAsync(dto, User.GetUserId());
            return Created($"/api/shelter/{id}", null);
        }

        [Authorize(Roles = "ShelterOwner")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute]int id)
        {
            await _shelterService.DeleteAsync(id, User.GetUserId());
            return NoContent();
        }

        [Authorize(Roles = "ShelterOwner")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromBody] UpdateShelterDto dto)
        {
            var result = await _shelterService.UpdateAsync(id, dto, User.GetUserId());
            return Ok(result);
        }
    }
}