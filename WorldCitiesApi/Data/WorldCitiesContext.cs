using Microsoft.EntityFrameworkCore;
using WorldCitiesApi.Models;

namespace WorldCitiesApi.Data
{
    // This is my DbContext – it manages the database connection
    public class WorldCitiesContext : DbContext
    {
        public WorldCitiesContext(DbContextOptions<WorldCitiesContext> options)
            : base(options)
        {
        }

        // This represents the WorldCities table
        public DbSet<WorldCity> WorldCities { get; set; } = null!;
    }
}
