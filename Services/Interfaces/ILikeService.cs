using Psinder.Data;
using Psinder.Dtos.AnimalDtos;

namespace Psinder.Services.Interfaces
{
    public interface ILikeService
    {
        Task AddLikeAsync(int animalId, int userId);
        Task<List<AnimalDto>> GetLikesAsync(int userId);
    }
}
