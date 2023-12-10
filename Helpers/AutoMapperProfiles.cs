using AutoMapper;
using Psinder.Data;
using Psinder.Dtos;

namespace Psinder.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Shelter, ShelterDto>();
            CreateMap<CreateShelterDto, Shelter>();
            CreateMap<UpdateShelterDto, Shelter>();
        } 
    }
}
