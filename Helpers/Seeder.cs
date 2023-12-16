using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Psinder.Data;
using System.Text.Json;

namespace Psinder.Helpers
{
    public class Seeder
    {
        public static async Task SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager, PsinderDb context)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var users = JsonSerializer.Deserialize<List<User>>(userData);

            var roles = new List<Role>
            {
                new Role{Name = "User"},
                new Role{Name = "ShelterWorker"},
                new Role{Name = "Admin"},
                new Role{Name = "ShelterOwner"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.Email;
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "User");
            }

            var admin = new User
            {
                UserName = "admin",
                Name = "admin",
                Surename = "admin",
                City = "admin",
                Country = "admin",
                Email = "admin",
            };
            var result = await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] { "Admin" });
        }

        public static async Task SeedShelters(PsinderDb context)
        {
            if (context.Database.CanConnect())
            {
                if (!context.Shelters.Any())
                {
                    var shelters = new List<Shelter>()
                    {
                        new Shelter()
                        {
                            Name = "AAA",
                            Animals = new List<Animal>()
                            {
                                new Animal()
                                {
                                    Name = "Doggo",
                                    Description = "Doggo desc"
                                },
                                new Animal()
                                {
                                    Name = "Kitty",
                                    Description = "Kitty desc"
                                }
                            }
                        },
                        new Shelter()
                        {
                            Name = "BBB"
                        }
                    };
                    await context.Shelters.AddRangeAsync(shelters);
                    await context.SaveChangesAsync();
                }
            }

        }
    }
}
