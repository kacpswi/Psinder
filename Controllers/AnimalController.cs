using Microsoft.AspNetCore.Mvc;
using Psinder.Dtos.AnimalDtos;
using Psinder.Services.Interfaces;

namespace Psinder.Controllers
{
    [ApiController]
    [Route("api/shelter/{shelterId}/animal")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;
        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet("{animalId}")]
        public async Task<ActionResult<AnimalDto>> GetAnimal([FromRoute] int shelterId, [FromRoute] int animalId)
        {
            var result = await _animalService.GetByIdAsync(shelterId, animalId);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<ActionResult<List<AnimalDto>>> GetAnimalsFromShelter([FromRoute] int shelterId)
        {
            var results = await _animalService.GetAllForShelter(shelterId);
            return Ok(results);
        }

        //[Route("api/animals")]
        //public async Task<ActionResult<List<AnimalDto>>> GetAnimals()
        //{
        //    var results = await _animalService.GetAllAsync();
        //    return Ok(results);
        //}

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateAnimalDto dto, [FromRoute] int shelterId)
        {
            var id = await _animalService.AddAsync(shelterId, dto);
            return Created($"/api/shelter/{shelterId}/animal/{id}",null);
        }

        [HttpDelete("{animalId}")]
        public async Task<ActionResult> Delete([FromRoute] int shelterId, [FromRoute] int animalId)
        {
            await _animalService.RemoveByIdAsync(shelterId, animalId);
            return NoContent();
        }

        [HttpPut("{animalId}")]
        public async Task<ActionResult> Edit([FromRoute] int shelterId, [FromRoute] int animalId, [FromBody]UpdateAnimalDto dto)
        {
            await _animalService.UpdateAsync(shelterId, animalId, dto);
            return Ok();
        }

    }
}
