using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CleverAPI.Data;
using CleverAPI.Models;

namespace CleverAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Densities")]
    public class DensitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DensitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Densities
        [HttpGet]
        public IEnumerable<Density> GetDensity()
        {
            return _context.Density;
        }

        // GET: api/Densities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDensity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var density = await _context.Density.SingleOrDefaultAsync(m => m.Id == id);

            if (density == null)
            {
                return NotFound();
            }

            return Ok(density);
        }

        // PUT: api/Densities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDensity([FromRoute] int id, [FromBody] Density density)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != density.Id)
            {
                return BadRequest();
            }

            _context.Entry(density).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DensityExists(id))
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

        // POST: api/Densities
        [HttpPost]
        public async Task<IActionResult> PostDensity([FromBody] Density density)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Density.Add(density);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDensity", new { id = density.Id }, density);
        }

        // DELETE: api/Densities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDensity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var density = await _context.Density.SingleOrDefaultAsync(m => m.Id == id);
            if (density == null)
            {
                return NotFound();
            }

            _context.Density.Remove(density);
            await _context.SaveChangesAsync();

            return Ok(density);
        }

        private bool DensityExists(int id)
        {
            return _context.Density.Any(e => e.Id == id);
        }
    }
}