using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Clever.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Clever.Controllers
{
    public class CATOController : Controller
    {
        private readonly HttpApiClientController _HttpApiClient;

        public CATOController(HttpApiClientController HttpApiClient)
        {
            _HttpApiClient = HttpApiClient;
        }

        public async Task<IActionResult> Index(string SortOrder,
            int? EgovIdFilter,
            string CodeFilter,
            string NameKKFilter,
            string NameRUFilter,
            int? ParentFilter,
            int? AreaTypeFilter,
            int? PageSize,
            int? Page)
        {
            List<CATO> catoes = new List<CATO>();

            ViewBag.EgovIdFilter = EgovIdFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.NameRUFilter = NameRUFilter;
            ViewBag.ParentFilter = ParentFilter;
            ViewBag.AreaTypeFilter = AreaTypeFilter;

            ViewBag.EgovIdSort = SortOrder == "EgovId" ? "EgovIdDesc" : "EgovId";
            ViewBag.CodeSort = SortOrder == "Code" ? "CodeDesc" : "Code";
            ViewBag.NameKKSort = SortOrder == "NameKK" ? "NameKKDesc" : "NameKK";
            ViewBag.NameRUSort = SortOrder == "NameRU" ? "NameRUDesc" : "NameRU";
            ViewBag.NameENSort = SortOrder == "NameEN" ? "NameENDesc" : "NameEN";
            ViewBag.ParentSort = SortOrder == "Parent" ? "ParentDesc" : "Parent";
            ViewBag.AreaTypeSort = SortOrder == "AreaType" ? "AreaTypeDesc" : "AreaType";

            string url = "api/CATO",
                route = "",
                routeCount = "";
            if(!string.IsNullOrEmpty(SortOrder))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"SortOrder={SortOrder}";
            }
            if (EgovIdFilter!=null)
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"EgovIdFilter={EgovIdFilter.ToString()}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"EgovIdFilter={EgovIdFilter.ToString()}";
            }
            if (!string.IsNullOrEmpty(CodeFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"CodeFilter={CodeFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"CodeFilter={CodeFilter}";
            }
            if (!string.IsNullOrEmpty(NameKKFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"NameKKFilter={NameKKFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"NameKKFilter={NameKKFilter}";
            }
            if (!string.IsNullOrEmpty(NameRUFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"NameRUFilter={NameRUFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"NameRUFilter={NameRUFilter}";
            }
            if (ParentFilter != null)
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"ParentFilter={ParentFilter.ToString()}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"ParentFilter={ParentFilter.ToString()}";
            }
            if (AreaTypeFilter != null)
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"AreaTypeFilter={AreaTypeFilter.ToString()}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"AreaTypeFilter={AreaTypeFilter.ToString()}";
            }
            IConfigurationSection pageSizeListSection = Startup.Configuration.GetSection("PageSizeList");
            var pageSizeList = pageSizeListSection.AsEnumerable();
            ViewBag.PageSizeList = new SelectList(pageSizeList.OrderBy(p => p.Key), "Value", "Value");
            if(PageSize == null)
            {
                PageSize = Convert.ToInt32(pageSizeList.Min(p => p.Value));
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
            if (Page!= null)
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Page={Page.ToString()}";
            }
            HttpResponseMessage response = await _HttpApiClient.GetAsync(url + route),
                responseCount = await _HttpApiClient.GetAsync(url + "/count" + routeCount);
            if (response.IsSuccessStatusCode)
            {
                catoes = await response.Content.ReadAsAsync<List<CATO>>();
            }
            int catoesCount = 0;
            if (responseCount.IsSuccessStatusCode)
            {
                catoesCount = await responseCount.Content.ReadAsAsync<int>();
            }
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page!=null ? (int)Page : 1;
            ViewBag.TotalPages = PageSize != null ? (int)(Math.Ceiling((decimal)catoesCount / (decimal)PageSize)) : 1;
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

            return View(catoes);
        }

        public async Task<IActionResult> Details(int? id,
            string SortOrder,
            int? EgovIdFilter,
            string CodeFilter,
            string NameKKFilter,
            string NameRUFilter,
            int? ParentFilter,
            int? AreaTypeFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.EgovIdFilter = EgovIdFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.PaNameRUFilterge = NameRUFilter;
            ViewBag.ParentFilter = ParentFilter;
            ViewBag.AreaTypeFilter = AreaTypeFilter;

            CATO cato = null;
            HttpResponseMessage response = await _HttpApiClient.GetAsync($"api/CATO/{id.ToString()}");
            if (response.IsSuccessStatusCode)
            {
                cato = await response.Content.ReadAsAsync<CATO>();
            }
            return View(cato);
        }

        public IActionResult Create(string SortOrder,
            int? EgovIdFilter,
            string CodeFilter,
            string NameKKFilter,
            string NameRUFilter,
            int? ParentFilter,
            int? AreaTypeFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.EgovIdFilter = EgovIdFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.PaNameRUFilterge = NameRUFilter;
            ViewBag.ParentFilter = ParentFilter;
            ViewBag.AreaTypeFilter = AreaTypeFilter;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Code,NameKK,NameRU,Parent,AreaType,EgovId")] CATO cato,
            string SortOrder,
            int? EgovIdFilter,
            string CodeFilter,
            string NameKKFilter,
            string NameRUFilter,
            int? ParentFilter,
            int? AreaTypeFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.EgovIdFilter = EgovIdFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.PaNameRUFilterge = NameRUFilter;
            ViewBag.ParentFilter = ParentFilter;
            ViewBag.AreaTypeFilter = AreaTypeFilter;
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _HttpApiClient.PostAsJsonAsync(
                    "api/cato", cato);

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
                    return View(cato);
                }

                return RedirectToAction(nameof(Index), new { PageSize = ViewBag.PageSize, Page = ViewBag.Page, SortOrder = ViewBag.SortOrder, EgovIdFilter = ViewBag.EgovIdFilter, CodeFilter = ViewBag.CodeFilter, NameKKFilter = ViewBag.NameKKFilter, NameRUFilter = ViewBag.NameRUFilter, ParentFilter = ViewBag.ParentFilter, AreaTypeFilter = ViewBag.AreaTypeFilter });
            }
            return View(cato);
        }

        public async Task<IActionResult> Edit(int? id,
            string SortOrder,
            int? EgovIdFilter,
            string CodeFilter,
            string NameKKFilter,
            string NameRUFilter,
            int? ParentFilter,
            int? AreaTypeFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.EgovIdFilter = EgovIdFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.PaNameRUFilterge = NameRUFilter;
            ViewBag.ParentFilter = ParentFilter;
            ViewBag.AreaTypeFilter = AreaTypeFilter;
            CATO cato = null;
            HttpResponseMessage response = await _HttpApiClient.GetAsync($"api/CATO/{id.ToString()}");
            if (response.IsSuccessStatusCode)
            {
                cato = await response.Content.ReadAsAsync<CATO>();
            }
            return View(cato);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,NameKK,NameRU,Parent,AreaType,EgovId")] CATO cato,
            string SortOrder,
            int? EgovIdFilter,
            string CodeFilter,
            string NameKKFilter,
            string NameRUFilter,
            int? ParentFilter,
            int? AreaTypeFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.EgovIdFilter = EgovIdFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.PaNameRUFilterge = NameRUFilter;
            ViewBag.ParentFilter = ParentFilter;
            ViewBag.AreaTypeFilter = AreaTypeFilter;
            if (id != cato.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _HttpApiClient.PutAsJsonAsync(
                    $"api/cato/{cato.Id}", cato);

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
                    return View(cato);
                }

                cato = await response.Content.ReadAsAsync<CATO>();
                return RedirectToAction(nameof(Index), new { PageSize = ViewBag.PageSize, Page = ViewBag.Page, SortOrder = ViewBag.SortOrder, EgovIdFilter = ViewBag.EgovIdFilter, CodeFilter = ViewBag.CodeFilter, NameKKFilter = ViewBag.NameKKFilter, NameRUFilter = ViewBag.NameRUFilter, ParentFilter = ViewBag.ParentFilter, AreaTypeFilter = ViewBag.AreaTypeFilter });
            }
            return View(cato);
        }

        public async Task<IActionResult> Delete(int? id,
            string SortOrder,
            int? EgovIdFilter,
            string CodeFilter,
            string NameKKFilter,
            string NameRUFilter,
            int? ParentFilter,
            int? AreaTypeFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.EgovIdFilter = EgovIdFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.PaNameRUFilterge = NameRUFilter;
            ViewBag.ParentFilter = ParentFilter;
            ViewBag.AreaTypeFilter = AreaTypeFilter;
            if (id == null)
            {
                return NotFound();
            }

            CATO cato = null;
            HttpResponseMessage response = await _HttpApiClient.GetAsync($"api/CATO/{id.ToString()}");
            if (response.IsSuccessStatusCode)
            {
                cato = await response.Content.ReadAsAsync<CATO>();
            }
            if (cato == null)
            {
                return NotFound();
            }
            return View(cato);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id,
            string SortOrder,
            int? EgovIdFilter,
            string CodeFilter,
            string NameKKFilter,
            string NameRUFilter,
            int? ParentFilter,
            int? AreaTypeFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.EgovIdFilter = EgovIdFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.PaNameRUFilterge = NameRUFilter;
            ViewBag.ParentFilter = ParentFilter;
            ViewBag.AreaTypeFilter = AreaTypeFilter;
            HttpResponseMessage response = await _HttpApiClient.DeleteAsync(
                $"api/cato/{id}");
            return RedirectToAction(nameof(Index), new { PageSize = ViewBag.PageSize, Page = ViewBag.Page, SortOrder = ViewBag.SortOrder, EgovIdFilter = ViewBag.EgovIdFilter, CodeFilter = ViewBag.CodeFilter, NameKKFilter = ViewBag.NameKKFilter, NameRUFilter = ViewBag.NameRUFilter, ParentFilter = ViewBag.ParentFilter, AreaTypeFilter = ViewBag.AreaTypeFilter });
        }

        public ActionResult Parse(string SortOrder,
            int? EgovIdFilter,
            string CodeFilter,
            string NameKKFilter,
            string NameRUFilter,
            int? ParentFilter,
            int? AreaTypeFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.EgovIdFilter = EgovIdFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.PaNameRUFilterge = NameRUFilter;
            ViewBag.ParentFilter = ParentFilter;
            ViewBag.AreaTypeFilter = AreaTypeFilter;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Parse(string Url,
            string SortOrder,
            int? EgovIdFilter,
            string CodeFilter,
            string NameKKFilter,
            string NameRUFilter,
            int? ParentFilter,
            int? AreaTypeFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.EgovIdFilter = EgovIdFilter;
            ViewBag.CodeFilter = CodeFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.PaNameRUFilterge = NameRUFilter;
            ViewBag.ParentFilter = ParentFilter;
            ViewBag.AreaTypeFilter = AreaTypeFilter;
            string url = "api/CATO",
                route = "?";
            route += $"Url={Url}";
            HttpResponseMessage response = await _HttpApiClient.GetAsync(url + "/parse" + route);
            string result = "";
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<string>();
            }
            ViewBag.Result = result;
            return View();
        }
    }
}