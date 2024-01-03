﻿using AutoMapper;
using Psinder.Data;
using Psinder.Dtos.AnimalDtos;
using Psinder.Dtos.ShelterDtos;
using Psinder.Exceptions;
using Psinder.Helpers;
using Psinder.Repositories.Interfaces;
using Psinder.Services.Interfaces;

namespace Psinder.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public AnimalService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(int shelterId, CreateAnimalDto dto)
        {
            var shelter = await _uow.ShelterRepository.GetByIdAsync(shelterId);

            if (shelter == null)
            {
                throw new NotFoundException("Shelter not found");
            }
            else
            {
                var animal = _mapper.Map<Animal>(dto);
                animal.ShelterId = shelterId;
                await _uow.AnimalRepository.AddAsync(animal);
                await _uow.Complete();
                return animal.Id;
            }
            
        }

        public async Task<PagedResult<AnimalDto>> GetAllAsync(PageQuery query)
        {
            var paginationResult = await _uow.AnimalRepository.GetAllAsync(query);

            var animalDto = _mapper.Map<List<AnimalDto>>(paginationResult.Data);

            var result = new PagedResult<AnimalDto>(animalDto, paginationResult.TotalCount, query.PageSize, query.PageNumber);

            return result;
        }

        public async Task<List<AnimalDto>> GetAllForShelter(int shelterId)
        {
            var shelter = await _uow.ShelterRepository.GetByIdAsync(shelterId);
            if (shelter == null)
            {
                throw new NotFoundException("Shelter not found");
            }
            var animals =  shelter.Animals.ToList();
            var results = _mapper.Map<List<AnimalDto>>(animals);
            return results;
        }

        public async Task<AnimalDto> GetByIdAsync(int shelterId, int animalId)
        {
            var shelter = await _uow.ShelterRepository.GetByIdAsync(shelterId);
            if (shelter == null)
            {
                throw new NotFoundException("Shelter not found");
            }
            else
            {
                var animal = shelter.Animals.FirstOrDefault(a => a.Id == animalId);
                if (animal == null)
                {
                    throw new NotFoundException("Animal not found");
                }
                var result = _mapper.Map<AnimalDto>(animal);
                return result;
            }
        }

        public async Task RemoveByIdAsync(int shelterId, int animalId)
        {
            var shelter = await _uow.ShelterRepository.GetByIdAsync(shelterId);

            var animal = await _uow.AnimalRepository.GetByIdAsync(animalId);

            if(animal == null || animal.ShelterId != shelterId)
            {
                throw new NotFoundException("Animal not found");
            }
            else
            {
                _uow.AnimalRepository.Delete(animal);
                await _uow.Complete();
            }
        }

        public async Task UpdateAsync(int shelterId, int animalId, UpdateAnimalDto dto)
        {
            var shelter = await _uow.ShelterRepository.GetByIdAsync(shelterId);
            var animal = await _uow.AnimalRepository.GetByIdAsync (animalId);
            if (shelter == null)
            {
                throw new NotFoundException("Shelter not found");
            }
            else if (animal == null)
            {
                throw new NotFoundException("Animal not found");
            }
            else
            {
                _mapper.Map(dto, animal);
                await _uow.Complete();
            }
        }
    }
}
