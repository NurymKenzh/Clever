using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Clever.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Clever.Controllers
{
    public class AirContaminantsKKController : Controller
    {
        private readonly HttpApiClientController _HttpApiClient;

        public AirContaminantsKKController(HttpApiClientController HttpApiClient)
        {
            _HttpApiClient = HttpApiClient;
        }

        public async Task<IActionResult> Index(string SortOrder,
            string NameFilter,
            string NumberCASFilter,
            string FormulaFilter,
            decimal? MaximumPermissibleConcentrationOneTimeMaximumFilter,
            decimal? MaximumPermissibleConcentrationDailyAverageFilter,
            int? HazardClassFilter,
            int? CodeFilter,
            decimal? ApproximateSafeExposureLevelFilter,
            int? PageSize,
            int? Page)
        {
            List<AirContaminantKK> airContaminantsKK = new List<AirContaminantKK>();

            ViewBag.NameFilter = NameFilter;
            ViewBag.NumberCASFilter = NumberCASFilter;
            ViewBag.FormulaFilter = FormulaFilter;
            ViewBag.MaximumPermissibleConcentrationOneTimeMaximumFilter = MaximumPermissibleConcentrationOneTimeMaximumFilter;
            ViewBag.MaximumPermissibleConcentrationDailyAverageFilter = MaximumPermissibleConcentrationDailyAverageFilter;
            ViewBag.HazardClassFilter = HazardClassFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.ApproximateSafeExposureLevelFilter = ApproximateSafeExposureLevelFilter;

            ViewBag.NameSort = SortOrder == "Name" ? "NameDesc" : "Name";
            ViewBag.NumberCASSort = SortOrder == "NumberCAS" ? "NumberCASDesc" : "NumberCAS";
            ViewBag.FormulaSort = SortOrder == "Formula" ? "FormulaDesc" : "Formula";
            ViewBag.MaximumPermissibleConcentrationOneTimeMaximumSort = SortOrder == "MaximumPermissibleConcentrationOneTimeMaximum" ? "MaximumPermissibleConcentrationOneTimeMaximumDesc" : "MaximumPermissibleConcentrationOneTimeMaximum";
            ViewBag.MaximumPermissibleConcentrationDailyAverageSort = SortOrder == "MaximumPermissibleConcentrationDailyAverage" ? "MaximumPermissibleConcentrationDailyAverageDesc" : "MaximumPermissibleConcentrationDailyAverage";
            ViewBag.HazardClassSort = SortOrder == "HazardClass" ? "HazardClassDesc" : "HazardClass";
            ViewBag.CodeSort = SortOrder == "Code" ? "CodeDesc" : "Code";
            ViewBag.ApproximateSafeExposureLevelSort = SortOrder == "ApproximateSafeExposureLevel" ? "ApproximateSafeExposureLevelDesc" : "ApproximateSafeExposureLevel";

            string url = "api/AirContaminantsKK",
                route = "",
                routeCount = "";
            if (!string.IsNullOrEmpty(SortOrder))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"SortOrder={SortOrder}";
            }
            if (!string.IsNullOrEmpty(NameFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Name={NameFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"Name={NameFilter}";
            }
            if (!string.IsNullOrEmpty(NumberCASFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"NumberCAS={NumberCASFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"NumberCAS={NumberCASFilter}";
            }
            if (!string.IsNullOrEmpty(FormulaFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Formula={FormulaFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"Formula={FormulaFilter}";
            }
            if (MaximumPermissibleConcentrationOneTimeMaximumFilter != null)
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"MaximumPermissibleConcentrationOneTimeMaximum={MaximumPermissibleConcentrationOneTimeMaximumFilter.ToString()}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"MaximumPermissibleConcentrationOneTimeMaximum={MaximumPermissibleConcentrationOneTimeMaximumFilter.ToString()}";
            }
            if (MaximumPermissibleConcentrationDailyAverageFilter != null)
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"MaximumPermissibleConcentrationDailyAverage={MaximumPermissibleConcentrationDailyAverageFilter.ToString()}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"MaximumPermissibleConcentrationDailyAverage={MaximumPermissibleConcentrationDailyAverageFilter.ToString()}";
            }
            if (HazardClassFilter != null)
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"HazardClass={HazardClassFilter.ToString()}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"HazardClass={HazardClassFilter.ToString()}";
            }
            if (CodeFilter != null)
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Code={CodeFilter.ToString()}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"Code={CodeFilter.ToString()}";
            }
            if (ApproximateSafeExposureLevelFilter != null)
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"ApproximateSafeExposureLevel={ApproximateSafeExposureLevelFilter.ToString()}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"ApproximateSafeExposureLevel={ApproximateSafeExposureLevelFilter.ToString()}";
            }
            IConfigurationSection pageSizeListSection = Startup.Configuration.GetSection("PageSizeList");
            var pageSizeList = pageSizeListSection.AsEnumerable();
            ViewBag.PageSizeList = new SelectList(pageSizeList.OrderBy(p => p.Key)
                .Select(p => 
                {
                    return new KeyValuePair<string, string>(p.Value ?? "0", p.Value);
                }), "Key", "Value");
            if (PageSize == null)
            {
                PageSize = Convert.ToInt32(pageSizeList.Min(p => p.Value));
            }
            if(PageSize == 0)
            {
                PageSize = null;
            }
            if (PageSize != null)
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"PageSize={PageSize.ToString()}";
                if (Page == null)
                {
                    Page = 1;
                }
            }
            if (Page != null)
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Page={Page.ToString()}";
            }
            HttpResponseMessage response = await _HttpApiClient.GetAsync(url + route),
                responseCount = await _HttpApiClient.GetAsync(url + "/count" + routeCount);
            if (response.IsSuccessStatusCode)
            {
                airContaminantsKK = await response.Content.ReadAsAsync<List<AirContaminantKK>>();
            }
            int airContaminantsKKCount = 0;
            if (responseCount.IsSuccessStatusCode)
            {
                airContaminantsKKCount = await responseCount.Content.ReadAsAsync<int>();
            }
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page != null ? (int)Page : 1;
            ViewBag.TotalPages = PageSize != null ? (int)(Math.Ceiling((decimal)airContaminantsKKCount / (decimal)PageSize)) : 1;
            ViewBag.StartPage = Page - 5;
            ViewBag.EndPage = Page + 4;
            if (ViewBag.StartPage <= 0)
            {
                ViewBag.EndPage -= (ViewBag.StartPage - 1);
                ViewBag.StartPage = 1;
            }
            if (ViewBag.EndPage > ViewBag.TotalPages)
            {
                ViewBag.EndPage = ViewBag.TotalPages;
                if (ViewBag.EndPage > 10)
                {
                    ViewBag.StartPage = ViewBag.EndPage - 9;
                }
            }

            return View(airContaminantsKK);
        }

        public async Task<IActionResult> Details(int? id,
            string SortOrder,
            string NameFilter,
            string NumberCASFilter,
            string FormulaFilter,
            decimal? MaximumPermissibleConcentrationOneTimeMaximumFilter,
            decimal? MaximumPermissibleConcentrationDailyAverageFilter,
            int? HazardClassFilter,
            int? CodeFilter,
            decimal? ApproximateSafeExposureLevelFilter,
            int? PageSize,
            int? Page)
        {
            AirContaminantKK airContaminantKK = null;
            HttpResponseMessage response = await _HttpApiClient.GetAsync($"api/AirContaminantsKK/{id.ToString()}");
            if (response.IsSuccessStatusCode)
            {
                airContaminantKK = await response.Content.ReadAsAsync<AirContaminantKK>();
            }
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.NameFilter = NameFilter;
            ViewBag.NumberCASFilter = NumberCASFilter;
            ViewBag.FormulaFilter = FormulaFilter;
            ViewBag.MaximumPermissibleConcentrationOneTimeMaximumFilter = MaximumPermissibleConcentrationOneTimeMaximumFilter;
            ViewBag.MaximumPermissibleConcentrationDailyAverageFilter = MaximumPermissibleConcentrationDailyAverageFilter;
            ViewBag.HazardClassFilter = HazardClassFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.ApproximateSafeExposureLevelFilter = ApproximateSafeExposureLevelFilter;
            return View(airContaminantKK);
        }

        public IActionResult Create(string SortOrder,
            string NameFilter,
            string NumberCASFilter,
            string FormulaFilter,
            decimal? MaximumPermissibleConcentrationOneTimeMaximumFilter,
            decimal? MaximumPermissibleConcentrationDailyAverageFilter,
            int? HazardClassFilter,
            int? CodeFilter,
            decimal? ApproximateSafeExposureLevelFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.NameFilter = NameFilter;
            ViewBag.NumberCASFilter = NumberCASFilter;
            ViewBag.FormulaFilter = FormulaFilter;
            ViewBag.MaximumPermissibleConcentrationOneTimeMaximumFilter = MaximumPermissibleConcentrationOneTimeMaximumFilter;
            ViewBag.MaximumPermissibleConcentrationDailyAverageFilter = MaximumPermissibleConcentrationDailyAverageFilter;
            ViewBag.HazardClassFilter = HazardClassFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.ApproximateSafeExposureLevelFilter = ApproximateSafeExposureLevelFilter;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,NumberCAS,Formula,MaximumPermissibleConcentrationOneTimeMaximum,MaximumPermissibleConcentrationDailyAverage,HazardClass,Code,ApproximateSafeExposureLevel")] AirContaminantKK airContaminantKK,
            string SortOrder,
            string NameFilter,
            string NumberCASFilter,
            string FormulaFilter,
            decimal? MaximumPermissibleConcentrationOneTimeMaximumFilter,
            decimal? MaximumPermissibleConcentrationDailyAverageFilter,
            int? HazardClassFilter,
            int? CodeFilter,
            decimal? ApproximateSafeExposureLevelFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.NameFilter = NameFilter;
            ViewBag.NumberCASFilter = NumberCASFilter;
            ViewBag.FormulaFilter = FormulaFilter;
            ViewBag.MaximumPermissibleConcentrationOneTimeMaximumFilter = MaximumPermissibleConcentrationOneTimeMaximumFilter;
            ViewBag.MaximumPermissibleConcentrationDailyAverageFilter = MaximumPermissibleConcentrationDailyAverageFilter;
            ViewBag.HazardClassFilter = HazardClassFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.ApproximateSafeExposureLevelFilter = ApproximateSafeExposureLevelFilter;
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _HttpApiClient.PostAsJsonAsync(
                    "api/AirContaminantsKK", airContaminantKK);

                string OutputViewText = await response.Content.ReadAsStringAsync();
                OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch
                {
                    dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                    foreach (Newtonsoft.Json.Linq.JProperty property in errors.Children())
                    {
                        ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    }
                    return View(airContaminantKK);
                }

                return RedirectToAction(nameof(Index), new { PageSize = ViewBag.PageSize, Page = ViewBag.Page, SortOrder = ViewBag.SortOrder, NameFilter = ViewBag.NameFilter, NumberCASFilter = ViewBag.NumberCASFilter, FormulaFilter = ViewBag.FormulaFilter, MaximumPermissibleConcentrationOneTimeMaximumFilter = ViewBag.MaximumPermissibleConcentrationOneTimeMaximumFilter, MaximumPermissibleConcentrationDailyAverageFilter = ViewBag.MaximumPermissibleConcentrationDailyAverageFilter, HazardClassFilter = ViewBag.HazardClassFilter, CodeFilter = ViewBag.CodeFilter, ApproximateSafeExposureLevelFilter = ViewBag.ApproximateSafeExposureLevelFilter });
            }
            return View(airContaminantKK);
        }

        public async Task<IActionResult> Edit(int? id,
            string SortOrder,
            string NameFilter,
            string NumberCASFilter,
            string FormulaFilter,
            decimal? MaximumPermissibleConcentrationOneTimeMaximumFilter,
            decimal? MaximumPermissibleConcentrationDailyAverageFilter,
            int? HazardClassFilter,
            int? CodeFilter,
            decimal? ApproximateSafeExposureLevelFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.NameFilter = NameFilter;
            ViewBag.NumberCASFilter = NumberCASFilter;
            ViewBag.FormulaFilter = FormulaFilter;
            ViewBag.MaximumPermissibleConcentrationOneTimeMaximumFilter = MaximumPermissibleConcentrationOneTimeMaximumFilter;
            ViewBag.MaximumPermissibleConcentrationDailyAverageFilter = MaximumPermissibleConcentrationDailyAverageFilter;
            ViewBag.HazardClassFilter = HazardClassFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.ApproximateSafeExposureLevelFilter = ApproximateSafeExposureLevelFilter;
            AirContaminantKK airContaminantKK = null;
            HttpResponseMessage response = await _HttpApiClient.GetAsync($"api/AirContaminantsKK/{id.ToString()}");
            if (response.IsSuccessStatusCode)
            {
                airContaminantKK = await response.Content.ReadAsAsync<AirContaminantKK>();
            }
            return View(airContaminantKK);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NumberCAS,Formula,MaximumPermissibleConcentrationOneTimeMaximum,MaximumPermissibleConcentrationDailyAverage,HazardClass,Code,ApproximateSafeExposureLevel")] AirContaminantKK airContaminantKK,
            string SortOrder,
            string NameFilter,
            string NumberCASFilter,
            string FormulaFilter,
            decimal? MaximumPermissibleConcentrationOneTimeMaximumFilter,
            decimal? MaximumPermissibleConcentrationDailyAverageFilter,
            int? HazardClassFilter,
            int? CodeFilter,
            decimal? ApproximateSafeExposureLevelFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.NameFilter = NameFilter;
            ViewBag.NumberCASFilter = NumberCASFilter;
            ViewBag.FormulaFilter = FormulaFilter;
            ViewBag.MaximumPermissibleConcentrationOneTimeMaximumFilter = MaximumPermissibleConcentrationOneTimeMaximumFilter;
            ViewBag.MaximumPermissibleConcentrationDailyAverageFilter = MaximumPermissibleConcentrationDailyAverageFilter;
            ViewBag.HazardClassFilter = HazardClassFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.ApproximateSafeExposureLevelFilter = ApproximateSafeExposureLevelFilter;
            if (id != airContaminantKK.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _HttpApiClient.PutAsJsonAsync(
                    $"api/AirContaminantsKK/{airContaminantKK.Id}", airContaminantKK);

                string OutputViewText = await response.Content.ReadAsStringAsync();
                OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch
                {
                    dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                    foreach (Newtonsoft.Json.Linq.JProperty property in errors.Children())
                    {
                        ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    }
                    return View(airContaminantKK);
                }

                airContaminantKK = await response.Content.ReadAsAsync<AirContaminantKK>();
                return RedirectToAction(nameof(Index), new { PageSize = ViewBag.PageSize, Page = ViewBag.Page, SortOrder = ViewBag.SortOrder, NameFilter = ViewBag.NameFilter, NumberCASFilter = ViewBag.NumberCASFilter, FormulaFilter = ViewBag.FormulaFilter, MaximumPermissibleConcentrationOneTimeMaximumFilter = ViewBag.MaximumPermissibleConcentrationOneTimeMaximumFilter, MaximumPermissibleConcentrationDailyAverageFilter = ViewBag.MaximumPermissibleConcentrationDailyAverageFilter, HazardClassFilter = ViewBag.HazardClassFilter, CodeFilter = ViewBag.CodeFilter, ApproximateSafeExposureLevelFilter = ViewBag.ApproximateSafeExposureLevelFilter });
            }
            return View(airContaminantKK);
        }

        public async Task<IActionResult> Delete(int? id,
            string SortOrder,
            string NameFilter,
            string NumberCASFilter,
            string FormulaFilter,
            decimal? MaximumPermissibleConcentrationOneTimeMaximumFilter,
            decimal? MaximumPermissibleConcentrationDailyAverageFilter,
            int? HazardClassFilter,
            int? CodeFilter,
            decimal? ApproximateSafeExposureLevelFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.NameFilter = NameFilter;
            ViewBag.NumberCASFilter = NumberCASFilter;
            ViewBag.FormulaFilter = FormulaFilter;
            ViewBag.MaximumPermissibleConcentrationOneTimeMaximumFilter = MaximumPermissibleConcentrationOneTimeMaximumFilter;
            ViewBag.MaximumPermissibleConcentrationDailyAverageFilter = MaximumPermissibleConcentrationDailyAverageFilter;
            ViewBag.HazardClassFilter = HazardClassFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.ApproximateSafeExposureLevelFilter = ApproximateSafeExposureLevelFilter;
            if (id == null)
            {
                return NotFound();
            }

            AirContaminantKK airContaminantKK = null;
            HttpResponseMessage response = await _HttpApiClient.GetAsync($"api/AirContaminantsKK/{id.ToString()}");
            if (response.IsSuccessStatusCode)
            {
                airContaminantKK = await response.Content.ReadAsAsync<AirContaminantKK>();
            }
            if (airContaminantKK == null)
            {
                return NotFound();
            }
            return View(airContaminantKK);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id,
            string SortOrder,
            string NameFilter,
            string NumberCASFilter,
            string FormulaFilter,
            decimal? MaximumPermissibleConcentrationOneTimeMaximumFilter,
            decimal? MaximumPermissibleConcentrationDailyAverageFilter,
            int? HazardClassFilter,
            int? CodeFilter,
            decimal? ApproximateSafeExposureLevelFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.NameFilter = NameFilter;
            ViewBag.NumberCASFilter = NumberCASFilter;
            ViewBag.FormulaFilter = FormulaFilter;
            ViewBag.MaximumPermissibleConcentrationOneTimeMaximumFilter = MaximumPermissibleConcentrationOneTimeMaximumFilter;
            ViewBag.MaximumPermissibleConcentrationDailyAverageFilter = MaximumPermissibleConcentrationDailyAverageFilter;
            ViewBag.HazardClassFilter = HazardClassFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.ApproximateSafeExposureLevelFilter = ApproximateSafeExposureLevelFilter;
            HttpResponseMessage response = await _HttpApiClient.DeleteAsync(
                $"api/AirContaminantsKK/{id}");
            return RedirectToAction(nameof(Index), new { PageSize = ViewBag.PageSize, Page = ViewBag.Page, SortOrder = ViewBag.SortOrder, NameFilter = ViewBag.NameFilter, NumberCASFilter = ViewBag.NumberCASFilter, FormulaFilter = ViewBag.FormulaFilter, MaximumPermissibleConcentrationOneTimeMaximumFilter = ViewBag.MaximumPermissibleConcentrationOneTimeMaximumFilter, MaximumPermissibleConcentrationDailyAverageFilter = ViewBag.MaximumPermissibleConcentrationDailyAverageFilter, HazardClassFilter = ViewBag.HazardClassFilter, CodeFilter = ViewBag.CodeFilter, ApproximateSafeExposureLevelFilter = ViewBag.ApproximateSafeExposureLevelFilter });
        }

        [DisableRequestSizeLimit]
        public IActionResult UploadFiles(string SortOrder,
            string NameFilter,
            string NumberCASFilter,
            string FormulaFilter,
            decimal? MaximumPermissibleConcentrationOneTimeMaximumFilter,
            decimal? MaximumPermissibleConcentrationDailyAverageFilter,
            int? HazardClassFilter,
            int? CodeFilter,
            decimal? ApproximateSafeExposureLevelFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.NameFilter = NameFilter;
            ViewBag.NumberCASFilter = NumberCASFilter;
            ViewBag.FormulaFilter = FormulaFilter;
            ViewBag.MaximumPermissibleConcentrationOneTimeMaximumFilter = MaximumPermissibleConcentrationOneTimeMaximumFilter;
            ViewBag.MaximumPermissibleConcentrationDailyAverageFilter = MaximumPermissibleConcentrationDailyAverageFilter;
            ViewBag.HazardClassFilter = HazardClassFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.ApproximateSafeExposureLevelFilter = ApproximateSafeExposureLevelFilter;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<string> UploadFiles(List<IFormFile> Files,
            string SortOrder,
            string NameFilter,
            string NumberCASFilter,
            string FormulaFilter,
            decimal? MaximumPermissibleConcentrationOneTimeMaximumFilter,
            decimal? MaximumPermissibleConcentrationDailyAverageFilter,
            int? HazardClassFilter,
            int? CodeFilter,
            decimal? ApproximateSafeExposureLevelFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.NameFilter = NameFilter;
            ViewBag.NumberCASFilter = NumberCASFilter;
            ViewBag.FormulaFilter = FormulaFilter;
            ViewBag.MaximumPermissibleConcentrationOneTimeMaximumFilter = MaximumPermissibleConcentrationOneTimeMaximumFilter;
            ViewBag.MaximumPermissibleConcentrationDailyAverageFilter = MaximumPermissibleConcentrationDailyAverageFilter;
            ViewBag.HazardClassFilter = HazardClassFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.ApproximateSafeExposureLevelFilter = ApproximateSafeExposureLevelFilter;
            string report = "";
            foreach (IFormFile file in Files)
            {
                byte[] data;
                using (var br = new BinaryReader(file.OpenReadStream()))
                    data = br.ReadBytes((int)file.OpenReadStream().Length);
                ByteArrayContent bytes = new ByteArrayContent(data);
                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                multiContent.Add(bytes, "file", file.FileName);
                var result = await _HttpApiClient.PostAsync("api/AirContaminantsKK/upload", multiContent).Result.Content.ReadAsStringAsync();
                report += $"\r\n{file.FileName}: {result}";
            }
            return report;
        }
    }
}