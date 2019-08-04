using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CleverAPI.Data;
using CleverAPI.Models;
using System.Net.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;

namespace CleverAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/AirContaminants")]
    public class AirContaminantsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AirContaminantsController(ApplicationDbContext context,
            IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: api/AirContaminants
        [HttpGet]
        public IEnumerable<AirContaminant> GetAirContaminant(string SortOrder, string Name, string NumberCAS, int? HazardClass, int? PageSize, int? Page)
        {
            var airContaminants = _context.AirContaminant
                .Where(a => true);

            if (!string.IsNullOrEmpty(Name))
            {
                airContaminants = airContaminants.Where(e => e.Name.ToLower().Contains(Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(NumberCAS))
            {
                airContaminants = airContaminants.Where(e => e.NumberCAS.ToLower().Contains(NumberCAS.ToLower()));
            }
            if (HazardClass!=null)
            {
                airContaminants = airContaminants.Where(e => e.HazardClass == HazardClass);
            }

            switch (SortOrder)
            {
                case "Name":
                    airContaminants = airContaminants.OrderBy(e => e.Name);
                    break;
                case "NameDesc":
                    airContaminants = airContaminants.OrderByDescending(e => e.Name);
                    break;
                case "NumberCAS":
                    airContaminants = airContaminants.OrderBy(e => e.NumberCAS);
                    break;
                case "NumberCASDesc":
                    airContaminants = airContaminants.OrderByDescending(e => e.NumberCAS);
                    break;
                case "HazardClass":
                    airContaminants = airContaminants.OrderBy(e => e.HazardClass);
                    break;
                case "HazardClassDesc":
                    airContaminants = airContaminants.OrderByDescending(e => e.HazardClass);
                    break;
                default:
                    airContaminants = airContaminants.OrderBy(e => e.Id);
                    break;
            }

            if (PageSize != null && Page != null)
            {
                airContaminants = airContaminants.Skip(((int)Page - 1) * (int)PageSize).Take((int)PageSize);
            }

            return airContaminants;
        }

        // GET: api/AirContaminants/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAirContaminant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var airContaminant = await _context.AirContaminant.SingleOrDefaultAsync(m => m.Id == id);

            if (airContaminant == null)
            {
                return NotFound();
            }

            return Ok(airContaminant);
        }

        // PUT: api/AirContaminants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirContaminant([FromRoute] int id, [FromBody] AirContaminant airContaminant)
        {
            var airContaminants = _context.AirContaminant.AsNoTracking().ToList();
            if (_context.AirContaminant.AsNoTracking().FirstOrDefault(a => a.Name == airContaminant.Name && a.Id != airContaminant.Id) != null)
            {
                ModelState.AddModelError("Name", "An object with this value already exists!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != airContaminant.Id)
            {
                return BadRequest();
            }

            _context.Entry(airContaminant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirContaminantExists(id))
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

        // POST: api/AirContaminants
        [HttpPost]
        public async Task<IActionResult> PostAirContaminant([FromBody] AirContaminant airContaminant)
        {
            var airContaminants = _context.AirContaminant.AsNoTracking().ToList();
            if (_context.AirContaminant.AsNoTracking().FirstOrDefault(a => a.Name == airContaminant.Name) != null)
            {
                ModelState.AddModelError("Name", "An object with this value already exists!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AirContaminant.Add(airContaminant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAirContaminant", new { id = airContaminant.Id }, airContaminant);
        }

        // DELETE: api/AirContaminants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirContaminant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var airContaminant = await _context.AirContaminant.SingleOrDefaultAsync(m => m.Id == id);
            if (airContaminant == null)
            {
                return NotFound();
            }

            _context.AirContaminant.Remove(airContaminant);
            await _context.SaveChangesAsync();

            return Ok(airContaminant);
        }

        private bool AirContaminantExists(int id)
        {
            return _context.AirContaminant.Any(e => e.Id == id);
        }

        // GET: api/AirContaminants/Count
        [HttpGet("count")]
        public async Task<IActionResult> GetAirContaminantsCount(string SortOrder, string Name, string NumberCAS, int? HazardClass)
        {
            var airContaminants = _context.AirContaminant
                .Where(a => true);
            if (!string.IsNullOrEmpty(Name))
            {
                airContaminants = airContaminants.Where(e => e.Name.ToLower().Contains(Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(NumberCAS))
            {
                airContaminants = airContaminants.Where(e => e.NumberCAS.ToLower().Contains(NumberCAS.ToLower()));
            }
            if (HazardClass != null)
            {
                airContaminants = airContaminants.Where(e => e.HazardClass == HazardClass);
            }
            int count = await airContaminants.CountAsync();
            return Ok(count);
        }

        // POST api/AirContaminants/Upload
        [HttpPost("upload")]
        public async Task<IActionResult> PostFiles(IFormFile file)
        {
            try
            {
                int countUploaded = 0;
                string sContentRootPath = _hostingEnvironment.WebRootPath;
                sContentRootPath = Path.Combine(sContentRootPath, "Uploads");
                var filePath = Path.Combine(sContentRootPath, Path.GetFileName(file.FileName));

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                FileInfo fileinfo = new FileInfo(Path.Combine(sContentRootPath, Path.GetFileName(filePath)));
                using (ExcelPackage package = new ExcelPackage(fileinfo))
                {
                    int start_row = 1;
                    bool FirstRowHeader = true;
                    if (FirstRowHeader)
                    {
                        start_row++;
                    }
                    List<AirContaminant> airContaminants = new List<AirContaminant>();
                    var existAirContaminants = _context.AirContaminant.AsNoTracking().ToList();
                    for (int i = start_row; ; i++)
                    {
                        if (package.Workbook.Worksheets.FirstOrDefault().Cells[i, 1].Value == null)
                        {
                            break;
                        }
                        string Name = null,
                            NumberCAS = null;
                        int? HazardClass = null;
                        decimal? MaximumPermissibleConcentrationOneTimeMaximum = null,
                            MaximumPermissibleConcentrationDailyAverage = null,
                            ApproximateSafeExposureLevel = null;
                        try
                        {
                            Name = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 1].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 1: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }
                        try
                        {
                            NumberCAS = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 2].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 2: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }
                        try
                        {
                            HazardClass = package.Workbook.Worksheets.FirstOrDefault().Cells[i, 3].Value == null ? null : (int?)Convert.ToInt32(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 3].Value) ;
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 3: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }
                        try
                        {
                            MaximumPermissibleConcentrationOneTimeMaximum = package.Workbook.Worksheets.FirstOrDefault().Cells[i, 4].Value == null ? null : (decimal?)Convert.ToDecimal(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 4].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 4: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }
                        try
                        {
                            MaximumPermissibleConcentrationDailyAverage = package.Workbook.Worksheets.FirstOrDefault().Cells[i, 5].Value == null ? null : (decimal?)Convert.ToDecimal(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 5].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 5: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }
                        try
                        {
                            ApproximateSafeExposureLevel = package.Workbook.Worksheets.FirstOrDefault().Cells[i, 6].Value == null ? null : (decimal?)Convert.ToDecimal(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 6].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 6: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }
                        if (_context.AirContaminant.AsNoTracking().FirstOrDefault(a => a.Name == Name) != null)
                        {
                            throw new Exception($"Row {i.ToString()}, Column 6: An object with this value already exists!");
                        }
                        airContaminants.Add(new AirContaminant()
                        {
                            Name = Name,
                            NumberCAS = NumberCAS,
                            HazardClass = HazardClass,
                            MaximumPermissibleConcentrationOneTimeMaximum = MaximumPermissibleConcentrationOneTimeMaximum,
                            MaximumPermissibleConcentrationDailyAverage = MaximumPermissibleConcentrationDailyAverage,
                            ApproximateSafeExposureLevel = ApproximateSafeExposureLevel
                        });
                    }
                    _context.AddRange(airContaminants);
                    countUploaded += airContaminants.Count();
                }
                try
                {
                    fileinfo.Delete();
                }
                catch
                {
                }
                _context.SaveChanges();
                return Ok(countUploaded);
            }
            catch (Exception exp)
            {
                return Json(exp.Message);
            }
        }
    }
}