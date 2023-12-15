using Microsoft.AspNetCore.Identity;

namespace Psinder.Data
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
