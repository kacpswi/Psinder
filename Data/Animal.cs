using System.Text.Json.Serialization;

namespace Psinder.Data
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsBooked { get; set; } = false;
        public int ShelterId { get; set; }
        [JsonIgnore]
        public Shelter Shelter { get; set; }
    }
}
