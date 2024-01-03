using Microsoft.EntityFrameworkCore;
using Psinder.Data;
using Psinder.Dtos;
using Psinder.Exceptions;
using Psinder.Helpers;
using Psinder.Repositories.Interfaces;
using System.Linq.Expressions;

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


        public async Task<Pagination<Shelter>> GetAllAsync(PageQuery query)
        {
            var baseQuery = _context.Shelters
                .Include(a => a.Animals)
                .Where(r => query.SearchPhrase == null || (r.Name.ToLower().Contains(query.SearchPhrase.ToLower())
                                                    || r.Description.ToLower().Contains(query.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Shelter, object>>>
                {
                    { nameof(Shelter.Name), r => r.Name},
                    { nameof(Shelter.Description), r => r.Description},
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                    baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var shelters = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            return new Pagination<Shelter>(shelters, totalItemsCount);
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
