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
    [Route("api/CaloricEquivalents")]
    public class CaloricEquivalentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CaloricEquivalentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CaloricEquivalents
        [HttpGet]
        public IEnumerable<CaloricEquivalent> GetCaloricEquivalent()
        {
            return _context.CaloricEquivalent;
        }

        // GET: api/CaloricEquivalents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCaloricEquivalent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var caloricEquivalent = await _context.CaloricEquivalent.SingleOrDefaultAsync(m => m.Id == id);

            if (caloricEquivalent == null)
            {
                return NotFound();
            }

            return Ok(caloricEquivalent);
        }

        // PUT: api/CaloricEquivalents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaloricEquivalent([FromRoute] int id, [FromBody] CaloricEquivalent caloricEquivalent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != caloricEquivalent.Id)
            {
                return BadRequest();
            }

            _context.Entry(caloricEquivalent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaloricEquivalentExists(id))
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

        // POST: api/CaloricEquivalents
        [HttpPost]
        public async Task<IActionResult> PostCaloricEquivalent([FromBody] CaloricEquivalent caloricEquivalent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CaloricEquivalent.Add(caloricEquivalent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCaloricEquivalent", new { id = caloricEquivalent.Id }, caloricEquivalent);
        }

        // DELETE: api/CaloricEquivalents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaloricEquivalent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var caloricEquivalent = await _context.CaloricEquivalent.SingleOrDefaultAsync(m => m.Id == id);
            if (caloricEquivalent == null)
            {
                return NotFound();
            }

            _context.CaloricEquivalent.Remove(caloricEquivalent);
            await _context.SaveChangesAsync();

            return Ok(caloricEquivalent);
        }

        private bool CaloricEquivalentExists(int id)
        {
            return _context.CaloricEquivalent.Any(e => e.Id == id);
        }

        [HttpPost("FuelSelect")]
        public SelectList FuelSelect()
        {
            var caloricEquivalent = new SelectList(_context.CaloricEquivalent.OrderBy(m => m.Name), "Name", "Name");
            return caloricEquivalent;
        }
    }
}