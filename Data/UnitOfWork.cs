using AutoMapper;
using Psinder.Repositories;
using Psinder.Repositories.Interfaces;

namespace Psinder.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PsinderDb _context;

        public UnitOfWork(PsinderDb context)
        {
            _context = context;
        }

        public IShelterRepository ShelterRepository => new ShelterRepository(_context);

        public IAnimalRepository AnimalRepository => new AnimalRepository(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
