using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoApp.Api.Data;
using GeoApp.Shared.Entities;
using GeoApp.Shared.DTO;
using GeoApp.Shared.DTO.Mapper;

namespace GeoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContinentsDTOController : ControllerBase
    {
        private readonly GeoDbContext _context;

        public ContinentsDTOController(GeoDbContext context)
        {
            _context = context;
        }

        // GET: api/ContinentsDTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContinentDTO>>> GetContinents()
        {
            if (_context.Continents == null)
            {
                return NotFound();
            }

            IList<Continent> continentEntities = await _context.Continents.ToListAsync();


            IList<ContinentDTO> continentDTOs = continentEntities.ToDTOs();
           
            
            return Ok(continentDTOs);
        }

        // GET: api/ContinentsDTO/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Continent>> GetContinent(int id)
        {
          if (_context.Continents == null)
          {
              return NotFound();
          }
            var continent = await _context.Continents.FindAsync(id);

            if (continent == null)
            {
                return NotFound();
            }

            return Ok(continent.ToDTO());
        }   

        // PUT: api/ContinentsDTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContinent(int id, ContinentDTO continentDTO)
        {
            Continent continent = continentDTO.ToEntity();

            if (id != continent.Id)
            {
                return BadRequest();
            }

            _context.Entry(continent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContinentExists(id))
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

        // POST: api/ContinentsDTO
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Continent>> PostContinent(ContinentDTO continentDTO)
        {
            if (_context.Continents == null)
            {
                return Problem("Entity set 'GeoDbContext.Continents'  is null.");
            }

            Continent continent = continentDTO.ToEntity();

            _context.Continents.Add(continent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContinent", new { id = continent.Id }, continent);
        }

        // DELETE: api/ContinentsDTO/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContinent(int id)
        {
            if (_context.Continents == null)
            {
                return NotFound();
            }
            var continent = await _context.Continents.FindAsync(id);
            if (continent == null)
            {
                return NotFound();
            }

            _context.Continents.Remove(continent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContinentExists(int id)
        {
            return (_context.Continents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
