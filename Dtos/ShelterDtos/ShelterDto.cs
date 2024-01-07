using Psinder.Data;
using Psinder.Dtos.AnimalDtos;

namespace Psinder.Dtos.ShelterDtos
{
    public class ShelterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public string? BuildingNumber { get; set; }
        public string? Description { get; set; }
        public int? CreatedById { get; set; }
        public List<AnimalDto> Animals { get; set; }
    }
}
