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
    [Route("api/ParametersEmissionSources")]
    public class ParametersEmissionSourcesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParametersEmissionSourcesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ParametersEmissionSources
        [HttpGet]
        public IEnumerable<ParametersEmissionSource> GetParametersEmissionSource()
        {
            return _context.ParametersEmissionSource;
        }

        // GET: api/ParametersEmissionSources/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParametersEmissionSource([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parametersEmissionSource = await _context.ParametersEmissionSource.SingleOrDefaultAsync(m => m.Id == id);

            if (parametersEmissionSource == null)
            {
                return NotFound();
            }

            return Ok(parametersEmissionSource);
        }

        // PUT: api/ParametersEmissionSources/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParametersEmissionSource([FromRoute] int id, [FromBody] ParametersEmissionSource parametersEmissionSource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parametersEmissionSource.Id)
            {
                return BadRequest();
            }

            _context.Entry(parametersEmissionSource).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParametersEmissionSourceExists(id))
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

        // POST: api/ParametersEmissionSources
        [HttpPost]
        public async Task<IActionResult> PostParametersEmissionSource([FromBody] ParametersEmissionSource parametersEmissionSource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ParametersEmissionSource.Add(parametersEmissionSource);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParametersEmissionSource", new { id = parametersEmissionSource.Id }, parametersEmissionSource);
        }

        // DELETE: api/ParametersEmissionSources/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParametersEmissionSource([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parametersEmissionSource = await _context.ParametersEmissionSource.SingleOrDefaultAsync(m => m.Id == id);
            if (parametersEmissionSource == null)
            {
                return NotFound();
            }

            _context.ParametersEmissionSource.Remove(parametersEmissionSource);
            await _context.SaveChangesAsync();

            return Ok(parametersEmissionSource);
        }

        private bool ParametersEmissionSourceExists(int id)
        {
            return _context.ParametersEmissionSource.Any(e => e.Id == id);
        }

        [HttpPost("HPAtypeSelect")]
        public SelectList HPAtypeSelect()
        {
            var typeHPA = new SelectList(_context.ParametersEmissionSource.OrderBy(m => m.Name), "Name", "Name");
            return typeHPA;
        }
    }
}