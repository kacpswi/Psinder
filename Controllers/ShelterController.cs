using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psinder.Data;
using Psinder.Dtos.ShelterDtos;
using Psinder.Helpers;
using Psinder.Services.Interfaces;

namespace Psinder.Controllers
{
    [ApiController]
    [Route("api/shelter")]
    public class ShelterController : ControllerBase
    {
        private readonly IShelterService _shelterService;
        public ShelterController(IShelterService shelterService)
        {
            _shelterService = shelterService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShelterDto>> GetShelter([FromRoute]int id)
        {
            var result = await _shelterService.GetByIdAsync(id);
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ShelterDto>>> GetAll([FromQuery]PageQuery query)
        {
            var result = await _shelterService.GetAllAsync(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateShelterDto dto)
        {
            var id = await _shelterService.AddAsync(dto);
            return Created($"/api/shelter/{id}", null);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute]int id)
        {
            await _shelterService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromBody] UpdateShelterDto dto)
        {
            await _shelterService.UpdateAsync(id, dto);
            return Ok();
        }
    }
}