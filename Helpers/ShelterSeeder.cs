using Microsoft.Identity.Client;
using Psinder.Data;

namespace Psinder.Helpers
{
    public class ShelterSeeder
    {
        private readonly PsinderDb _dbContext;

        public ShelterSeeder(PsinderDb dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Shelters.Any())
                {
                    var shelters = GetShelters();
                    _dbContext.Shelters.AddRange(shelters);
                    _dbContext.SaveChanges();
                }
            }
        }

        public IEnumerable<Shelter> GetShelters()
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
            return shelters;
        }
    }
}
