using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CleverAPI.Data;
using CleverAPI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleverAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/GasDensities")]
    public class GasDensitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GasDensitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GasDensities
        [HttpGet]
        public IEnumerable<GasDensity> GetGasDensity()
        {
            return _context.GasDensity;
        }

        // GET: api/GasDensities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGasDensity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gasDensity = await _context.GasDensity.SingleOrDefaultAsync(m => m.Id == id);

            if (gasDensity == null)
            {
                return NotFound();
            }

            return Ok(gasDensity);
        }

        // PUT: api/GasDensities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGasDensity([FromRoute] int id, [FromBody] GasDensity gasDensity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gasDensity.Id)
            {
                return BadRequest();
            }

            _context.Entry(gasDensity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GasDensityExists(id))
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

        // POST: api/GasDensities
        [HttpPost]
        public async Task<IActionResult> PostGasDensity([FromBody] GasDensity gasDensity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GasDensity.Add(gasDensity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGasDensity", new { id = gasDensity.Id }, gasDensity);
        }

        // DELETE: api/GasDensities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasDensity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gasDensity = await _context.GasDensity.SingleOrDefaultAsync(m => m.Id == id);
            if (gasDensity == null)
            {
                return NotFound();
            }

            _context.GasDensity.Remove(gasDensity);
            await _context.SaveChangesAsync();

            return Ok(gasDensity);
        }

        private bool GasDensityExists(int id)
        {
            return _context.GasDensity.Any(e => e.Id == id);
        }

        [HttpPost("GasSelect")]
        public SelectList GasSelect()
        {
            var gasDensity = new SelectList(_context.GasDensity.OrderBy(m => m.Name), "Name", "Name");
            return gasDensity;
        }
    }
}