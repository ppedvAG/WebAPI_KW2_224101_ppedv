using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Grundlagen.Data;
using WebAPI_Grundlagen.Models;

namespace WebAPI_Grundlagen.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //Besagt, dass diese Klasse eine Web-API-Controller darstellt 
    [Produces("application/xml")] //Dieses Attribut hilft dem Benutzer, das Framework zu informieren, um das Ausgabeergebnis immer in einem bestimmten Inhaltstyp wie folgt zu generieren und zu senden. 

    //[Consumes("application/json")] //hilft dem Benutzer, das Framework zu informieren, um die Eingabe immer in einem bestimmten Inhaltstyp wie folgt zu akzeptieren. 

    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ConventionsController : ControllerBase
    {
        private readonly CarDbContext _context;

        #region Konstruktor mit Injections
        public ConventionsController(CarDbContext context)
        {
            _context = context;
        }

        #endregion


        #region Get - Methoden
        // GET: api/Conventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar()
        {
          if (_context.Cars == null)
          {
              return NotFound();
          }
            return await _context.Cars.ToListAsync();
        }

        // GET: api/Conventions/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
          if (_context.Cars == null)
          {
              return NotFound();
          }
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            if (car.Brand == "Porsche")
                return BadRequest();

            return car;
        }
        #endregion


        #region Update-Methode
        // PUT: api/Conventions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Put))]

        //ApiConventionMethod löst in einzelne ProdcuesResponseType auf
        //[ProducesDefaultResponseType]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest(); //400
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();//204
        }
        #endregion


        #region Post-Methode
        // POST: api/Conventions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
          if (_context.Cars == null)
          {
              return Problem("Entity set 'CarDbContext.Car'  is null.");
          }
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }
        #endregion


        #region Delete-Methode
        // DELETE: api/Conventions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            if (_context.Cars == null)
            {
                return NotFound();
            }
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        private bool CarExists(int id)
        {
            return (_context.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
