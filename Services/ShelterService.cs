using Psinder.Data;
using Psinder.Repositories.Interfaces;
using Psinder.Services.Interfaces;
using Psinder.Exceptions;
using AutoMapper;
using Psinder.Dtos.ShelterDtos;

namespace Psinder.Services
{
    public class ShelterService : IShelterService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ShelterService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(CreateShelterDto dto)
        {
            var shelter = _mapper.Map<Shelter>(dto);
            await _uow.ShelterRepository.AddAsync(shelter);
            await _uow.Complete();
            return shelter.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var shelter = await _uow.ShelterRepository.GetByIdAsync(id);
            if (shelter != null)
            {
                _uow.ShelterRepository.Delete(shelter);
                await _uow.Complete();
            }
            else 
            {
                throw new NotFoundException("Shelter not found");
            }
        }

        public async Task<IEnumerable<ShelterDto>> GetAllAsync()
        {
            var shelters =  await _uow.ShelterRepository.GetAllAsync();
            var result = _mapper.Map<List<ShelterDto>>(shelters);
            return result;
        }

        public async Task<ShelterDto> GetByIdAsync(int id)
        {
            var shelter = await _uow.ShelterRepository.GetByIdAsync(id);
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
            var shelter = await _uow.ShelterRepository.GetByIdAsync(id);
            if(shelter != null)
            {
                _mapper.Map(dto, shelter);
                await _uow.Complete();
            }
            else
            {
                throw new NotFoundException("Shelter not found");
            }
        }
    }
}
