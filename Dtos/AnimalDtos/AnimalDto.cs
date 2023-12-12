namespace Psinder.Dtos.AnimalDtos
{
    public class AnimalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsBooked { get; set; } = false;
    }
}
