using Psinder.Data;
using Psinder.Helpers;

namespace Psinder.Repositories.Interfaces
{
    public interface IAnimalRepository
    {
        public Task<IEnumerable<Animal>> GetAllForShelterAsync(int shelterId);
        public Task<Pagination<Animal>> GetAllAsync(PageQuery query);
        public Task<Animal> GetByIdAsync(int id);
        public Task AddAsync(Animal entity);
        public void Update(Animal entity);
        public void Delete(Animal entity);
    }
}
