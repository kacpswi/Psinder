using Microsoft.EntityFrameworkCore;
using Psinder.Data;
using Psinder.Repositories.Interfaces;

namespace Psinder.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly PsinderDb _context;
        public LikeRepository(PsinderDb context)
        {
            _context = context;
        }

        public async Task<UserLike> GetUserLikeAsync(int userId, int animalId)
        {
            return await _context.Likes.FindAsync(userId, animalId);
        }

        public async Task<List<Animal>> GetUserLikesAsync(int userId)
        {
            var animals = _context.Animals.OrderBy( a => a.Name).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            likes = likes.Where(like=>like.UserId == userId);
            animals = likes.Select(like => like.Animal);

            return await animals.ToListAsync();
        }

        public async Task<User> GetUserWithLikesAsync(int userId)
        {
            return await _context.Users
                .Include(x => x.LikedAnimals)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
