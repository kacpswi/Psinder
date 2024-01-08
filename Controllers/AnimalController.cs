using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psinder.Dtos.AnimalDtos;
using Psinder.Extensions;
using Psinder.Helpers;
using Psinder.Services.Interfaces;

namespace Psinder.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;
        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [AllowAnonymous]
        [HttpGet("shelter/{shelterId}/animal/{animalId}")]
        public async Task<ActionResult<AnimalDto>> GetAnimal([FromRoute] int shelterId, [FromRoute] int animalId)
        {
            var result = await _animalService.GetByIdAsync(shelterId, animalId);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("shelter/{shelterId}/animals")]
        public async Task<ActionResult<List<AnimalDto>>> GetAnimalsFromShelter([FromRoute] int shelterId)
        {
            var results = await _animalService.GetAllForShelter(shelterId);
            return Ok(results);
        }

        [AllowAnonymous]
        [Route("animals")]
        [HttpGet()]
        public async Task<ActionResult<List<AnimalDto>>> GetAnimals([FromQuery] PageQuery query)
        {
            var results = await _animalService.GetAllAsync(query);
            return Ok(results);
        }

        [Authorize(Roles = "ShelterWorker, ShelterOwner")]
        [HttpPost("shelter/{shelterId}/animal")]
        public async Task<ActionResult> Create([FromBody] CreateAnimalDto dto, [FromRoute] int shelterId)
        {
            var id = await _animalService.AddAsync(shelterId, dto, User.GetUserId());
            return Created($"/api/shelter/{shelterId}/animal/{id}",null);
        }

        [Authorize(Roles = "ShelterWorker, ShelterOwner")]
        [HttpDelete("shelter/{shelterId}/animal/{animalId}")]
        public async Task<ActionResult> Delete([FromRoute] int shelterId, [FromRoute] int animalId)
        {
            await _animalService.RemoveByIdAsync(shelterId, animalId, User.GetUserId());
            return NoContent();
        }

        [Authorize(Roles = "ShelterWorker, ShelterOwner")]
        [HttpPut("shelter/{shelterId}/animal/{animalId}")]
        public async Task<ActionResult> Edit([FromRoute] int shelterId, [FromRoute] int animalId, [FromBody]UpdateAnimalDto dto)
        {
            await _animalService.UpdateAsync(shelterId, animalId, dto, User.GetUserId());
            return Ok();
        }

    }
}
