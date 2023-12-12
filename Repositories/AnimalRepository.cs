using Microsoft.EntityFrameworkCore;
using Psinder.Data;
using Psinder.Repositories.Interfaces;

namespace Psinder.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly PsinderDb _context;
        public AnimalRepository(PsinderDb context)
        {
            _context = context;
        }
        public async Task AddAsync(Animal entity)
        {
            await _context.AddAsync(entity);
        }

        public void Delete(Animal entity)
        {
            _context.Animals.Remove(entity);
        }

        public async Task<IEnumerable<Animal>> GetAllAsync(int shelterId)
        {
            return await _context.Animals.Where(a => a.ShelterId == shelterId).ToListAsync();
        }

        public Task<IEnumerable<Animal>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Animal> GetByIdAsync(int id)
        {
            return await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
        }

        public void Update(Animal entity)
        {
            _context.Animals.Update(entity);
        }
    }
}
