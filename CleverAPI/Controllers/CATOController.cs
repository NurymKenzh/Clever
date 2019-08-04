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
using System.Security.Authentication;
using Newtonsoft.Json;

namespace CleverAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/CATO")]
    public class CATOController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CATOController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CATO
        [HttpGet]
        public IEnumerable<CATO> GetCATO(string SortOrder,
            int? EgovId,
            string Code,
            string NameKK,
            string NameRU,
            int? Parent,
            int? AreaType,
            int? PageSize,
            int? Page)
        {
            var catoes = _context.CATO
                .Where(c => c.ExpiredDateTime == null);

            if (EgovId!=null)
            {
                catoes = catoes.Where(e => e.EgovId == EgovId);
            }
            if (!string.IsNullOrEmpty(Code))
            {
                catoes = catoes.Where(e => e.Code.ToLower().Contains(Code.ToLower()));
            }
            if (!string.IsNullOrEmpty(NameKK))
            {
                catoes = catoes.Where(e => e.NameKK.ToLower().Contains(NameKK.ToLower()));
            }
            if (!string.IsNullOrEmpty(NameRU))
            {
                catoes = catoes.Where(e => e.NameRU.ToLower().Contains(NameRU.ToLower()));
            }
            if (Parent != null)
            {
                catoes = catoes.Where(e => e.Parent == Parent);
            }
            if (AreaType != null)
            {
                catoes = catoes.Where(e => e.AreaType == AreaType);
            }

            switch (SortOrder)
            {
                case "EgovId":
                    catoes = catoes.OrderBy(e => e.EgovId);
                    break;
                case "EgovIdDesc":
                    catoes = catoes.OrderByDescending(e => e.EgovId);
                    break;
                case "Code":
                    catoes = catoes.OrderBy(e => e.Code);
                    break;
                case "CodeDesc":
                    catoes = catoes.OrderByDescending(e => e.Code);
                    break;
                case "NameKK":
                    catoes = catoes.OrderBy(e => e.NameKK);
                    break;
                case "NameKKDesc":
                    catoes = catoes.OrderByDescending(e => e.NameKK);
                    break;
                case "NameRU":
                    catoes = catoes.OrderBy(e => e.NameRU);
                    break;
                case "NameRUDesc":
                    catoes = catoes.OrderByDescending(e => e.NameRU);
                    break;
                case "Parent":
                    catoes = catoes.OrderBy(e => e.Parent);
                    break;
                case "ParentDesc":
                    catoes = catoes.OrderByDescending(e => e.Parent);
                    break;
                case "AreaType":
                    catoes = catoes.OrderBy(e => e.AreaType);
                    break;
                case "AreaTypeDesc":
                    catoes = catoes.OrderByDescending(e => e.AreaType);
                    break;
                default:
                    catoes = catoes.OrderBy(e => e.Id);
                    break;
            }

            if(PageSize!=null && Page != null)
            {
                catoes = catoes.Skip(((int)Page - 1) * (int)PageSize).Take((int)PageSize);
            }
            
            return catoes;
        }

        // GET: api/CATO/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCATO([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cATO = await _context.CATO.SingleOrDefaultAsync(m => m.Id == id);

            if (cATO == null)
            {
                return NotFound();
            }

            return Ok(cATO);
        }

        // PUT: api/CATO/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCATO([FromRoute] int id, [FromBody] CATO cATO)
        {
            var catoes = _context.CATO.AsNoTracking().ToList();
            if (_context.CATO.AsNoTracking().FirstOrDefault(c => c.ExpiredDateTime == null && c.Code == cATO.Code && c.Id != cATO.Id) != null)
            {
                ModelState.AddModelError("Code", "An object with this value already exists!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cATO.Id)
            {
                return BadRequest();
            }

            _context.Entry(cATO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CATOExists(id))
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

        // POST: api/CATO
        [HttpPost]
        public async Task<IActionResult> PostCATO([FromBody] CATO cATO)
        {
            var catoes = _context.CATO.AsNoTracking().ToList();
            if (_context.CATO.AsNoTracking().FirstOrDefault(c => c.ExpiredDateTime == null && c.Code == cATO.Code) != null)
            {
                ModelState.AddModelError("Code", "An object with this value already exists!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CATO.Add(cATO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCATO", new { id = cATO.Id }, cATO);
        }

        // DELETE: api/CATO/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCATO([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cATO = await _context.CATO.SingleOrDefaultAsync(m => m.Id == id);
            if (cATO == null)
            {
                return NotFound();
            }

            _context.CATO.Remove(cATO);
            await _context.SaveChangesAsync();

            return Ok(cATO);
        }

        // GET: api/CATO/Count
        [HttpGet("count")]
        public async Task<IActionResult> GetCATOCount(int? EgovId,
            string Code,
            string NameKK,
            string NameRU,
            int? Parent,
            int? AreaType)
        {
            var catoes = _context.CATO
                .Where(c => c.ExpiredDateTime == null);
            if (EgovId != null)
            {
                catoes = catoes.Where(e => e.EgovId == EgovId);
            }
            if (!string.IsNullOrEmpty(Code))
            {
                catoes = catoes.Where(e => e.Code.ToLower().Contains(Code.ToLower()));
            }
            if (!string.IsNullOrEmpty(NameKK))
            {
                catoes = catoes.Where(e => e.NameKK.ToLower().Contains(NameKK.ToLower()));
            }
            if (!string.IsNullOrEmpty(NameRU))
            {
                catoes = catoes.Where(e => e.NameRU.ToLower().Contains(NameRU.ToLower()));
            }
            if (Parent != null)
            {
                catoes = catoes.Where(e => e.Parent == Parent);
            }
            if (AreaType != null)
            {
                catoes = catoes.Where(e => e.AreaType == AreaType);
            }
            int count = await catoes.CountAsync();
            return Ok(count);
        }

        // GET: api/CATO/Parse
        [HttpGet("parse")]
        public async Task<IActionResult> ParseCATO(string Url)
        {
            try
            {
                int importedCount = 0,
                i = 0;
                if (!string.IsNullOrEmpty(Url))
                {
                    string jsontext = "[{}]";
                    List<CATO> catoes = new List<CATO>();
                    while (jsontext.Length > 2)
                    {
                        string url = Url;
                        if (url.Last() == '/')
                        {
                            url = url.Remove(url.Length - 1, 1);
                        }
                        if (url.Contains("?"))
                        {
                            url += "&source={" + $"\"from\":{i * 10000},\"size\":10000" + "}";
                        }
                        else
                        {
                            url += "?source={" + $"\"from\":{i * 10000},\"size\":10000" + "}";
                        }
                        i++;
                        using (var handler = new HttpClientHandler())
                        {
                            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                            handler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
                            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                            using (var client = new HttpClient(handler))
                            {
                                var response = client.GetAsync(url).GetAwaiter().GetResult();
                                response.EnsureSuccessStatusCode();
                                jsontext = await response.Content.ReadAsStringAsync();
                            }
                        }
                        dynamic data = JsonConvert.DeserializeObject<dynamic>(jsontext);
                        foreach (Newtonsoft.Json.Linq.JObject jobject in data)
                        {
                            CATO cato = new CATO()
                            {
                                Code = jobject["Code"]?.ToString(),
                                NameKK = jobject["NameKaz"]?.ToString(),
                                NameRU = jobject["NameRus"]?.ToString(),
                                Parent = !string.IsNullOrEmpty(jobject["Parent"]?.ToString()) ? (int?)Convert.ToInt32(jobject["Parent"]?.ToString()) : null,
                                AreaType = !string.IsNullOrEmpty(jobject["AreaType"]?.ToString()) ? (int?)Convert.ToInt32(jobject["AreaType"]?.ToString()) : null,
                                EgovId = !string.IsNullOrEmpty(jobject["Id"]?.ToString()) ? (int?)Convert.ToInt32(jobject["Id"]?.ToString()) : null
                            };
                            catoes.Add(cato);

                        }
                    }
                    // compare old and importing catoes
                    List<CATO> catoesOld = _context.CATO.Where(c => c.ExpiredDateTime == null).ToList();
                    bool different = false; // old and new catoes
                    if (catoesOld.Count() != catoes.Count())
                    {
                        different = true;
                    }
                    else
                    {
                        catoesOld = catoesOld.Select(c => { c.Id = 0; return c; }).ToList();
                        List<CATO> catoesE = new List<CATO>(),
                            catoesOldE = new List<CATO>();
                        catoesE.AddRange(catoes);
                        catoesOldE.AddRange(catoesOld);
                        foreach (CATO cato in catoes)
                        {
                            CATO dublicateCato = catoesOldE.FirstOrDefault(c => c.Code == cato.Code
                                && c.NameKK == cato.NameKK
                                && c.NameRU == cato.NameRU);
                            if (dublicateCato != null)
                            {
                                catoesOldE.Remove(dublicateCato);
                            }
                        }
                        foreach (CATO cato in catoesOld)
                        {
                            CATO dublicateCato = catoesE.FirstOrDefault(c => c.Code == cato.Code
                                && c.NameKK == cato.NameKK
                                && c.NameRU == cato.NameRU);
                            if (dublicateCato != null)
                            {
                                catoesE.Remove(dublicateCato);
                            }
                        }
                        if (catoesE.Count() > 0 || catoesOldE.Count() > 0)
                        {
                            different = true;
                        }
                    }

                    if (different)
                    {
                        foreach (CATO cato in _context.CATO.Where(c => c.ExpiredDateTime == null))
                        {
                            cato.ExpiredDateTime = DateTime.Now;
                            _context.Update(cato);
                        }
                        _context.CATO.AddRange(catoes);
                        await _context.SaveChangesAsync();
                        importedCount = catoes.Count();
                    }
                }
                return Ok(importedCount);
            }
            catch (Exception exp)
            {
                return Json(exp.Message);
            }
        }

        private bool CATOExists(int id)
        {
            return _context.CATO.Any(e => e.Id == id);
        }
    }
}