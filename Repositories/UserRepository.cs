using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Psinder.Data;
using Psinder.Helpers;
using Psinder.Repositories.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq.Expressions;

namespace Psinder.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PsinderDb _context;
        public UserRepository(PsinderDb context)
        {
            _context = context;
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<Pagination<User>> GetUsersAsync(PageQuery query)
        {
            var baseQuery = _context.Users
               .Where(r => query.SearchPhrase == null || (r.Name.ToLower().Contains(query.SearchPhrase.ToLower())
                                                   || r.Surename.ToLower().Contains(query.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<User, object>>>
                {
                    { nameof(User.Name), r => r.Name},
                    { nameof(User.Surename), r => r.Surename},
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                    baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
            }

            var users = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            return new Pagination<User>(users, totalItemsCount);
        }
    }
}
