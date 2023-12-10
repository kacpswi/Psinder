using Psinder.Data;
using Psinder.Dtos;

namespace Psinder.Services.Interfaces
{
    public interface IShelterService
    {
        Task<IEnumerable<ShelterDto>> GetAllAsync();
        Task<ShelterDto> GetByIdAsync(int id);
        Task<int> AddAsync(CreateShelterDto dto);
        Task UpdateAsync(int id, UpdateShelterDto dto);
        Task DeleteAsync(int id);
    }
}
