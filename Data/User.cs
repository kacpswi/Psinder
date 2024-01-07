using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Psinder.Data
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surename { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<UserLike> LikedAnimals { get; set; }
        public List<Message> MessagesReceived { get; set; }
        public List<Message> MessagesSend { get; set; }
        public int? ShelterId { get; set; }
        [JsonIgnore]
        public Shelter? Shelter { get; set; }
    }
}
