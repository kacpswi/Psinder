using Microsoft.AspNetCore.Identity;

namespace Psinder.Data
{
    public class User : IdentityUser<int>
    {
        public string? City { get; set; }
        public string? Country { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
