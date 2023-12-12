using AutoMapper;
using Psinder.Data;
using Psinder.Dtos.AnimalDtos;
using Psinder.Dtos.ShelterDtos;

namespace Psinder.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Shelter, ShelterDto>();
            CreateMap<CreateShelterDto, Shelter>();
            CreateMap<UpdateShelterDto, Shelter>();

            CreateMap<Animal, AnimalDto>();
            CreateMap<CreateAnimalDto, Animal>();
            CreateMap<UpdateAnimalDto, Animal>();
        } 
    }
}
