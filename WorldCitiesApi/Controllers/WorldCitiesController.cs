using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldCitiesApi.Data;
using WorldCitiesApi.Models;

namespace WorldCitiesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorldCitiesController : ControllerBase
    {
        private readonly WorldCitiesContext _context;

        public WorldCitiesController(WorldCitiesContext context)
        {
            _context = context;
        }

        // GET: api/WorldCities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorldCity>>> GetWorldCities()
        {
            return await _context.WorldCities.ToListAsync();
        }

        // GET: api/WorldCities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorldCity>> GetWorldCity(int id)
        {
            var worldCity = await _context.WorldCities.FindAsync(id);

            if (worldCity == null)
            {
                return NotFound();
            }

            return worldCity;
        }

        // PUT: api/WorldCities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorldCity(int id, WorldCity worldCity)
        {
            if (id != worldCity.CityId)
            {
                return BadRequest();
            }

            _context.Entry(worldCity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorldCityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WorldCities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorldCity>> PostWorldCity(WorldCity worldCity)
        {
            _context.WorldCities.Add(worldCity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorldCity", new { id = worldCity.CityId }, worldCity);
        }

        // DELETE: api/WorldCities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorldCity(int id)
        {
            var worldCity = await _context.WorldCities.FindAsync(id);
            if (worldCity == null)
            {
                return NotFound();
            }

            _context.WorldCities.Remove(worldCity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorldCityExists(int id)
        {
            return _context.WorldCities.Any(e => e.CityId == id);
        }
    }
}
