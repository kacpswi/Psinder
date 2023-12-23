using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Psinder.Data;
using Psinder.Repositories.Interfaces;

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

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
