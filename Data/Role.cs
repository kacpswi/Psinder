using Microsoft.AspNetCore.Identity;

namespace Psinder.Data
{
    public class Role : IdentityRole<int>
    {
        public List<UserRole> UserRoles { get; set; }
    }
}
