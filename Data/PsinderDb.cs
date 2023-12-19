using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace Psinder.Data
{
    public class PsinderDb : IdentityDbContext<User, Role, int,
            IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>,
            IdentityUserToken<int>>
    {
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<UserLike> Likes { get; set; }
        public PsinderDb(DbContextOptions<PsinderDb> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            modelBuilder.Entity<Shelter>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Shelter>()
                .Property(e => e.Name)
                .IsRequired();

            modelBuilder.Entity<Animal>()
                .Property(a => a.Name)
                .IsRequired();

            modelBuilder.Entity<Animal>()
                .Property(a => a.Description)
                .IsRequired();

            modelBuilder.Entity<UserLike>()
                .HasKey(k => new { k.UserId, k.AnimalId });

            modelBuilder.Entity<UserLike>()
                .HasOne(s => s.User)
                .WithMany(l => l.LikedAnimals)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserLike>()
                .HasOne(s => s.Animal)
                .WithMany(l => l.LikedByUsers)
                .HasForeignKey(s => s.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
