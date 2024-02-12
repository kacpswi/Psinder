using Psinder.Data;
using Psinder.Dtos;
using Psinder.Helpers;

namespace Psinder.Repositories.Interfaces
{
    public interface IShelterRepository
    {
        public Task<Pagination<Shelter>> GetAllAsync(PageQuery query);
        public Task<Shelter> GetByIdAsync(int id);
        public Task AddAsync(Shelter entity);
        public void Update(Shelter entity);
        public void Delete(Shelter entity);
    }
}
