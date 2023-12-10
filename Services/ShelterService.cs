using Psinder.Data;
using Psinder.Dtos;
using Psinder.Repositories.Interfaces;
using Psinder.Services.Interfaces;
using Psinder.Exceptions;
using AutoMapper;

namespace Psinder.Services
{
    public class ShelterService : IShelterService
    {
        private readonly IShelterRepository _shelterRepository;
        private readonly IMapper _mapper;
        public ShelterService(IShelterRepository shelterRepository, IMapper mapper)
        {
            _shelterRepository = shelterRepository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(CreateShelterDto dto)
        {
            var shelter = _mapper.Map<Shelter>(dto);
            await _shelterRepository.AddAsync(shelter);
            await _shelterRepository.SaveChangesAsync();
            return shelter.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var shelter = await _shelterRepository.GetByIdAsync(id);
            if (shelter != null)
            {
                _shelterRepository.Delete(shelter);
                await _shelterRepository.SaveChangesAsync();
            }
            else 
            {
                throw new NotFoundException("Shelter not found");
            }
        }

        public async Task<IEnumerable<ShelterDto>> GetAllAsync()
        {
            var shelters =  await _shelterRepository.GetAllAsync();
            var result = _mapper.Map<List<ShelterDto>>(shelters);
            return result;
        }

        public async Task<ShelterDto> GetByIdAsync(int id)
        {
            var shelter = await _shelterRepository.GetByIdAsync(id);
            if (shelter != null)
            {
                return _mapper.Map<ShelterDto>(shelter);
            }
            else
            {
                throw new NotFoundException("Shelter not found");
            }
        }

        public async Task UpdateAsync(int id, UpdateShelterDto dto)
        {
            var shelter = await _shelterRepository.GetByIdAsync(id);
            if(shelter != null)
            {
                _mapper.Map(dto, shelter);
                await _shelterRepository.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException("Shelter not found");
            }
        }
    }
}
