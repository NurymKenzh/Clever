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
    [Route("api/Substances")]
    public class SubstancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubstancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Substances
        [HttpGet]
        public IEnumerable<Substance> GetSubstance()
        {
            return _context.Substance;
        }

        // GET: api/Substances/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubstance([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var substance = await _context.Substance.SingleOrDefaultAsync(m => m.Id == id);

            if (substance == null)
            {
                return NotFound();
            }

            return Ok(substance);
        }

        // PUT: api/Substances/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubstance([FromRoute] int id, [FromBody] Substance substance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != substance.Id)
            {
                return BadRequest();
            }

            _context.Entry(substance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubstanceExists(id))
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

        // POST: api/Substances
        [HttpPost]
        public async Task<IActionResult> PostSubstance([FromBody] Substance substance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Substance.Add(substance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubstance", new { id = substance.Id }, substance);
        }

        // DELETE: api/Substances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubstance([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var substance = await _context.Substance.SingleOrDefaultAsync(m => m.Id == id);
            if (substance == null)
            {
                return NotFound();
            }

            _context.Substance.Remove(substance);
            await _context.SaveChangesAsync();

            return Ok(substance);
        }

        private bool SubstanceExists(int id)
        {
            return _context.Substance.Any(e => e.Id == id);
        }
    }
}