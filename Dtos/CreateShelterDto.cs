namespace Psinder.Dtos
{
    public class CreateShelterDto
    {
        public string Name { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public string? BuildingNumber { get; set; }
        public string? Description { get; set; }
    }
}
