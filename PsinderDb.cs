using Microsoft.EntityFrameworkCore;
using Psinder.Data;
using System.Xml.Serialization;

namespace Psinder
{
    public class PsinderDb : DbContext 
    {
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public PsinderDb(DbContextOptions<PsinderDb> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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

        }
    }
}
