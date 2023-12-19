using Psinder.Data;

namespace Psinder.Repositories.Interfaces
{
    public interface ILikeRepository
    {
        Task<User> GetUserWithLikesAsync(int userId);
        Task<UserLike> GetUserLikeAsync(int userId, int animalId);
        Task<List<Animal>> GetUserLikesAsync(int userId);
    }
}
