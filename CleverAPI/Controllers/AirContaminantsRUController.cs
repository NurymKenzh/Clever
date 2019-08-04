using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CleverAPI.Data;
using CleverAPI.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;

namespace CleverAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/AirContaminantsRU")]
    public class AirContaminantsRUController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AirContaminantsRUController(ApplicationDbContext context,
            IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: api/AirContaminantsRU
        [HttpGet]
        public IEnumerable<AirContaminantRU> GetAirContaminantRU(string SortOrder,
            string Name,
            string NumberCAS,
            string Formula,
            decimal? MaximumPermissibleConcentrationOneTimeMaximum,
            decimal? MaximumPermissibleConcentrationDailyAverage,
            int? HazardClass,
            int? Code,
            decimal? ApproximateSafeExposureLevel,
            int? PageSize,
            int? Page)
        {
            var airContaminantsRU = _context.AirContaminantRU
                .Where(a => true);

            if (!string.IsNullOrEmpty(Name))
            {
                airContaminantsRU = airContaminantsRU.Where(a => a.Name.ToLower().Contains(Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(NumberCAS))
            {
                airContaminantsRU = airContaminantsRU.Where(a => a.NumberCAS.ToLower().Contains(NumberCAS.ToLower()));
            }
            if (!string.IsNullOrEmpty(Formula))
            {
                airContaminantsRU = airContaminantsRU.Where(a => a.Formula.ToLower().Contains(Formula.ToLower()));
            }
            if (MaximumPermissibleConcentrationOneTimeMaximum != null)
            {
                airContaminantsRU = airContaminantsRU.Where(a => a.MaximumPermissibleConcentrationOneTimeMaximum == MaximumPermissibleConcentrationOneTimeMaximum);
            }
            if (MaximumPermissibleConcentrationDailyAverage != null)
            {
                airContaminantsRU = airContaminantsRU.Where(a => a.MaximumPermissibleConcentrationDailyAverage == MaximumPermissibleConcentrationDailyAverage);
            }
            if (HazardClass != null)
            {
                airContaminantsRU = airContaminantsRU.Where(a => a.HazardClass == HazardClass);
            }
            if (Code != null)
            {
                airContaminantsRU = airContaminantsRU.Where(a => a.Code == Code);
            }
            if (ApproximateSafeExposureLevel != null)
            {
                airContaminantsRU = airContaminantsRU.Where(a => a.ApproximateSafeExposureLevel == ApproximateSafeExposureLevel);
            }

            switch (SortOrder)
            {
                case "Name":
                    airContaminantsRU = airContaminantsRU.OrderBy(a => a.Name);
                    break;
                case "NameDesc":
                    airContaminantsRU = airContaminantsRU.OrderByDescending(a => a.Name);
                    break;
                case "NumberCAS":
                    airContaminantsRU = airContaminantsRU.OrderBy(a => a.NumberCAS);
                    break;
                case "NumberCASDesc":
                    airContaminantsRU = airContaminantsRU.OrderByDescending(a => a.NumberCAS);
                    break;
                case "Formula":
                    airContaminantsRU = airContaminantsRU.OrderBy(a => a.Formula);
                    break;
                case "FormulaDesc":
                    airContaminantsRU = airContaminantsRU.OrderByDescending(a => a.Formula);
                    break;
                case "MaximumPermissibleConcentrationOneTimeMaximum":
                    airContaminantsRU = airContaminantsRU.OrderBy(a => a.MaximumPermissibleConcentrationOneTimeMaximum);
                    break;
                case "MaximumPermissibleConcentrationOneTimeMaximumDesc":
                    airContaminantsRU = airContaminantsRU.OrderByDescending(a => a.MaximumPermissibleConcentrationOneTimeMaximum);
                    break;
                case "MaximumPermissibleConcentrationDailyAverage":
                    airContaminantsRU = airContaminantsRU.OrderBy(a => a.MaximumPermissibleConcentrationDailyAverage);
                    break;
                case "MaximumPermissibleConcentrationDailyAverageDesc":
                    airContaminantsRU = airContaminantsRU.OrderByDescending(a => a.MaximumPermissibleConcentrationDailyAverage);
                    break;
                case "HazardClass":
                    airContaminantsRU = airContaminantsRU.OrderBy(a => a.HazardClass);
                    break;
                case "HazardClassDesc":
                    airContaminantsRU = airContaminantsRU.OrderByDescending(a => a.HazardClass);
                    break;
                case "Code":
                    airContaminantsRU = airContaminantsRU.OrderBy(a => a.Code);
                    break;
                case "CodeDesc":
                    airContaminantsRU = airContaminantsRU.OrderByDescending(a => a.Code);
                    break;
                case "ApproximateSafeExposureLevel":
                    airContaminantsRU = airContaminantsRU.OrderBy(a => a.ApproximateSafeExposureLevel);
                    break;
                case "ApproximateSafeExposureLevelDesc":
                    airContaminantsRU = airContaminantsRU.OrderByDescending(a => a.ApproximateSafeExposureLevel);
                    break;
                default:
                    airContaminantsRU = airContaminantsRU.OrderBy(a => a.Id);
                    break;
            }

            if (PageSize != null && Page != null)
            {
                airContaminantsRU = airContaminantsRU.Skip(((int)Page - 1) * (int)PageSize).Take((int)PageSize);
            }

            return airContaminantsRU;
        }

        // GET: api/AirContaminantsRU/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAirContaminantRU([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var airContaminantRU = await _context.AirContaminantRU.SingleOrDefaultAsync(m => m.Id == id);

            if (airContaminantRU == null)
            {
                return NotFound();
            }

            return Ok(airContaminantRU);
        }

        // PUT: api/AirContaminantsRU/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirContaminantRU([FromRoute] int id, [FromBody] AirContaminantRU airContaminantRU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != airContaminantRU.Id)
            {
                return BadRequest();
            }

            _context.Entry(airContaminantRU).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirContaminantRUExists(id))
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

        // POST: api/AirContaminantsRU
        [HttpPost]
        public async Task<IActionResult> PostAirContaminantRU([FromBody] AirContaminantRU airContaminantRU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AirContaminantRU.Add(airContaminantRU);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAirContaminantRU", new { id = airContaminantRU.Id }, airContaminantRU);
        }

        // DELETE: api/AirContaminantsRU/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirContaminantRU([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var airContaminantRU = await _context.AirContaminantRU.SingleOrDefaultAsync(m => m.Id == id);
            if (airContaminantRU == null)
            {
                return NotFound();
            }

            _context.AirContaminantRU.Remove(airContaminantRU);
            await _context.SaveChangesAsync();

            return Ok(airContaminantRU);
        }

        private bool AirContaminantRUExists(int id)
        {
            return _context.AirContaminantRU.Any(e => e.Id == id);
        }

        // GET: api/AirContaminantsRU/Count
        [HttpGet("count")]
        public async Task<IActionResult> GetAirContaminantsRUCount(string Name,
            string NumberCAS,
            string Formula,
            decimal? MaximumPermissibleConcentrationOneTimeMaximum,
            decimal? MaximumPermissibleConcentrationDailyAverage,
            int? HazardClass,
            int? Code,
            decimal? ApproximateSafeExposureLevel)
        {
            var airContaminantsRU = _context.AirContaminantRU
                .Where(a => true);

            if (!string.IsNullOrEmpty(Name))
            {
                airContaminantsRU = airContaminantsRU.Where(a => a.Name.ToLower().Contains(Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(NumberCAS))
            {
                airContaminantsRU = airContaminantsRU.Where(a => a.NumberCAS.ToLower().Contains(NumberCAS.ToLower()));
            }
            if (!string.IsNullOrEmpty(Formula))
            {
                airContaminantsRU = airContaminantsRU.Where(a => a.Formula.ToLower().Contains(Formula.ToLower()));
            }
            if (MaximumPermissibleConcentrationOneTimeMaximum != null)
            {
                airContaminantsRU = airContaminantsRU.Where(a => a.MaximumPermissibleConcentrationOneTimeMaximum == MaximumPermissibleConcentrationOneTimeMaximum);
            }
            if (MaximumPermissibleConcentrationDailyAverage != null)
            {
                airContaminantsRU = airContaminantsRU.Where(a => a.MaximumPermissibleConcentrationDailyAverage == MaximumPermissibleConcentrationDailyAverage);
            }
            if (HazardClass != null)
            {
                airContaminantsRU = airContaminantsRU.Where(a => a.HazardClass == HazardClass);
            }
            if (Code != null)
            {
                airContaminantsRU = airContaminantsRU.Where(a => a.Code == Code);
            }
            if (ApproximateSafeExposureLevel != null)
            {
                airContaminantsRU = airContaminantsRU.Where(a => a.ApproximateSafeExposureLevel == ApproximateSafeExposureLevel);
            }
            int count = await airContaminantsRU.CountAsync();
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
                    List<AirContaminantRU> airContaminantsRU = new List<AirContaminantRU>();
                    for (int i = start_row; ; i++)
                    {
                        if (package.Workbook.Worksheets.FirstOrDefault().Cells[i, 1].Value == null)
                        {
                            break;
                        }
                        string Name = null,
                            NumberCAS = null,
                            Formula = null;
                        decimal? MaximumPermissibleConcentrationOneTimeMaximum = null,
                            MaximumPermissibleConcentrationDailyAverage = null,
                            ApproximateSafeExposureLevel = null;
                        int? HazardClass = null,
                            Code = null;
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
                            Formula = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 3].Value);
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
                            HazardClass = package.Workbook.Worksheets.FirstOrDefault().Cells[i, 6].Value == null ? null : (int?)Convert.ToInt32(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 6].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 6: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            Code = package.Workbook.Worksheets.FirstOrDefault().Cells[i, 7].Value == null ? null : (int?)Convert.ToInt32(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 7].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 7: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            ApproximateSafeExposureLevel = package.Workbook.Worksheets.FirstOrDefault().Cells[i, 8].Value == null ? null : (decimal?)Convert.ToDecimal(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 8].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 8: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        airContaminantsRU.Add(new AirContaminantRU()
                        {
                            Name = Name,
                            NumberCAS = NumberCAS,
                            Formula = Formula,
                            MaximumPermissibleConcentrationOneTimeMaximum = MaximumPermissibleConcentrationOneTimeMaximum,
                            MaximumPermissibleConcentrationDailyAverage = MaximumPermissibleConcentrationDailyAverage,
                            HazardClass = HazardClass,
                            Code = Code,
                            ApproximateSafeExposureLevel = ApproximateSafeExposureLevel
                        });
                    }
                    _context.AddRange(airContaminantsRU);
                    countUploaded += airContaminantsRU.Count();
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