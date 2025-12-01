using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WorldCitiesApi.Data
{
    // This factory is only used at design time by EF Core
    // It helps EF Core create a WorldCitiesContext when I run migrations
    public class WorldCitiesContextFactory : IDesignTimeDbContextFactory<WorldCitiesContext>
    {
        public WorldCitiesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WorldCitiesContext>();

            // Same connection string as in appsettings.json
            var connectionString = "Server=localhost,1433;Database=WorldCitiesDb;User Id=sa;Password=Beyz@vi64;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);

            return new WorldCitiesContext(optionsBuilder.Options);
        }
    }
}
