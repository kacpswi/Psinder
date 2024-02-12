using Psinder.Dtos;
using Psinder.Dtos.AnimalDtos;
using Psinder.Helpers;

namespace Psinder.Services.Interfaces
{
    public interface IAnimalService
    {
        Task<int> AddAsync(int shelterId, CreateAnimalDto dto, int userId);
        Task<AnimalDto> GetByIdAsync(int shelterId, int animalId);
        Task RemoveByIdAsync(int shelterId, int animalId, int userId);
        Task UpdateAsync(int shelterId, int animalId, UpdateAnimalDto dto, int userId);
        Task<List<AnimalDto>> GetAllForShelter(int shelterId);
        Task<PagedResult<AnimalDto>> GetAllAsync(PageQuery query);
    }
}
