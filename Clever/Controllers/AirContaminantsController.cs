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
    public class AirContaminantsController : Controller
    {
        private readonly HttpApiClientController _HttpApiClient;

        public AirContaminantsController(HttpApiClientController HttpApiClient)
        {
            _HttpApiClient = HttpApiClient;
        }

        public async Task<IActionResult> Index(string SortOrder, string Name, string NumberCAS, int? HazardClass, int? PageSize, int? Page)
        {
            List<AirContaminant> airContaminants = new List<AirContaminant>();

            ViewBag.NameFilter = Name;
            ViewBag.NumberCASFilter = NumberCAS;
            ViewBag.HazardClassFilter = HazardClass;

            ViewBag.NameSort = SortOrder == "Name" ? "NameDesc" : "Name";
            ViewBag.NumberCASSort = SortOrder == "NumberCAS" ? "NumberCASDesc" : "NumberCAS";
            ViewBag.HazardClassSort = SortOrder == "HazardClass" ? "HazardClassDesc" : "HazardClass";

            string url = "api/AirContaminants",
                route = "",
                routeCount = "";
            if (!string.IsNullOrEmpty(SortOrder))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"SortOrder={SortOrder}";
            }
            if (!string.IsNullOrEmpty(Name))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Name={Name}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"Name={Name}";
            }
            if (!string.IsNullOrEmpty(NumberCAS))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"NumberCAS={NumberCAS}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"NumberCAS={NumberCAS}";
            }
            if (HazardClass!=null)
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"HazardClass={HazardClass.ToString()}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"HazardClass={HazardClass.ToString()}";
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
                airContaminants = await response.Content.ReadAsAsync<List<AirContaminant>>();
            }
            int airContaminantsCount = 0;
            if (responseCount.IsSuccessStatusCode)
            {
                airContaminantsCount = await responseCount.Content.ReadAsAsync<int>();
            }
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page != null ? (int)Page : 1;
            ViewBag.TotalPages = PageSize != null ? (int)(Math.Ceiling((decimal)airContaminantsCount / (decimal)PageSize)) : 1;
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
            IConfigurationSection pageSizeListSection = Startup.Configuration.GetSection("PageSizeList");
            var pageSizeList = pageSizeListSection.AsEnumerable();
            ViewBag.PageSizeList = new SelectList(pageSizeList.OrderBy(p => p.Key), "Value", "Value");

            return View(airContaminants);
        }

        public async Task<IActionResult> Details(int? id)
        {
            AirContaminant airContaminant = null;
            HttpResponseMessage response = await _HttpApiClient.GetAsync($"api/AirContaminants/{id.ToString()}");
            if (response.IsSuccessStatusCode)
            {
                airContaminant = await response.Content.ReadAsAsync<AirContaminant>();
            }
            return View(airContaminant);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,NumberCAS,HazardClass,MaximumPermissibleConcentrationOneTimeMaximum,MaximumPermissibleConcentrationDailyAverage,ApproximateSafeExposureLevel")] AirContaminant airContaminant)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _HttpApiClient.PostAsJsonAsync(
                    "api/AirContaminants", airContaminant);

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
                    return View(airContaminant);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(airContaminant);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            AirContaminant airContaminant = null;
            HttpResponseMessage response = await _HttpApiClient.GetAsync($"api/AirContaminants/{id.ToString()}");
            if (response.IsSuccessStatusCode)
            {
                airContaminant = await response.Content.ReadAsAsync<AirContaminant>();
            }
            return View(airContaminant);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NumberCAS,HazardClass,MaximumPermissibleConcentrationOneTimeMaximum,MaximumPermissibleConcentrationDailyAverage,ApproximateSafeExposureLevel")] AirContaminant airContaminant)
        {
            if (id != airContaminant.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _HttpApiClient.PutAsJsonAsync(
                    $"api/AirContaminants/{airContaminant.Id}", airContaminant);

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
                    return View(airContaminant);
                }

                airContaminant = await response.Content.ReadAsAsync<AirContaminant>();
                return RedirectToAction(nameof(Index));
            }
            return View(airContaminant);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AirContaminant airContaminant = null;
            HttpResponseMessage response = await _HttpApiClient.GetAsync($"api/AirContaminants/{id.ToString()}");
            if (response.IsSuccessStatusCode)
            {
                airContaminant = await response.Content.ReadAsAsync<AirContaminant>();
            }
            if (airContaminant == null)
            {
                return NotFound();
            }
            return View(airContaminant);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _HttpApiClient.DeleteAsync(
                $"api/AirContaminants/{id}");
            return RedirectToAction(nameof(Index));
        }

        [DisableRequestSizeLimit]
        public IActionResult UploadFiles()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<string> UploadFiles(List<IFormFile> Files)
        {
            string report = "";
            foreach (IFormFile file in Files)
            {
                byte[] data;
                using (var br = new BinaryReader(file.OpenReadStream()))
                    data = br.ReadBytes((int)file.OpenReadStream().Length);
                ByteArrayContent bytes = new ByteArrayContent(data);
                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                multiContent.Add(bytes, "file", file.FileName);
                var result = await _HttpApiClient.PostAsync("api/AirContaminants/upload", multiContent).Result.Content.ReadAsStringAsync();
                report += $"\r\n{file.FileName}: {result}";
            }
            return report;
        }
    }
}