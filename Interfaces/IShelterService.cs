using Psinder.Data;
using Psinder.Dtos;

namespace Psinder.Interfaces
{
    public interface IShelterService
    {
        public ShelterDto GetShelter(int id);
        public IEnumerable<ShelterDto> GetShelters();
        public void DeleteShelter(int id);
        public void ModifyShelter(int id);
        public void CreateShelter(CreateShelterDto shelter);
    }
}
