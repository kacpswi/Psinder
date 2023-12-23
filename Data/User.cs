using Microsoft.AspNetCore.Identity;

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
    }
}
