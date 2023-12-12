namespace Psinder.Dtos.AnimalDtos
{
    public class CreateAnimalDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsBooked { get; set; } = false;
    }
}
