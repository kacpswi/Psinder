namespace Psinder.Dtos.AnimalDtos
{
    public class UpdateAnimalDto
    {
        public string Description { get; set; }
        public bool IsBooked { get; set; } = false;
    }
}
