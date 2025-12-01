using Microsoft.EntityFrameworkCore;
using WorldCitiesApi.Data;
using WorldCitiesApi.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<WorldCitiesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WorldCitiesConnection")));

builder.Services.AddControllers();

// Add Swagger for testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// -------------------------------
// Apply Migrations Automatically
// -------------------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WorldCitiesContext>();

    Console.WriteLine("Checking database migrations...");
    db.Database.Migrate();
}

// -------------------------------
// Seed Database From CSV (First Run Only)
// -------------------------------
void SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<WorldCitiesContext>();

    // If already seeded, skip
    if (db.WorldCities.Any())
    {
        Console.WriteLine("Database already seeded.");
        return;
    }

    Console.WriteLine("Seeding database...");

    // *** FIXED PATH — USING ContentRootPath ***
    var csvFilePath = Path.Combine(app.Environment.ContentRootPath, "Data", "worldcities.csv");

    if (!File.Exists(csvFilePath))
    {
        Console.WriteLine($"CSV file not found: {csvFilePath}");
        return;
    }

    var lines = File.ReadAllLines(csvFilePath);

    // Skip header
    foreach (var line in lines.Skip(1))
    {
        var parts = line.Split(",");

        if (parts.Length != 3) continue;

        db.WorldCities.Add(new WorldCity
        {
            City = parts[0].Trim(),
            Country = parts[1].Trim(),
            Population = int.Parse(parts[2], CultureInfo.InvariantCulture)
        });
    }

    db.SaveChanges();
    Console.WriteLine("Seeding completed.");
}

SeedDatabase(app);

// -------------------------------
// Middleware
// -------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
