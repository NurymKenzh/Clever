using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CleverAPI.Data;
using CleverAPI.Models;
using System.Net;
using System.IO;
using System.Text;
using System.Diagnostics;
using WebKit;
using OpenQA.Selenium.PhantomJS;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using HtmlAgilityPack;
using Ionic.Zip;
using ExcelDataReader;

namespace CleverAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/CompaniesKK")]
    public class CompaniesKKController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CompaniesKKController(ApplicationDbContext context,
            IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: api/CompaniesKK
        [HttpGet]
        public IEnumerable<CompanyKK> GetCompanyKK(string SortOrder,
            string BIN,
            string NameKK,
            string NameRU,
            DateTime? DateRegister,
            string OKED,
            string ActivityKindKK,
            string ActivityKindRU,
            string OKEDSecondary,
            string KRP,
            string KRPNameKK,
            string KRPNameRU,
            string CATO,
            string LocalityKK,
            string LocalityRU,
            string LegalAddress,
            string HeadName,
            string Search,
            int? PageSize,
            int? Page)
        {
            var companiesKK = _context.CompanyKK
                .Where(c => c.ExpiredDateTime == null);

            if (!string.IsNullOrEmpty(Search))
            {
                //companiesKK = companiesKK.Where(c => c.BIN.ToLower().Contains(Search.ToLower())
                //    || c.NameKK.ToLower().Contains(Search.ToLower())
                //    || c.NameRU.ToLower().Contains(Search.ToLower())
                //    || c.OKED.ToLower().Contains(Search.ToLower())
                //    || c.ActivityKindKK.ToLower().Contains(Search.ToLower())
                //    || c.ActivityKindRU.ToLower().Contains(Search.ToLower())
                //    || c.KRP.ToLower().Contains(Search.ToLower())
                //    || c.KRPNameKK.ToLower().Contains(Search.ToLower())
                //    || c.KRPNameRU.ToLower().Contains(Search.ToLower())
                //    || c.CATO.ToLower().Contains(Search.ToLower())
                //    || c.LocalityKK.ToLower().Contains(Search.ToLower())
                //    || c.LocalityRU.ToLower().Contains(Search.ToLower())
                //    || c.LegalAddress.ToLower().Contains(Search.ToLower())
                //    || c.HeadName.ToLower().Contains(Search.ToLower()));

                //companiesKK = companiesKK.Where(c => (c.BIN.ToLower() + " " +
                //    c.NameKK.ToLower() + " " +
                //    c.NameRU.ToLower() + " " +
                //    c.OKED.ToLower() + " " +
                //    c.ActivityKindKK.ToLower() + " " +
                //    c.ActivityKindRU.ToLower() + " " +
                //    c.KRP.ToLower() + " " +
                //    c.KRPNameKK.ToLower() + " " +
                //    c.KRPNameRU.ToLower() + " " +
                //    c.CATO.ToLower() + " " +
                //    c.LocalityKK.ToLower() + " " +
                //    c.LocalityRU.ToLower() + " " +
                //    c.LegalAddress.ToLower() + " " +
                //    c.HeadName.ToLower()).Contains(Search.ToLower()));

                //string search = Search.ToLower();
                //companiesKK = companiesKK.Where(c => (c.BIN + " " +
                //    c.NameKK + " " +
                //    c.NameRU + " " +
                //    c.OKED + " " +
                //    c.ActivityKindKK + " " +
                //    c.ActivityKindRU + " " +
                //    c.KRP + " " +
                //    c.KRPNameKK + " " +
                //    c.KRPNameRU + " " +
                //    c.CATO + " " +
                //    c.LocalityKK + " " +
                //    c.LocalityRU + " " +
                //    c.LegalAddress + " " +
                //    c.HeadName).Contains(search.ToLower()));

                //string query = "SELECT * FROM \"CompanyKK\" WHERE \"BIN\" || ' ' || \"NameKK\" || ' ' || \"OKED\" || ' ' || \"ActivityKindKK\" || ' ' || \"ActivityKindRU\" || ' ' || \"KRP\" || ' ' || \"KRPNameKK\" || ' ' || \"KRPNameRU\" || ' ' || \"CATO\" || ' ' || \"LocalityKK\" || ' ' || \"LocalityRU\" || ' ' || \"LegalAddress\" || ' ' || \"HeadName\" ILIKE '%илькама%';' ;
                //companiesKK = _context.CompanyKK.AsQueryable(query);

                //companiesKK = companiesKK.Where(c => EF.Functions.ILike((c.BIN + " " +
                //    c.NameKK + " " +
                //    c.NameRU + " " +
                //    c.OKED + " " +
                //    c.ActivityKindKK + " " +
                //    c.ActivityKindRU + " " +
                //    c.KRP + " " +
                //    c.KRPNameKK + " " +
                //    c.KRPNameRU + " " +
                //    c.CATO + " " +
                //    c.LocalityKK + " " +
                //    c.LocalityRU + " " +
                //    c.LegalAddress + " " +
                //    c.HeadName), $"%{Search}%"));

                //string search = Search.ToLower();
                //companiesKK = companiesKK.Where(c => (c.BIN + " " +
                //    c.NameKK + " " +
                //    c.NameRU + " " +
                //    c.OKED + " " +
                //    c.ActivityKindKK + " " +
                //    c.ActivityKindRU + " " +
                //    c.KRP + " " +
                //    c.KRPNameKK + " " +
                //    c.KRPNameRU + " " +
                //    c.CATO + " " +
                //    c.LocalityKK + " " +
                //    c.LocalityRU + " " +
                //    c.LegalAddress + " " +
                //    c.HeadName).Contains($"%{Search}%"));

                string query = $"SELECT * FROM \"CompanyKK\" WHERE \"BIN\" || ' ' || \"NameKK\" || ' ' || \"OKED\" || ' ' || \"ActivityKindKK\" || ' ' || \"ActivityKindRU\" || ' ' || \"KRP\" || ' ' || \"KRPNameKK\" || ' ' || \"KRPNameRU\" || ' ' || \"CATO\" || ' ' || \"LocalityKK\" || ' ' || \"LocalityRU\" || ' ' || \"LegalAddress\" || ' ' || \"HeadName\" ILIKE '%{Search}%';";
                companiesKK = _context.CompanyKK.FromSql(query).ToList().AsQueryable();
            }
            if (!string.IsNullOrEmpty(BIN))
            {
                companiesKK = companiesKK.Where(c => c.BIN.ToLower().Contains(BIN.ToLower()));
            }
            if (!string.IsNullOrEmpty(NameKK))
            {
                companiesKK = companiesKK.Where(c => c.NameKK.ToLower().Contains(NameKK.ToLower()));
            }
            if (!string.IsNullOrEmpty(NameRU))
            {
                companiesKK = companiesKK.Where(c => c.NameRU.ToLower().Contains(NameRU.ToLower()));
            }
            if (DateRegister != null)
            {
                companiesKK = companiesKK.Where(c => c.DateRegister == DateRegister);
            }
            if (!string.IsNullOrEmpty(OKED))
            {
                companiesKK = companiesKK.Where(c => c.OKED.ToLower().Contains(OKED.ToLower()));
            }
            if (!string.IsNullOrEmpty(ActivityKindKK))
            {
                companiesKK = companiesKK.Where(c => c.ActivityKindKK.ToLower().Contains(ActivityKindKK.ToLower()));
            }
            if (!string.IsNullOrEmpty(ActivityKindRU))
            {
                companiesKK = companiesKK.Where(c => c.ActivityKindRU.ToLower().Contains(ActivityKindRU.ToLower()));
            }
            if (!string.IsNullOrEmpty(OKEDSecondary))
            {
                companiesKK = companiesKK.Where(c => c.OKEDSecondary.ToLower().Contains(OKEDSecondary.ToLower()));
            }
            if (!string.IsNullOrEmpty(KRP))
            {
                companiesKK = companiesKK.Where(c => c.KRP.ToLower().Contains(KRP.ToLower()));
            }
            if (!string.IsNullOrEmpty(KRPNameKK))
            {
                companiesKK = companiesKK.Where(c => c.KRPNameKK.ToLower().Contains(KRPNameKK.ToLower()));
            }
            if (!string.IsNullOrEmpty(KRPNameRU))
            {
                companiesKK = companiesKK.Where(c => c.KRPNameRU.ToLower().Contains(KRPNameRU.ToLower()));
            }
            if (!string.IsNullOrEmpty(CATO))
            {
                companiesKK = companiesKK.Where(c => c.CATO.ToLower().Contains(CATO.ToLower()));
            }
            if (!string.IsNullOrEmpty(LocalityKK))
            {
                companiesKK = companiesKK.Where(c => c.LocalityKK.ToLower().Contains(LocalityKK.ToLower()));
            }
            if (!string.IsNullOrEmpty(LocalityRU))
            {
                companiesKK = companiesKK.Where(c => c.LocalityRU.ToLower().Contains(LocalityRU.ToLower()));
            }
            if (!string.IsNullOrEmpty(LegalAddress))
            {
                companiesKK = companiesKK.Where(c => c.LegalAddress.ToLower().Contains(LegalAddress.ToLower()));
            }
            if (!string.IsNullOrEmpty(HeadName))
            {
                companiesKK = companiesKK.Where(c => c.HeadName.ToLower().Contains(HeadName.ToLower()));
            }

            switch (SortOrder)
            {
                case "BIN":
                    companiesKK = companiesKK.OrderBy(c => c.BIN);
                    break;
                case "BINDesc":
                    companiesKK = companiesKK.OrderByDescending(c => c.BIN);
                    break;
                case "NameKK":
                    companiesKK = companiesKK.OrderBy(c => c.NameKK);
                    break;
                case "NameKKDesc":
                    companiesKK = companiesKK.OrderByDescending(c => c.NameKK);
                    break;
                case "NameRU":
                    companiesKK = companiesKK.OrderBy(c => c.NameRU);
                    break;
                case "NameRUDesc":
                    companiesKK = companiesKK.OrderByDescending(c => c.NameRU);
                    break;
                case "DateRegister":
                    companiesKK = companiesKK.OrderBy(c => c.DateRegister);
                    break;
                case "DateRegisterDesc":
                    companiesKK = companiesKK.OrderByDescending(c => c.DateRegister);
                    break;
                case "OKED":
                    companiesKK = companiesKK.OrderBy(c => c.OKED);
                    break;
                case "OKEDDesc":
                    companiesKK = companiesKK.OrderByDescending(c => c.OKED);
                    break;
                case "ActivityKindKK":
                    companiesKK = companiesKK.OrderBy(c => c.ActivityKindKK);
                    break;
                case "ActivityKindKKDesc":
                    companiesKK = companiesKK.OrderByDescending(c => c.ActivityKindKK);
                    break;
                case "ActivityKindRU":
                    companiesKK = companiesKK.OrderBy(c => c.ActivityKindRU);
                    break;
                case "ActivityKindRUDesc":
                    companiesKK = companiesKK.OrderByDescending(c => c.ActivityKindRU);
                    break;
                case "OKEDSecondary":
                    companiesKK = companiesKK.OrderBy(c => c.OKEDSecondary);
                    break;
                case "OKEDSecondaryDesc":
                    companiesKK = companiesKK.OrderByDescending(c => c.OKEDSecondary);
                    break;
                case "KRP":
                    companiesKK = companiesKK.OrderBy(c => c.KRP);
                    break;
                case "KRPDesc":
                    companiesKK = companiesKK.OrderByDescending(c => c.KRP);
                    break;
                case "KRPNameKK":
                    companiesKK = companiesKK.OrderBy(c => c.KRPNameKK);
                    break;
                case "KRPNameKKDesc":
                    companiesKK = companiesKK.OrderByDescending(c => c.KRPNameKK);
                    break;
                case "KRPNameRU":
                    companiesKK = companiesKK.OrderBy(c => c.KRPNameRU);
                    break;
                case "KRPNameRUDesc":
                    companiesKK = companiesKK.OrderByDescending(c => c.KRPNameRU);
                    break;
                case "CATO":
                    companiesKK = companiesKK.OrderBy(c => c.CATO);
                    break;
                case "CATODesc":
                    companiesKK = companiesKK.OrderByDescending(c => c.CATO);
                    break;
                case "LocalityKK":
                    companiesKK = companiesKK.OrderBy(c => c.LocalityKK);
                    break;
                case "LocalityKKDesc":
                    companiesKK = companiesKK.OrderByDescending(c => c.LocalityKK);
                    break;
                case "LocalityRU":
                    companiesKK = companiesKK.OrderBy(c => c.LocalityRU);
                    break;
                case "LocalityRUDesc":
                    companiesKK = companiesKK.OrderByDescending(c => c.LocalityRU);
                    break;
                case "LegalAddress":
                    companiesKK = companiesKK.OrderBy(c => c.LegalAddress);
                    break;
                case "LegalAddressDesc":
                    companiesKK = companiesKK.OrderByDescending(c => c.LegalAddress);
                    break;
                case "HeadName":
                    companiesKK = companiesKK.OrderBy(c => c.HeadName);
                    break;
                case "HeadNameDesc":
                    companiesKK = companiesKK.OrderByDescending(c => c.HeadName);
                    break;
                default:
                    companiesKK = companiesKK.OrderBy(c => c.Id);
                    break;
            }

            if (PageSize != null && Page != null)
            {
                companiesKK = companiesKK.Skip(((int)Page - 1) * (int)PageSize).Take((int)PageSize);
            }

            return companiesKK;
        }

        // GET: api/CompaniesKK/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyKK([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyKK = await _context.CompanyKK.SingleOrDefaultAsync(m => m.Id == id);

            if (companyKK == null)
            {
                return NotFound();
            }

            return Ok(companyKK);
        }

        // PUT: api/CompaniesKK/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyKK([FromRoute] int id, [FromBody] CompanyKK companyKK)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != companyKK.Id)
            {
                return BadRequest();
            }

            _context.Entry(companyKK).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyKKExists(id))
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

        // POST: api/CompaniesKK
        [HttpPost]
        public async Task<IActionResult> PostCompanyKK([FromBody] CompanyKK companyKK)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CompanyKK.Add(companyKK);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompanyKK", new { id = companyKK.Id }, companyKK);
        }

        // DELETE: api/CompaniesKK/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyKK([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyKK = await _context.CompanyKK.SingleOrDefaultAsync(m => m.Id == id);
            if (companyKK == null)
            {
                return NotFound();
            }

            _context.CompanyKK.Remove(companyKK);
            await _context.SaveChangesAsync();

            return Ok(companyKK);
        }

        private bool CompanyKKExists(int id)
        {
            return _context.CompanyKK.Any(e => e.Id == id);
        }

        // GET: api/CompaniesKK/Count
        [HttpGet("count")]
        public async Task<IActionResult> GetCompaniesKKCount(string BIN,
            string NameKK,
            string NameRU,
            DateTime? DateRegister,
            string OKED,
            string ActivityKindKK,
            string ActivityKindRU,
            string OKEDSecondary,
            string KRP,
            string KRPNameKK,
            string KRPNameRU,
            string CATO,
            string LocalityKK,
            string LocalityRU,
            string LegalAddress,
            string HeadName,
            string Search)
        {
            var companiesKK = _context.CompanyKK
                .Where(c => c.ExpiredDateTime == null);

            if (!string.IsNullOrEmpty(Search))
            {
                //companiesKK = companiesKK.Where(c => c.BIN.ToLower().Contains(Search.ToLower())
                //    || c.NameKK.ToLower().Contains(Search.ToLower())
                //    || c.NameRU.ToLower().Contains(Search.ToLower())
                //    || c.OKED.ToLower().Contains(Search.ToLower())
                //    || c.ActivityKindKK.ToLower().Contains(Search.ToLower())
                //    || c.ActivityKindRU.ToLower().Contains(Search.ToLower())
                //    || c.KRP.ToLower().Contains(Search.ToLower())
                //    || c.KRPNameKK.ToLower().Contains(Search.ToLower())
                //    || c.KRPNameRU.ToLower().Contains(Search.ToLower())
                //    || c.CATO.ToLower().Contains(Search.ToLower())
                //    || c.LocalityKK.ToLower().Contains(Search.ToLower())
                //    || c.LocalityRU.ToLower().Contains(Search.ToLower())
                //    || c.LegalAddress.ToLower().Contains(Search.ToLower())
                //    || c.HeadName.ToLower().Contains(Search.ToLower()));

                //companiesKK = companiesKK.Where(c => (c.BIN.ToLower() + " " +
                //    c.NameKK.ToLower() + " " +
                //    c.NameRU.ToLower() + " " +
                //    c.OKED.ToLower() + " " +
                //    c.ActivityKindKK.ToLower() + " " +
                //    c.ActivityKindRU.ToLower() + " " +
                //    c.KRP.ToLower() + " " +
                //    c.KRPNameKK.ToLower() + " " +
                //    c.KRPNameRU.ToLower() + " " +
                //    c.CATO.ToLower() + " " +
                //    c.LocalityKK.ToLower() + " " +
                //    c.LocalityRU.ToLower() + " " +
                //    c.LegalAddress.ToLower() + " " +
                //    c.HeadName.ToLower()).Contains(Search.ToLower()));

                //string search = Search.ToLower();
                //companiesKK = companiesKK.Where(c => (c.BIN + " " +
                //    c.NameKK + " " +
                //    c.NameRU + " " +
                //    c.OKED + " " +
                //    c.ActivityKindKK + " " +
                //    c.ActivityKindRU + " " +
                //    c.KRP + " " +
                //    c.KRPNameKK + " " +
                //    c.KRPNameRU + " " +
                //    c.CATO + " " +
                //    c.LocalityKK + " " +
                //    c.LocalityRU + " " +
                //    c.LegalAddress + " " +
                //    c.HeadName).Contains(search.ToLower()));

                //string query = "SELECT * FROM \"CompanyKK\" WHERE \"BIN\" || ' ' || \"NameKK\" || ' ' || \"OKED\" || ' ' || \"ActivityKindKK\" || ' ' || \"ActivityKindRU\" || ' ' || \"KRP\" || ' ' || \"KRPNameKK\" || ' ' || \"KRPNameRU\" || ' ' || \"CATO\" || ' ' || \"LocalityKK\" || ' ' || \"LocalityRU\" || ' ' || \"LegalAddress\" || ' ' || \"HeadName\" ILIKE '%илькама%';' ;
                //companiesKK = _context.CompanyKK.AsQueryable(query);

                //companiesKK = companiesKK.Where(c => EF.Functions.ILike((c.BIN + " " +
                //    c.NameKK + " " +
                //    c.NameRU + " " +
                //    c.OKED + " " +
                //    c.ActivityKindKK + " " +
                //    c.ActivityKindRU + " " +
                //    c.KRP + " " +
                //    c.KRPNameKK + " " +
                //    c.KRPNameRU + " " +
                //    c.CATO + " " +
                //    c.LocalityKK + " " +
                //    c.LocalityRU + " " +
                //    c.LegalAddress + " " +
                //    c.HeadName), $"%{Search}%"));

                //string search = Search.ToLower();
                //companiesKK = companiesKK.Where(c => (c.BIN + " " +
                //    c.NameKK + " " +
                //    c.NameRU + " " +
                //    c.OKED + " " +
                //    c.ActivityKindKK + " " +
                //    c.ActivityKindRU + " " +
                //    c.KRP + " " +
                //    c.KRPNameKK + " " +
                //    c.KRPNameRU + " " +
                //    c.CATO + " " +
                //    c.LocalityKK + " " +
                //    c.LocalityRU + " " +
                //    c.LegalAddress + " " +
                //    c.HeadName).Contains($"%{Search}%"));

                string query = $"SELECT * FROM \"CompanyKK\" WHERE \"BIN\" || ' ' || \"NameKK\" || ' ' || \"OKED\" || ' ' || \"ActivityKindKK\" || ' ' || \"ActivityKindRU\" || ' ' || \"KRP\" || ' ' || \"KRPNameKK\" || ' ' || \"KRPNameRU\" || ' ' || \"CATO\" || ' ' || \"LocalityKK\" || ' ' || \"LocalityRU\" || ' ' || \"LegalAddress\" || ' ' || \"HeadName\" ILIKE '%{Search}%';";
                companiesKK = _context.CompanyKK.FromSql(query).ToList().AsQueryable();
            }
            if (!string.IsNullOrEmpty(BIN))
            {
                companiesKK = companiesKK.Where(c => c.BIN.ToLower().Contains(BIN.ToLower()));
            }
            if (!string.IsNullOrEmpty(NameKK))
            {
                companiesKK = companiesKK.Where(c => c.NameKK.ToLower().Contains(NameKK.ToLower()));
            }
            if (!string.IsNullOrEmpty(NameRU))
            {
                companiesKK = companiesKK.Where(c => c.NameRU.ToLower().Contains(NameRU.ToLower()));
            }
            if (DateRegister != null)
            {
                companiesKK = companiesKK.Where(c => c.DateRegister == DateRegister);
            }
            if (!string.IsNullOrEmpty(OKED))
            {
                companiesKK = companiesKK.Where(c => c.OKED.ToLower().Contains(OKED.ToLower()));
            }
            if (!string.IsNullOrEmpty(ActivityKindKK))
            {
                companiesKK = companiesKK.Where(c => c.ActivityKindKK.ToLower().Contains(ActivityKindKK.ToLower()));
            }
            if (!string.IsNullOrEmpty(ActivityKindRU))
            {
                companiesKK = companiesKK.Where(c => c.ActivityKindRU.ToLower().Contains(ActivityKindRU.ToLower()));
            }
            if (!string.IsNullOrEmpty(OKEDSecondary))
            {
                companiesKK = companiesKK.Where(c => c.OKEDSecondary.ToLower().Contains(OKEDSecondary.ToLower()));
            }
            if (!string.IsNullOrEmpty(KRP))
            {
                companiesKK = companiesKK.Where(c => c.KRP.ToLower().Contains(KRP.ToLower()));
            }
            if (!string.IsNullOrEmpty(KRPNameKK))
            {
                companiesKK = companiesKK.Where(c => c.KRPNameKK.ToLower().Contains(KRPNameKK.ToLower()));
            }
            if (!string.IsNullOrEmpty(KRPNameRU))
            {
                companiesKK = companiesKK.Where(c => c.KRPNameRU.ToLower().Contains(KRPNameRU.ToLower()));
            }
            if (!string.IsNullOrEmpty(CATO))
            {
                companiesKK = companiesKK.Where(c => c.CATO.ToLower().Contains(CATO.ToLower()));
            }
            if (!string.IsNullOrEmpty(LocalityKK))
            {
                companiesKK = companiesKK.Where(c => c.LocalityKK.ToLower().Contains(LocalityKK.ToLower()));
            }
            if (!string.IsNullOrEmpty(LocalityRU))
            {
                companiesKK = companiesKK.Where(c => c.LocalityRU.ToLower().Contains(LocalityRU.ToLower()));
            }
            if (!string.IsNullOrEmpty(LegalAddress))
            {
                companiesKK = companiesKK.Where(c => c.LegalAddress.ToLower().Contains(LegalAddress.ToLower()));
            }
            if (!string.IsNullOrEmpty(HeadName))
            {
                companiesKK = companiesKK.Where(c => c.HeadName.ToLower().Contains(HeadName.ToLower()));
            }
            int count = companiesKK.Count();
            return Ok(count);
        }

        // GET: api/CompaniesKK/Parse
        [HttpGet("parse")]
        public async Task<IActionResult> ParseCompaniesKK(string Url)
        {
            try
            {
                if (string.IsNullOrEmpty(Url))
                {
                    Url = Startup.Configuration["eGovCompaniesDownloadUrl"];
                }

                //скачивание страницы в виде HTML
                HtmlDocument html = new HtmlDocument();
                using (var client = new WebClient())
                {
                    using (Stream stream = client.OpenRead(new Uri(Url)))
                    {
                        var reader = new StreamReader(stream, Encoding.UTF8);
                        var html_ = reader.ReadToEnd();
                        html.LoadHtml(html_);
                    }
                }

                // получение ссылок на zip-файлы со скачанной страницы
                HtmlNode root = html.DocumentNode;
                List<string> fileUrls = new List<string>();
                foreach (HtmlNode node in root.Descendants())
                {
                    if (node.Name == "a")
                    {
                        if (node.GetAttributeValue("href", "").Trim().Contains("getImg?id=ESTAT"))
                        {
                            fileUrls.Add($"{(new Uri(Url).Scheme)}://{(new Uri(Url).Host)}{node.GetAttributeValue("href", "").Trim()}");
                        }
                    }
                }

                //fileUrls.Add("http://stat.gov.kz/getImg?id=ESTAT086117");
                //fileUrls.Add("http://stat.gov.kz/getImg?id=ESTAT086118");
                //fileUrls.Add("http://stat.gov.kz/getImg?id=ESTAT086119");
                //fileUrls.Add("http://stat.gov.kz/getImg?id=ESTAT086120");
                //fileUrls.Add("http://stat.gov.kz/getImg?id=ESTAT086121");
                //fileUrls.Add("http://stat.gov.kz/getImg?id=ESTAT086123");
                //fileUrls.Add("http://stat.gov.kz/getImg?id=ESTAT086124");
                //fileUrls.Add("http://stat.gov.kz/getImg?id=ESTAT086125");
                //fileUrls.Add("http://stat.gov.kz/getImg?id=ESTAT086126");
                //fileUrls.Add("http://stat.gov.kz/getImg?id=ESTAT086127");
                //fileUrls.Add("http://stat.gov.kz/getImg?id=ESTAT086128");
                //fileUrls.Add("http://stat.gov.kz/getImg?id=ESTAT086129");
                //fileUrls.Add("http://stat.gov.kz/getImg?id=ESTAT086131");
                //fileUrls.Add("http://stat.gov.kz/getImg?id=ESTAT086132");
                //fileUrls.Add("http://stat.gov.kz/getImg?id=ESTAT086133");
                //fileUrls.Add("http://stat.gov.kz/getImg?id=ESTAT086134");

                // скачивание zip-файлов
                int fileCount = 0;
                string sContentRootPath = _hostingEnvironment.WebRootPath;
                sContentRootPath = Path.Combine(sContentRootPath, "Downloads");
                List<string> zipFiles = new List<string>();
                foreach (string url in fileUrls)
                {
                    using (var client = new WebClient())
                    {
                        fileCount++;
                        string fileName = Path.ChangeExtension(fileCount.ToString(), ".zip");
                        var filePath = Path.Combine(sContentRootPath, Path.GetFileName(fileName));
                        zipFiles.Add(filePath);
                    client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.33 Safari/537.36");
                    client.DownloadFile(url, filePath);
                }
            }

                // разархивирование zip-файлов
                List<string> xlsFiles = new List<string>();
                foreach (string zipFile in zipFiles)
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    using (ZipFile zip = ZipFile.Read(zipFile))
                    {
                        foreach (ZipEntry filefromzip in zip)
                        {
                            filefromzip.Extract(sContentRootPath, ExtractExistingFileAction.OverwriteSilently);
                            xlsFiles.Add(Path.Combine(sContentRootPath, filefromzip.FileName));
                        }
                    }
                }
                //xlsFiles.Add(@"D:\Documents\Google Drive\repos\Clever\CleverAPI\wwwroot\Downloads\11.xls");
                //xlsFiles.Add(@"D:\Documents\Google Drive\repos\Clever\CleverAPI\wwwroot\Downloads\15.xls");
                //xlsFiles.Add(@"D:\Documents\Google Drive\repos\Clever\CleverAPI\wwwroot\Downloads\19.xls");
                //xlsFiles.Add(@"D:\Documents\Google Drive\repos\Clever\CleverAPI\wwwroot\Downloads\23.xls");
                //xlsFiles.Add(@"D:\Documents\Google Drive\repos\Clever\CleverAPI\wwwroot\Downloads\27.xls");
                //xlsFiles.Add(@"D:\Documents\Google Drive\repos\Clever\CleverAPI\wwwroot\Downloads\31.xls");
                //xlsFiles.Add(@"D:\Documents\Google Drive\repos\Clever\CleverAPI\wwwroot\Downloads\35.xls");
                //xlsFiles.Add(@"D:\Documents\Google Drive\repos\Clever\CleverAPI\wwwroot\Downloads\39.xls");
                //xlsFiles.Add(@"D:\Documents\Google Drive\repos\Clever\CleverAPI\wwwroot\Downloads\43.xls");
                //xlsFiles.Add(@"D:\Documents\Google Drive\repos\Clever\CleverAPI\wwwroot\Downloads\47.xls");
                //xlsFiles.Add(@"D:\Documents\Google Drive\repos\Clever\CleverAPI\wwwroot\Downloads\51.xls");
                //xlsFiles.Add(@"D:\Documents\Google Drive\repos\Clever\CleverAPI\wwwroot\Downloads\55.xls");
                //xlsFiles.Add(@"D:\Documents\Google Drive\repos\Clever\CleverAPI\wwwroot\Downloads\59.xls");
                //xlsFiles.Add(@"D:\Documents\Google Drive\repos\Clever\CleverAPI\wwwroot\Downloads\63.xls");
                //xlsFiles.Add(@"D:\Documents\Google Drive\repos\Clever\CleverAPI\wwwroot\Downloads\71.xls");
                //xlsFiles.Add(@"D:\Documents\Google Drive\repos\Clever\CleverAPI\wwwroot\Downloads\75.xls");

                // получение данных с Excel-файлов
                List<CompanyKK> companiesKKNew = new List<CompanyKK>();
                foreach (string xlsFile in xlsFiles)
                {
                    var file = new FileInfo(xlsFile);
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    using (var stream = System.IO.File.Open(xlsFile, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            do
                            {
                                while (reader.Read())
                                {
                                    
                                }
                            } while (reader.NextResult());
                            var result = reader.AsDataSet();
                            for (var i = 4; i < result.Tables[0].Rows.Count; i++)
                            {
                                string[] dateRegister = result.Tables[0].Rows[i][3].ToString().Split('.');
                                companiesKKNew.Add(new CompanyKK()
                                {
                                    BIN = result.Tables[0].Rows[i][0].ToString(),
                                    NameKK = result.Tables[0].Rows[i][1].ToString(),
                                    NameRU = result.Tables[0].Rows[i][2].ToString(),
                                    DateRegister = !string.IsNullOrEmpty(result.Tables[0].Rows[i][3].ToString()) ? (DateTime?)(new DateTime(Convert.ToInt32(dateRegister[2]), Convert.ToInt32(dateRegister[1]), Convert.ToInt32(dateRegister[0]))) : null,
                                    OKED = result.Tables[0].Rows[i][4].ToString(),
                                    ActivityKindKK = result.Tables[0].Rows[i][5].ToString(),
                                    ActivityKindRU = result.Tables[0].Rows[i][6].ToString(),
                                    OKEDSecondary = result.Tables[0].Rows[i][7].ToString(),
                                    KRP = result.Tables[0].Rows[i][8].ToString(),
                                    KRPNameKK = result.Tables[0].Rows[i][9].ToString(),
                                    KRPNameRU = result.Tables[0].Rows[i][10].ToString(),
                                    CATO = result.Tables[0].Rows[i][11].ToString(),
                                    LocalityKK = result.Tables[0].Rows[i][12].ToString(),
                                    LocalityRU = result.Tables[0].Rows[i][13].ToString(),
                                    LegalAddress = result.Tables[0].Rows[i][14].ToString(),
                                    HeadName = result.Tables[0].Rows[i][15].ToString()
                                });
                            }
                        }
                    }
                }

                int importedCount = 0,
                    deletedCount = 0;
                foreach (CompanyKK companyKK in _context.CompanyKK.Where(c => c.ExpiredDateTime == null))
                {
                    if(companiesKKNew.FirstOrDefault(c => c.NameRU == companyKK.NameRU) == null)
                    {
                        companyKK.ExpiredDateTime = null;
                        _context.Entry(companyKK).State = EntityState.Modified;
                        deletedCount++;
                    }
                }
                foreach(CompanyKK companyKK in companiesKKNew)
                {
                    if(_context.CompanyKK.FirstOrDefault(c => c.NameRU == companyKK.NameRU) == null)
                    {
                        _context.CompanyKK.Add(companyKK);
                        importedCount++;
                    }
                }
                _context.SaveChanges();

                //385 830 were
                //386 603 new

                return Ok($"Imported: {importedCount.ToString()}, deleted: {deletedCount.ToString()}");
            }
            catch (Exception exp)
            {
                return Json(exp.Message);
            }
        }

        // POST api/CompaniesKK/Upload
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
                    int start_row = 5;
                    //bool FirstRowHeader = true;
                    //if (FirstRowHeader)
                    //{
                    //    start_row++;
                    //}
                    List<CompanyKK> companiesKK = new List<CompanyKK>();
                    for (int i = start_row; ; i++)
                    {
                        if (package.Workbook.Worksheets.FirstOrDefault().Cells[i, 1].Value == null)
                        {
                            break;
                        }
                        string BIN = null,
                            NameKK = null,
                            NameRU = null,
                            OKED = null,
                            ActivityKindKK = null,
                            ActivityKindRU = null,
                            OKEDSecondary = null,
                            KRP = null,
                            KRPNameKK = null,
                            KRPNameRU = null,
                            CATO = null,
                            LocalityKK = null,
                            LocalityRU = null,
                            LegalAddress = null,
                            HeadName = null;
                        DateTime? DateRegister = null;

                        try
                        {
                            BIN = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 1].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 1: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            NameKK = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 2].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 2: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            NameRU = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 3].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 3: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            string DateRegisterStr = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 4].Value);
                            if (!string.IsNullOrEmpty(DateRegisterStr))
                            {
                                string[] dmy = DateRegisterStr.Split('.');
                                if (dmy.Length == 3)
                                {
                                    DateRegister = new DateTime(Convert.ToInt32(dmy[2]),
                                        Convert.ToInt32(dmy[1]),
                                        Convert.ToInt32(dmy[0]));
                                }
                            }
                            //DateRegister = package.Workbook.Worksheets.FirstOrDefault().Cells[i, 4].Value == null ? null : (DateTime?)Convert.ToDateTime(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 4].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 4: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            OKED = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 5].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 5: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            ActivityKindKK = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 6].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 6: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            ActivityKindRU = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 7].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 7: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            OKEDSecondary = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 8].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 8: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            KRP = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 9].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 9: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            KRPNameKK = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 10].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 10: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            KRPNameRU = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 11].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 11: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            CATO = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 12].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 12: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            LocalityKK = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 13].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 13: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            LocalityRU = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 14].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 14: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            LegalAddress = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 15].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 15: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        try
                        {
                            HeadName = Convert.ToString(package.Workbook.Worksheets.FirstOrDefault().Cells[i, 16].Value);
                        }
                        catch (Exception e)
                        {
                            return Json($"Row {i.ToString()}, Column 16: " + e.Message + (e.InnerException == null ? "" : ": " + e.InnerException.Message));
                        }

                        companiesKK.Add(new CompanyKK()
                        {
                            BIN = BIN,
                            NameKK = NameKK,
                            NameRU = NameRU,
                            DateRegister = DateRegister,
                            OKED = OKED,
                            ActivityKindKK = ActivityKindKK,
                            ActivityKindRU = ActivityKindRU,
                            OKEDSecondary = OKEDSecondary,
                            KRP = KRP,
                            KRPNameKK = KRPNameKK,
                            KRPNameRU = KRPNameRU,
                            CATO = CATO,
                            LocalityKK = LocalityKK,
                            LocalityRU = LocalityRU,
                            LegalAddress = LegalAddress,
                            HeadName = HeadName
                        });
                    }
                    _context.AddRange(companiesKK);
                    countUploaded += companiesKK.Count();
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