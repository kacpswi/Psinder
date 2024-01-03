using Microsoft.AspNetCore.Mvc;
using Psinder.Dtos.AnimalDtos;
using Psinder.Helpers;
using Psinder.Services.Interfaces;

namespace Psinder.Controllers
{
    [ApiController]
    [Route("api")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;
        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet("shelter/{shelterId}/animal/{animalId}")]
        public async Task<ActionResult<AnimalDto>> GetAnimal([FromRoute] int shelterId, [FromRoute] int animalId)
        {
            var result = await _animalService.GetByIdAsync(shelterId, animalId);
            return Ok(result);
        }

        [HttpGet("shelter/{shelterId}/animal")]
        public async Task<ActionResult<List<AnimalDto>>> GetAnimalsFromShelter([FromRoute] int shelterId)
        {
            var results = await _animalService.GetAllForShelter(shelterId);
            return Ok(results);
        }

        [Route("animals")]
        [HttpGet()]
        public async Task<ActionResult<List<AnimalDto>>> GetAnimals([FromQuery] PageQuery query)
        {
            var results = await _animalService.GetAllAsync(query);
            return Ok(results);
        }

        [HttpPost("shelter/{shelterId}/animal")]
        public async Task<ActionResult> Create([FromBody] CreateAnimalDto dto, [FromRoute] int shelterId)
        {
            var id = await _animalService.AddAsync(shelterId, dto);
            return Created($"/api/shelter/{shelterId}/animal/{id}",null);
        }

        [HttpDelete("shelter/{shelterId}/animal/{animalId}")]
        public async Task<ActionResult> Delete([FromRoute] int shelterId, [FromRoute] int animalId)
        {
            await _animalService.RemoveByIdAsync(shelterId, animalId);
            return NoContent();
        }

        [HttpPut("shelter/{shelterId}/animal/{animalId}")]
        public async Task<ActionResult> Edit([FromRoute] int shelterId, [FromRoute] int animalId, [FromBody]UpdateAnimalDto dto)
        {
            await _animalService.UpdateAsync(shelterId, animalId, dto);
            return Ok();
        }

    }
}
