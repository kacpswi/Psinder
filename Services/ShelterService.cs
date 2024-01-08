using Psinder.Data;
using Psinder.Repositories.Interfaces;
using Psinder.Services.Interfaces;
using Psinder.Exceptions;
using AutoMapper;
using Psinder.Dtos.ShelterDtos;
using Psinder.Helpers;
using System.Linq.Expressions;
using System.Linq;

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

        public async Task<int> AddAsync(CreateShelterDto dto, int userId)
        {
            var shelter = _mapper.Map<Shelter>(dto);
            shelter.CreatedById = userId;
            var user = await _uow.UserRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            if (user.ShelterId != null)
            {
                throw new BadRequestException("You cannot be owner of multiple shelters");
            }
            shelter.Workers.Add(user);
            await _uow.ShelterRepository.AddAsync(shelter);
            await _uow.Complete();
            return shelter.Id;
        }

        public async Task DeleteAsync(int id, int userId)
        {
            var shelter = await _uow.ShelterRepository.GetByIdAsync(id);
            if (shelter != null)
            {
                var user = await _uow.UserRepository.GetUserByIdAsync(userId);
                if(user.ShelterId == shelter.Id)
                {
                    user.ShelterId = null;
                    _uow.ShelterRepository.Delete(shelter);
                    await _uow.Complete();
                }
                else
                {
                    throw new BadRequestException("You cannot delete others shelter");
                }
            }
            else 
            {
                throw new NotFoundException("Shelter not found");
            }
        }

        public async Task<PagedResult<ShelterDto>> GetAllAsync(PageQuery query)
        {
            var paginationResult =  await _uow.ShelterRepository.GetAllAsync(query);

            var sheltersDto = _mapper.Map<List<ShelterDto>>(paginationResult.Data);
                     
            var result = new PagedResult<ShelterDto>(sheltersDto, paginationResult.TotalCount, query.PageSize, query.PageNumber);

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

        public async Task<ShelterDto> UpdateAsync(int id, UpdateShelterDto dto, int userId)
        {
            var shelter = await _uow.ShelterRepository.GetByIdAsync(id);
            if(shelter != null)
            {
                var user = await _uow.UserRepository.GetUserByIdAsync(userId);
                if(user.ShelterId != shelter.Id)
                {
                    throw new BadRequestException("You cannot edit others shelter");
                }
                var newShelter = _mapper.Map(dto, shelter);
                await _uow.Complete();
                var newShelterDto = _mapper.Map<ShelterDto>(newShelter);
                return newShelterDto;
            }
            else
            {
                throw new NotFoundException("Shelter not found");
            }
        }
    }
}
