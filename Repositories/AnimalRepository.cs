using Microsoft.EntityFrameworkCore;
using Psinder.Data;
using Psinder.Helpers;
using Psinder.Repositories.Interfaces;
using System.Linq.Expressions;

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

        public async Task<IEnumerable<Animal>> GetAllForShelterAsync(int shelterId)
        {
            return await _context.Animals.Where(a => a.ShelterId == shelterId).ToListAsync();
        }

        public async Task<Pagination<Animal>> GetAllAsync(PageQuery query)
        {
            var baseQuery = _context.Animals
                .Where(r => query.SearchPhrase == null || (r.Name.ToLower().Contains(query.SearchPhrase.ToLower())
                                                    || r.Description.ToLower().Contains(query.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Animal, object>>>
                {
                    { nameof(Animal.Name), r => r.Name},
                    { nameof(Animal.Description), r => r.Description},
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                    baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var animals = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            return new Pagination<Animal>(animals, totalItemsCount);
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
