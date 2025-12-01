using System.ComponentModel.DataAnnotations;

namespace WorldCitiesApi.Models
{
    // This is my simple model for one city in the world
    // I keep it small and clear so it is easy to read
    public class WorldCity
    {
        // I mark this property as the primary key for the table
        [Key]
        public int CityId { get; set; }

        // Name of the city, for example "Stockholm"
        public string City { get; set; } = string.Empty;

        // Name of the country, for example "Sweden"
        public string Country { get; set; } = string.Empty;

        // Population of the city
        public int Population { get; set; }
    }
}
