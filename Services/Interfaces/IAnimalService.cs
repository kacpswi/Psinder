using Psinder.Dtos;
using Psinder.Dtos.AnimalDtos;
using Psinder.Helpers;

namespace Psinder.Services.Interfaces
{
    public interface IAnimalService
    {
        Task<int> AddAsync(int shelterId, CreateAnimalDto dto);
        Task<AnimalDto> GetByIdAsync(int shelterId, int animalId);
        Task RemoveByIdAsync(int shelterId, int animalId);
        Task UpdateAsync(int shelterId, int animalId, UpdateAnimalDto dto);
        Task<List<AnimalDto>> GetAllForShelter(int shelterId);
        Task<PagedResult<AnimalDto>> GetAllAsync(PageQuery query);
    }
}
