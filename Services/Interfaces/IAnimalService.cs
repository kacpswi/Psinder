using Psinder.Dtos;
using Psinder.Dtos.AnimalDtos;

namespace Psinder.Services.Interfaces
{
    public interface IAnimalService
    {
        Task<int> AddAsync(int shelterId, CreateAnimalDto dto);
        Task<AnimalDto> GetByIdAsync(int shelterId, int animalId);
        Task<List<AnimalDto>> GetAllAsync();
        Task RemoveByIdAsync(int shelterId, int animalId);
        Task UpdateAsync(int shelterId, int animalId, UpdateAnimalDto dto);
        Task<List<AnimalDto>> GetAllForShelter(int shelterId);
    }
}
