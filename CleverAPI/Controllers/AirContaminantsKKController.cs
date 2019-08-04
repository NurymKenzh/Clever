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
    [Route("api/AirContaminantsKK")]
    public class AirContaminantsKKController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AirContaminantsKKController(ApplicationDbContext context,
            IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: api/AirContaminantsKK
        [HttpGet]
        public IEnumerable<AirContaminantKK> GetAirContaminantKK(string SortOrder,
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
            var airContaminantsKK = _context.AirContaminantKK
                .Where(a => true);

            if (!string.IsNullOrEmpty(Name))
            {
                airContaminantsKK = airContaminantsKK.Where(a => a.Name.ToLower().Contains(Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(NumberCAS))
            {
                airContaminantsKK = airContaminantsKK.Where(a => a.NumberCAS.ToLower().Contains(NumberCAS.ToLower()));
            }
            if (!string.IsNullOrEmpty(Formula))
            {
                airContaminantsKK = airContaminantsKK.Where(a => a.Formula.ToLower().Contains(Formula.ToLower()));
            }
            if (MaximumPermissibleConcentrationOneTimeMaximum != null)
            {
                airContaminantsKK = airContaminantsKK.Where(a => a.MaximumPermissibleConcentrationOneTimeMaximum == MaximumPermissibleConcentrationOneTimeMaximum);
            }
            if (MaximumPermissibleConcentrationDailyAverage != null)
            {
                airContaminantsKK = airContaminantsKK.Where(a => a.MaximumPermissibleConcentrationDailyAverage == MaximumPermissibleConcentrationDailyAverage);
            }
            if (HazardClass != null)
            {
                airContaminantsKK = airContaminantsKK.Where(a => a.HazardClass == HazardClass);
            }
            if (Code != null)
            {
                airContaminantsKK = airContaminantsKK.Where(a => a.Code == Code);
            }
            if (ApproximateSafeExposureLevel != null)
            {
                airContaminantsKK = airContaminantsKK.Where(a => a.ApproximateSafeExposureLevel == ApproximateSafeExposureLevel);
            }

            switch (SortOrder)
            {
                case "Name":
                    airContaminantsKK = airContaminantsKK.OrderBy(a => a.Name);
                    break;
                case "NameDesc":
                    airContaminantsKK = airContaminantsKK.OrderByDescending(a => a.Name);
                    break;
                case "NumberCAS":
                    airContaminantsKK = airContaminantsKK.OrderBy(a => a.NumberCAS);
                    break;
                case "NumberCASDesc":
                    airContaminantsKK = airContaminantsKK.OrderByDescending(a => a.NumberCAS);
                    break;
                case "Formula":
                    airContaminantsKK = airContaminantsKK.OrderBy(a => a.Formula);
                    break;
                case "FormulaDesc":
                    airContaminantsKK = airContaminantsKK.OrderByDescending(a => a.Formula);
                    break;
                case "MaximumPermissibleConcentrationOneTimeMaximum":
                    airContaminantsKK = airContaminantsKK.OrderBy(a => a.MaximumPermissibleConcentrationOneTimeMaximum);
                    break;
                case "MaximumPermissibleConcentrationOneTimeMaximumDesc":
                    airContaminantsKK = airContaminantsKK.OrderByDescending(a => a.MaximumPermissibleConcentrationOneTimeMaximum);
                    break;
                case "MaximumPermissibleConcentrationDailyAverage":
                    airContaminantsKK = airContaminantsKK.OrderBy(a => a.MaximumPermissibleConcentrationDailyAverage);
                    break;
                case "MaximumPermissibleConcentrationDailyAverageDesc":
                    airContaminantsKK = airContaminantsKK.OrderByDescending(a => a.MaximumPermissibleConcentrationDailyAverage);
                    break;
                case "HazardClass":
                    airContaminantsKK = airContaminantsKK.OrderBy(a => a.HazardClass);
                    break;
                case "HazardClassDesc":
                    airContaminantsKK = airContaminantsKK.OrderByDescending(a => a.HazardClass);
                    break;
                case "Code":
                    airContaminantsKK = airContaminantsKK.OrderBy(a => a.Code);
                    break;
                case "CodeDesc":
                    airContaminantsKK = airContaminantsKK.OrderByDescending(a => a.Code);
                    break;
                case "ApproximateSafeExposureLevel":
                    airContaminantsKK = airContaminantsKK.OrderBy(a => a.ApproximateSafeExposureLevel);
                    break;
                case "ApproximateSafeExposureLevelDesc":
                    airContaminantsKK = airContaminantsKK.OrderByDescending(a => a.ApproximateSafeExposureLevel);
                    break;
                default:
                    airContaminantsKK = airContaminantsKK.OrderBy(a => a.Id);
                    break;
            }

            if (PageSize != null && Page != null)
            {
                airContaminantsKK = airContaminantsKK.Skip(((int)Page - 1) * (int)PageSize).Take((int)PageSize);
            }

            return airContaminantsKK;
        }

        // GET: api/AirContaminantsKK/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAirContaminantKK([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var airContaminantKK = await _context.AirContaminantKK.SingleOrDefaultAsync(m => m.Id == id);

            if (airContaminantKK == null)
            {
                return NotFound();
            }

            return Ok(airContaminantKK);
        }

        // PUT: api/AirContaminantsKK/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirContaminantKK([FromRoute] int id, [FromBody] AirContaminantKK airContaminantKK)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != airContaminantKK.Id)
            {
                return BadRequest();
            }

            _context.Entry(airContaminantKK).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirContaminantKKExists(id))
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

        // POST: api/AirContaminantsKK
        [HttpPost]
        public async Task<IActionResult> PostAirContaminantKK([FromBody] AirContaminantKK airContaminantKK)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AirContaminantKK.Add(airContaminantKK);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAirContaminantKK", new { id = airContaminantKK.Id }, airContaminantKK);
        }

        // DELETE: api/AirContaminantsKK/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirContaminantKK([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var airContaminantKK = await _context.AirContaminantKK.SingleOrDefaultAsync(m => m.Id == id);
            if (airContaminantKK == null)
            {
                return NotFound();
            }

            _context.AirContaminantKK.Remove(airContaminantKK);
            await _context.SaveChangesAsync();

            return Ok(airContaminantKK);
        }

        private bool AirContaminantKKExists(int id)
        {
            return _context.AirContaminantKK.Any(e => e.Id == id);
        }

        // GET: api/AirContaminantsKK/Count
        [HttpGet("count")]
        public async Task<IActionResult> GetAirContaminantsKKCount(string Name,
            string NumberCAS,
            string Formula,
            decimal? MaximumPermissibleConcentrationOneTimeMaximum,
            decimal? MaximumPermissibleConcentrationDailyAverage,
            int? HazardClass,
            int? Code,
            decimal? ApproximateSafeExposureLevel)
        {
            var airContaminantsKK = _context.AirContaminantKK
                .Where(a => true);

            if (!string.IsNullOrEmpty(Name))
            {
                airContaminantsKK = airContaminantsKK.Where(a => a.Name.ToLower().Contains(Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(NumberCAS))
            {
                airContaminantsKK = airContaminantsKK.Where(a => a.NumberCAS.ToLower().Contains(NumberCAS.ToLower()));
            }
            if (!string.IsNullOrEmpty(Formula))
            {
                airContaminantsKK = airContaminantsKK.Where(a => a.Formula.ToLower().Contains(Formula.ToLower()));
            }
            if (MaximumPermissibleConcentrationOneTimeMaximum != null)
            {
                airContaminantsKK = airContaminantsKK.Where(a => a.MaximumPermissibleConcentrationOneTimeMaximum == MaximumPermissibleConcentrationOneTimeMaximum);
            }
            if (MaximumPermissibleConcentrationDailyAverage != null)
            {
                airContaminantsKK = airContaminantsKK.Where(a => a.MaximumPermissibleConcentrationDailyAverage == MaximumPermissibleConcentrationDailyAverage);
            }
            if (HazardClass != null)
            {
                airContaminantsKK = airContaminantsKK.Where(a => a.HazardClass == HazardClass);
            }
            if (Code != null)
            {
                airContaminantsKK = airContaminantsKK.Where(a => a.Code == Code);
            }
            if (ApproximateSafeExposureLevel != null)
            {
                airContaminantsKK = airContaminantsKK.Where(a => a.ApproximateSafeExposureLevel == ApproximateSafeExposureLevel);
            }
            int count = await airContaminantsKK.CountAsync();
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
                    List<AirContaminantKK> airContaminantsKK = new List<AirContaminantKK>();
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

                        airContaminantsKK.Add(new AirContaminantKK()
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
                    _context.AddRange(airContaminantsKK);
                    countUploaded += airContaminantsKK.Count();
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