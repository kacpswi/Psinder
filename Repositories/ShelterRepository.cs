using Microsoft.EntityFrameworkCore;
using Psinder.Data;
using Psinder.Dtos;
using Psinder.Exceptions;
using Psinder.Repositories.Interfaces;

namespace Psinder.Repositories
{
    public class ShelterRepository : IShelterRepository
    {
        private readonly PsinderDb _context;
        public ShelterRepository(PsinderDb context) 
        {
            _context = context;
        }

        public async Task AddAsync(Shelter entity)
        {
            await _context.AddAsync(entity);
        }

        public void Delete(Shelter entity)
        {
            _context.Shelters.Remove(entity);
        }


        public async Task<IEnumerable<Shelter>> GetAllAsync()
        {
            var result = await _context.Shelters
                .Include(a => a.Animals)
                .ToListAsync();
            return result;
        }


        public async Task<Shelter> GetByIdAsync(int id)
        {
            var result = await _context.Shelters.
                Include(a => a.Animals).
                FirstOrDefaultAsync(s => s.Id == id);
            return result;
        }

        public void Update(Shelter entity)
        {
            _context.Shelters.Update(entity);
        }
    }
}
