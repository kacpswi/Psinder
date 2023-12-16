using AutoMapper;
using Psinder.Data;
using Psinder.Dtos.AnimalDtos;
using Psinder.Dtos.ShelterDtos;
using Psinder.Dtos.UserDtos;

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

            CreateMap<RegisterUserDto, User>();
        } 
    }
}
