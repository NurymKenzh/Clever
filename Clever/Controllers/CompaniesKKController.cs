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
    public class CompaniesKKController : Controller
    {
        private readonly HttpApiClientController _HttpApiClient;

        public CompaniesKKController(HttpApiClientController HttpApiClient)
        {
            _HttpApiClient = HttpApiClient;
        }

        public async Task<IActionResult> Index(string SortOrder,
            string Search,
            string BINFilter,
            string NameKKFilter,
            string NameRUFilter,
            DateTime? DateRegisterFilter,
            string OKEDFilter,
            string ActivityKindKKFilter,
            string ActivityKindRUFilter,
            string OKEDSecondaryFilter,
            string KRPFilter,
            string KRPNameKKFilter,
            string KRPNameRUFilter,
            string CATOFilter,
            string LocalityKKFilter,
            string LocalityRUFilter,
            string LegalAddressFilter,
            string HeadNameFilter,
            int? PageSize,
            int? Page)
        {
            List<CompanyKK> companiesKK = new List<CompanyKK>();

            ViewBag.BINFilter = BINFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.NameRUFilter = NameRUFilter;
            ViewBag.DateRegisterFilter = DateRegisterFilter;
            ViewBag.OKEDFilter = OKEDFilter;
            ViewBag.ActivityKindKKFilter = ActivityKindKKFilter;
            ViewBag.ActivityKindRUFilter = ActivityKindRUFilter;
            ViewBag.OKEDSecondaryFilter = OKEDSecondaryFilter;
            ViewBag.KRPFilter = KRPFilter;
            ViewBag.KRPNameKKFilter = KRPNameKKFilter;
            ViewBag.KRPNameRUFilter = KRPNameRUFilter;
            ViewBag.CATOFilter = CATOFilter;
            ViewBag.LocalityKKFilter = LocalityKKFilter;
            ViewBag.LocalityRUFilter = LocalityRUFilter;
            ViewBag.LegalAddressFilter = LegalAddressFilter;
            ViewBag.HeadNameFilter = HeadNameFilter;

            ViewBag.BINSort = SortOrder == "BIN" ? "BINDesc" : "BIN";
            ViewBag.NameKKSort = SortOrder == "NameKK" ? "NameKKDesc" : "NameKK";
            ViewBag.NameRUSort = SortOrder == "NameRU" ? "NameRUDesc" : "NameRU";
            ViewBag.DateRegisterSort = SortOrder == "DateRegister" ? "DateRegisterDesc" : "DateRegister";
            ViewBag.OKEDSort = SortOrder == "OKED" ? "OKEDDesc" : "OKED";
            ViewBag.ActivityKindKKSort = SortOrder == "ActivityKindKK" ? "ActivityKindKKDesc" : "ActivityKindKK";
            ViewBag.ActivityKindRUSort = SortOrder == "ActivityKindRU" ? "ActivityKindRUDesc" : "ActivityKindRU";
            ViewBag.OKEDSecondarySort = SortOrder == "OKEDSecondary" ? "OKEDSecondaryDesc" : "OKEDSecondary";
            ViewBag.KRPSort = SortOrder == "KRP" ? "KRPDesc" : "KRP";
            ViewBag.KRPNameKKSort = SortOrder == "KRPNameKK" ? "KRPNameKKDesc" : "KRPNameKK";
            ViewBag.KRPNameRUSort = SortOrder == "KRPNameRU" ? "KRPNameRUDesc" : "KRPNameRU";
            ViewBag.CATOSort = SortOrder == "CATO" ? "CATODesc" : "CATO";
            ViewBag.LocalityKKSort = SortOrder == "LocalityKK" ? "LocalityKKDesc" : "LocalityKK";
            ViewBag.LocalityRUSort = SortOrder == "LocalityRU" ? "LocalityRUDesc" : "LocalityRU";
            ViewBag.LegalAddressSort = SortOrder == "LegalAddress" ? "LegalAddressDesc" : "LegalAddress";
            ViewBag.HeadNameSort = SortOrder == "HeadName" ? "HeadNameDesc" : "HeadName";

            string url = "api/CompaniesKK",
                route = "",
                routeCount = "";
            if (!string.IsNullOrEmpty(SortOrder))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"SortOrder={SortOrder}";
            }
            if (!string.IsNullOrEmpty(Search))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Search={Search}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"Search={Search}";
            }
            if (!string.IsNullOrEmpty(BINFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"BIN={BINFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"BIN={BINFilter}";
            }
            if (!string.IsNullOrEmpty(NameKKFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"NameKK={NameKKFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"NameKK={NameKKFilter}";
            }
            if (!string.IsNullOrEmpty(NameRUFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"NameRU={NameRUFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"NameRU={NameRUFilter}";
            }
            if (DateRegisterFilter != null)
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"DateRegister={DateRegisterFilter.ToString()}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"DateRegister={DateRegisterFilter.ToString()}";
            }
            if (!string.IsNullOrEmpty(OKEDFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"OKED={OKEDFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"OKED={OKEDFilter}";
            }
            if (!string.IsNullOrEmpty(ActivityKindKKFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"ActivityKindKK={ActivityKindKKFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"ActivityKindKK={ActivityKindKKFilter}";
            }
            if (!string.IsNullOrEmpty(ActivityKindRUFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"ActivityKindRU={ActivityKindRUFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"ActivityKindRU={ActivityKindRUFilter}";
            }
            if (!string.IsNullOrEmpty(OKEDSecondaryFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"OKEDSecondary={OKEDSecondaryFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"OKEDSecondary={OKEDSecondaryFilter}";
            }
            if (!string.IsNullOrEmpty(KRPFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"KRP={KRPFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"KRP={KRPFilter}";
            }
            if (!string.IsNullOrEmpty(KRPNameKKFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"KRPNameKK={KRPNameKKFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"KRPNameKK={KRPNameKKFilter}";
            }
            if (!string.IsNullOrEmpty(KRPNameRUFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"KRPNameRU={KRPNameRUFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"KRPNameRU={KRPNameRUFilter}";
            }
            if (!string.IsNullOrEmpty(CATOFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"CATO={CATOFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"CATO={CATOFilter}";
            }
            if (!string.IsNullOrEmpty(LocalityKKFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"LocalityKK={LocalityKKFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"LocalityKK={LocalityKKFilter}";
            }
            if (!string.IsNullOrEmpty(LocalityRUFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"LocalityRU={LocalityRUFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"LocalityRU={LocalityRUFilter}";
            }
            if (!string.IsNullOrEmpty(LegalAddressFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"LegalAddress={LegalAddressFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"LegalAddress={LegalAddressFilter}";
            }
            if (!string.IsNullOrEmpty(HeadNameFilter))
            {
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"HeadName={HeadNameFilter}";
                routeCount += string.IsNullOrEmpty(routeCount) ? "?" : "&";
                routeCount += $"HeadName={HeadNameFilter}";
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
            if (PageSize == 0)
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
                companiesKK = await response.Content.ReadAsAsync<List<CompanyKK>>();
            }
            int companiesKKCount = 0;
            if (responseCount.IsSuccessStatusCode)
            {
                companiesKKCount = await responseCount.Content.ReadAsAsync<int>();
            }
            ViewBag.Search = Search;
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page != null ? (int)Page : 1;
            ViewBag.TotalPages = PageSize != null ? (int)(Math.Ceiling((decimal)companiesKKCount / (decimal)PageSize)) : 1;
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

            return View(companiesKK);
        }

        public async Task<IActionResult> Details(int? id,
            string Search,
            string SortOrder,
            string BINFilter,
            string NameKKFilter,
            string NameRUFilter,
            DateTime? DateRegisterFilter,
            string OKEDFilter,
            string ActivityKindKKFilter,
            string ActivityKindRUFilter,
            string OKEDSecondaryFilter,
            string KRPFilter,
            string KRPNameKKFilter,
            string KRPNameRUFilter,
            string CATOFilter,
            string LocalityKKFilter,
            string LocalityRUFilter,
            string LegalAddressFilter,
            string HeadNameFilter,
            int? PageSize,
            int? Page)
        {
            CompanyKK companyKK = null;
            HttpResponseMessage response = await _HttpApiClient.GetAsync($"api/CompaniesKK/{id.ToString()}");
            if (response.IsSuccessStatusCode)
            {
                companyKK = await response.Content.ReadAsAsync<CompanyKK>();
            }
            ViewBag.Search = Search;
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.BINFilter = BINFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.NameRUFilter = NameRUFilter;
            ViewBag.DateRegisterFilter = DateRegisterFilter;
            ViewBag.OKEDFilter = OKEDFilter;
            ViewBag.ActivityKindKKFilter = ActivityKindKKFilter;
            ViewBag.ActivityKindRUFilter = ActivityKindRUFilter;
            ViewBag.OKEDSecondaryFilter = OKEDSecondaryFilter;
            ViewBag.KRPFilter = KRPFilter;
            ViewBag.KRPNameKKFilter = KRPNameKKFilter;
            ViewBag.KRPNameRUFilter = KRPNameRUFilter;
            ViewBag.CATOFilter = CATOFilter;
            ViewBag.LocalityKKFilter = LocalityKKFilter;
            ViewBag.LocalityRUFilter = LocalityRUFilter;
            ViewBag.LegalAddressFilter = LegalAddressFilter;
            ViewBag.HeadNameFilter = HeadNameFilter;
            return View(companyKK);
        }

        public IActionResult Create(string SortOrder,
            string Search,
            string BINFilter,
            string NameKKFilter,
            string NameRUFilter,
            DateTime? DateRegisterFilter,
            string OKEDFilter,
            string ActivityKindKKFilter,
            string ActivityKindRUFilter,
            string OKEDSecondaryFilter,
            string KRPFilter,
            string KRPNameKKFilter,
            string KRPNameRUFilter,
            string CATOFilter,
            string LocalityKKFilter,
            string LocalityRUFilter,
            string LegalAddressFilter,
            string HeadNameFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.Search = Search;
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.BINFilter = BINFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.NameRUFilter = NameRUFilter;
            ViewBag.DateRegisterFilter = DateRegisterFilter;
            ViewBag.OKEDFilter = OKEDFilter;
            ViewBag.ActivityKindKKFilter = ActivityKindKKFilter;
            ViewBag.ActivityKindRUFilter = ActivityKindRUFilter;
            ViewBag.OKEDSecondaryFilter = OKEDSecondaryFilter;
            ViewBag.KRPFilter = KRPFilter;
            ViewBag.KRPNameKKFilter = KRPNameKKFilter;
            ViewBag.KRPNameRUFilter = KRPNameRUFilter;
            ViewBag.CATOFilter = CATOFilter;
            ViewBag.LocalityKKFilter = LocalityKKFilter;
            ViewBag.LocalityRUFilter = LocalityRUFilter;
            ViewBag.LegalAddressFilter = LegalAddressFilter;
            ViewBag.HeadNameFilter = HeadNameFilter;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,BIN,NameKK,NameRU,DateRegister,OKED,ActivityKindKK,ActivityKindRU,OKEDSecondary,KRP,KRPNameKK,KRPNameRU,CATO,LocalityKK,LocalityRU,LegalAddress,HeadName")] CompanyKK companyKK,
            string SortOrder,
            string Search,
            string BINFilter,
            string NameKKFilter,
            string NameRUFilter,
            DateTime? DateRegisterFilter,
            string OKEDFilter,
            string ActivityKindKKFilter,
            string ActivityKindRUFilter,
            string OKEDSecondaryFilter,
            string KRPFilter,
            string KRPNameKKFilter,
            string KRPNameRUFilter,
            string CATOFilter,
            string LocalityKKFilter,
            string LocalityRUFilter,
            string LegalAddressFilter,
            string HeadNameFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.Search = Search;
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.BINFilter = BINFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.NameRUFilter = NameRUFilter;
            ViewBag.DateRegisterFilter = DateRegisterFilter;
            ViewBag.OKEDFilter = OKEDFilter;
            ViewBag.ActivityKindKKFilter = ActivityKindKKFilter;
            ViewBag.ActivityKindRUFilter = ActivityKindRUFilter;
            ViewBag.OKEDSecondaryFilter = OKEDSecondaryFilter;
            ViewBag.KRPFilter = KRPFilter;
            ViewBag.KRPNameKKFilter = KRPNameKKFilter;
            ViewBag.KRPNameRUFilter = KRPNameRUFilter;
            ViewBag.CATOFilter = CATOFilter;
            ViewBag.LocalityKKFilter = LocalityKKFilter;
            ViewBag.LocalityRUFilter = LocalityRUFilter;
            ViewBag.LegalAddressFilter = LegalAddressFilter;
            ViewBag.HeadNameFilter = HeadNameFilter;
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _HttpApiClient.PostAsJsonAsync(
                    "api/CompaniesKK", companyKK);

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
                    return View(companyKK);
                }

                return RedirectToAction(nameof(Index), new
                {
                    PageSize = ViewBag.PageSize,
                    Search = ViewBag.Search,
                    SortOrder = ViewBag.SortOrder,
                    Page = ViewBag.Page,
                    BINFilter = ViewBag.BINFilter,
                    NameKKFilter = ViewBag.NameKKFilter,
                    NameRUFilter = ViewBag.NameRUFilter,
                    DateRegisterFilter = ViewBag.DateRegisterFilter,
                    OKEDFilter = ViewBag.OKEDFilter,
                    ActivityKindKKFilter = ViewBag.ActivityKindKKFilter,
                    ActivityKindRUFilter = ViewBag.ActivityKindRUFilter,
                    OKEDSecondaryFilter = ViewBag.OKEDSecondaryFilter,
                    KRPFilter = ViewBag.KRPFilter,
                    KRPNameKKFilter = ViewBag.KRPNameKKFilter,
                    KRPNameRUFilter = ViewBag.KRPNameRUFilter,
                    CATOFilter = ViewBag.CATOFilter,
                    LocalityKKFilter = ViewBag.LocalityKKFilter,
                    LocalityRUFilter = ViewBag.LocalityRUFilter,
                    LegalAddressFilter = ViewBag.LegalAddressFilter,
                    HeadNameFilter = ViewBag.HeadNameFilter
                });
            }
            return View(companyKK);
        }

        public async Task<IActionResult> Edit(int? id,
            string Search,
            string SortOrder,
            string BINFilter,
            string NameKKFilter,
            string NameRUFilter,
            DateTime? DateRegisterFilter,
            string OKEDFilter,
            string ActivityKindKKFilter,
            string ActivityKindRUFilter,
            string OKEDSecondaryFilter,
            string KRPFilter,
            string KRPNameKKFilter,
            string KRPNameRUFilter,
            string CATOFilter,
            string LocalityKKFilter,
            string LocalityRUFilter,
            string LegalAddressFilter,
            string HeadNameFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.Search = Search;
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.BINFilter = BINFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.NameRUFilter = NameRUFilter;
            ViewBag.DateRegisterFilter = DateRegisterFilter;
            ViewBag.OKEDFilter = OKEDFilter;
            ViewBag.ActivityKindKKFilter = ActivityKindKKFilter;
            ViewBag.ActivityKindRUFilter = ActivityKindRUFilter;
            ViewBag.OKEDSecondaryFilter = OKEDSecondaryFilter;
            ViewBag.KRPFilter = KRPFilter;
            ViewBag.KRPNameKKFilter = KRPNameKKFilter;
            ViewBag.KRPNameRUFilter = KRPNameRUFilter;
            ViewBag.CATOFilter = CATOFilter;
            ViewBag.LocalityKKFilter = LocalityKKFilter;
            ViewBag.LocalityRUFilter = LocalityRUFilter;
            ViewBag.LegalAddressFilter = LegalAddressFilter;
            ViewBag.HeadNameFilter = HeadNameFilter;
            CompanyKK companyKK = null;
            HttpResponseMessage response = await _HttpApiClient.GetAsync($"api/CompaniesKK/{id.ToString()}");
            if (response.IsSuccessStatusCode)
            {
                companyKK = await response.Content.ReadAsAsync<CompanyKK>();
            }
            return View(companyKK);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BIN,NameKK,NameRU,DateRegister,OKED,ActivityKindKK,ActivityKindRU,OKEDSecondary,KRP,KRPNameKK,KRPNameRU,CATO,LocalityKK,LocalityRU,LegalAddress,HeadName")] CompanyKK companyKK,
            string SortOrder,
            string Search,
            string BINFilter,
            string NameKKFilter,
            string NameRUFilter,
            DateTime? DateRegisterFilter,
            string OKEDFilter,
            string ActivityKindKKFilter,
            string ActivityKindRUFilter,
            string OKEDSecondaryFilter,
            string KRPFilter,
            string KRPNameKKFilter,
            string KRPNameRUFilter,
            string CATOFilter,
            string LocalityKKFilter,
            string LocalityRUFilter,
            string LegalAddressFilter,
            string HeadNameFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.Search = Search;
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.BINFilter = BINFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.NameRUFilter = NameRUFilter;
            ViewBag.DateRegisterFilter = DateRegisterFilter;
            ViewBag.OKEDFilter = OKEDFilter;
            ViewBag.ActivityKindKKFilter = ActivityKindKKFilter;
            ViewBag.ActivityKindRUFilter = ActivityKindRUFilter;
            ViewBag.OKEDSecondaryFilter = OKEDSecondaryFilter;
            ViewBag.KRPFilter = KRPFilter;
            ViewBag.KRPNameKKFilter = KRPNameKKFilter;
            ViewBag.KRPNameRUFilter = KRPNameRUFilter;
            ViewBag.CATOFilter = CATOFilter;
            ViewBag.LocalityKKFilter = LocalityKKFilter;
            ViewBag.LocalityRUFilter = LocalityRUFilter;
            ViewBag.LegalAddressFilter = LegalAddressFilter;
            ViewBag.HeadNameFilter = HeadNameFilter;
            if (id != companyKK.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _HttpApiClient.PutAsJsonAsync(
                    $"api/CompaniesKK/{companyKK.Id}", companyKK);

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
                    return View(companyKK);
                }

                companyKK = await response.Content.ReadAsAsync<CompanyKK>();
                return RedirectToAction(nameof(Index), new
                {
                    PageSize = ViewBag.PageSize,
                    Search = ViewBag.Search,
                    SortOrder = ViewBag.SortOrder,
                    Page = ViewBag.Page,
                    BINFilter = ViewBag.BINFilter,
                    NameKKFilter = ViewBag.NameKKFilter,
                    NameRUFilter = ViewBag.NameRUFilter,
                    DateRegisterFilter = ViewBag.DateRegisterFilter,
                    OKEDFilter = ViewBag.OKEDFilter,
                    ActivityKindKKFilter = ViewBag.ActivityKindKKFilter,
                    ActivityKindRUFilter = ViewBag.ActivityKindRUFilter,
                    OKEDSecondaryFilter = ViewBag.OKEDSecondaryFilter,
                    KRPFilter = ViewBag.KRPFilter,
                    KRPNameKKFilter = ViewBag.KRPNameKKFilter,
                    KRPNameRUFilter = ViewBag.KRPNameRUFilter,
                    CATOFilter = ViewBag.CATOFilter,
                    LocalityKKFilter = ViewBag.LocalityKKFilter,
                    LocalityRUFilter = ViewBag.LocalityRUFilter,
                    LegalAddressFilter = ViewBag.LegalAddressFilter,
                    HeadNameFilter = ViewBag.HeadNameFilter
                });
            }
            return View(companyKK);
        }

        public async Task<IActionResult> Delete(int? id,
            string Search,
            string SortOrder,
            string BINFilter,
            string NameKKFilter,
            string NameRUFilter,
            DateTime? DateRegisterFilter,
            string OKEDFilter,
            string ActivityKindKKFilter,
            string ActivityKindRUFilter,
            string OKEDSecondaryFilter,
            string KRPFilter,
            string KRPNameKKFilter,
            string KRPNameRUFilter,
            string CATOFilter,
            string LocalityKKFilter,
            string LocalityRUFilter,
            string LegalAddressFilter,
            string HeadNameFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.Search = Search;
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.BINFilter = BINFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.NameRUFilter = NameRUFilter;
            ViewBag.DateRegisterFilter = DateRegisterFilter;
            ViewBag.OKEDFilter = OKEDFilter;
            ViewBag.ActivityKindKKFilter = ActivityKindKKFilter;
            ViewBag.ActivityKindRUFilter = ActivityKindRUFilter;
            ViewBag.OKEDSecondaryFilter = OKEDSecondaryFilter;
            ViewBag.KRPFilter = KRPFilter;
            ViewBag.KRPNameKKFilter = KRPNameKKFilter;
            ViewBag.KRPNameRUFilter = KRPNameRUFilter;
            ViewBag.CATOFilter = CATOFilter;
            ViewBag.LocalityKKFilter = LocalityKKFilter;
            ViewBag.LocalityRUFilter = LocalityRUFilter;
            ViewBag.LegalAddressFilter = LegalAddressFilter;
            ViewBag.HeadNameFilter = HeadNameFilter;
            if (id == null)
            {
                return NotFound();
            }

            CompanyKK companyKK = null;
            HttpResponseMessage response = await _HttpApiClient.GetAsync($"api/CompaniesKK/{id.ToString()}");
            if (response.IsSuccessStatusCode)
            {
                companyKK = await response.Content.ReadAsAsync<CompanyKK>();
            }
            if (companyKK == null)
            {
                return NotFound();
            }
            return View(companyKK);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id,
            string Search,
            string SortOrder,
            string BINFilter,
            string NameKKFilter,
            string NameRUFilter,
            DateTime? DateRegisterFilter,
            string OKEDFilter,
            string ActivityKindKKFilter,
            string ActivityKindRUFilter,
            string OKEDSecondaryFilter,
            string KRPFilter,
            string KRPNameKKFilter,
            string KRPNameRUFilter,
            string CATOFilter,
            string LocalityKKFilter,
            string LocalityRUFilter,
            string LegalAddressFilter,
            string HeadNameFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.Search = Search;
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.BINFilter = BINFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.NameRUFilter = NameRUFilter;
            ViewBag.DateRegisterFilter = DateRegisterFilter;
            ViewBag.OKEDFilter = OKEDFilter;
            ViewBag.ActivityKindKKFilter = ActivityKindKKFilter;
            ViewBag.ActivityKindRUFilter = ActivityKindRUFilter;
            ViewBag.OKEDSecondaryFilter = OKEDSecondaryFilter;
            ViewBag.KRPFilter = KRPFilter;
            ViewBag.KRPNameKKFilter = KRPNameKKFilter;
            ViewBag.KRPNameRUFilter = KRPNameRUFilter;
            ViewBag.CATOFilter = CATOFilter;
            ViewBag.LocalityKKFilter = LocalityKKFilter;
            ViewBag.LocalityRUFilter = LocalityRUFilter;
            ViewBag.LegalAddressFilter = LegalAddressFilter;
            ViewBag.HeadNameFilter = HeadNameFilter;
            HttpResponseMessage response = await _HttpApiClient.DeleteAsync(
                $"api/CompaniesKK/{id}");
            return RedirectToAction(nameof(Index), new
            {
                PageSize = ViewBag.PageSize,
                Search = ViewBag.Search,
                SortOrder = ViewBag.SortOrder,
                Page = ViewBag.Page,
                BINFilter = ViewBag.BINFilter,
                NameKKFilter = ViewBag.NameKKFilter,
                NameRUFilter = ViewBag.NameRUFilter,
                DateRegisterFilter = ViewBag.DateRegisterFilter,
                OKEDFilter = ViewBag.OKEDFilter,
                ActivityKindKKFilter = ViewBag.ActivityKindKKFilter,
                ActivityKindRUFilter = ViewBag.ActivityKindRUFilter,
                OKEDSecondaryFilter = ViewBag.OKEDSecondaryFilter,
                KRPFilter = ViewBag.KRPFilter,
                KRPNameKKFilter = ViewBag.KRPNameKKFilter,
                KRPNameRUFilter = ViewBag.KRPNameRUFilter,
                CATOFilter = ViewBag.CATOFilter,
                LocalityKKFilter = ViewBag.LocalityKKFilter,
                LocalityRUFilter = ViewBag.LocalityRUFilter,
                LegalAddressFilter = ViewBag.LegalAddressFilter,
                HeadNameFilter = ViewBag.HeadNameFilter
            });
        }

        [DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        public IActionResult UploadFiles(string SortOrder,
            string Search,
            string BINFilter,
            string NameKKFilter,
            string NameRUFilter,
            DateTime? DateRegisterFilter,
            string OKEDFilter,
            string ActivityKindKKFilter,
            string ActivityKindRUFilter,
            string OKEDSecondaryFilter,
            string KRPFilter,
            string KRPNameKKFilter,
            string KRPNameRUFilter,
            string CATOFilter,
            string LocalityKKFilter,
            string LocalityRUFilter,
            string LegalAddressFilter,
            string HeadNameFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.Search = Search;
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.BINFilter = BINFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.NameRUFilter = NameRUFilter;
            ViewBag.DateRegisterFilter = DateRegisterFilter;
            ViewBag.OKEDFilter = OKEDFilter;
            ViewBag.ActivityKindKKFilter = ActivityKindKKFilter;
            ViewBag.ActivityKindRUFilter = ActivityKindRUFilter;
            ViewBag.OKEDSecondaryFilter = OKEDSecondaryFilter;
            ViewBag.KRPFilter = KRPFilter;
            ViewBag.KRPNameKKFilter = KRPNameKKFilter;
            ViewBag.KRPNameRUFilter = KRPNameRUFilter;
            ViewBag.CATOFilter = CATOFilter;
            ViewBag.LocalityKKFilter = LocalityKKFilter;
            ViewBag.LocalityRUFilter = LocalityRUFilter;
            ViewBag.LegalAddressFilter = LegalAddressFilter;
            ViewBag.HeadNameFilter = HeadNameFilter;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<string> UploadFiles(List<IFormFile> Files,
            string Search,
            string SortOrder,
            string BINFilter,
            string NameKKFilter,
            string NameRUFilter,
            DateTime? DateRegisterFilter,
            string OKEDFilter,
            string ActivityKindKKFilter,
            string ActivityKindRUFilter,
            string OKEDSecondaryFilter,
            string KRPFilter,
            string KRPNameKKFilter,
            string KRPNameRUFilter,
            string CATOFilter,
            string LocalityKKFilter,
            string LocalityRUFilter,
            string LegalAddressFilter,
            string HeadNameFilter,
            int? PageSize,
            int? Page)
        {
            ViewBag.Search = Search;
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageSize = PageSize;
            ViewBag.Page = Page;
            ViewBag.BINFilter = BINFilter;
            ViewBag.NameKKFilter = NameKKFilter;
            ViewBag.NameRUFilter = NameRUFilter;
            ViewBag.DateRegisterFilter = DateRegisterFilter;
            ViewBag.OKEDFilter = OKEDFilter;
            ViewBag.ActivityKindKKFilter = ActivityKindKKFilter;
            ViewBag.ActivityKindRUFilter = ActivityKindRUFilter;
            ViewBag.OKEDSecondaryFilter = OKEDSecondaryFilter;
            ViewBag.KRPFilter = KRPFilter;
            ViewBag.KRPNameKKFilter = KRPNameKKFilter;
            ViewBag.KRPNameRUFilter = KRPNameRUFilter;
            ViewBag.CATOFilter = CATOFilter;
            ViewBag.LocalityKKFilter = LocalityKKFilter;
            ViewBag.LocalityRUFilter = LocalityRUFilter;
            ViewBag.LegalAddressFilter = LegalAddressFilter;
            ViewBag.HeadNameFilter = HeadNameFilter;
            string report = "";
            foreach (IFormFile file in Files)
            {
                byte[] data;
                using (var br = new BinaryReader(file.OpenReadStream()))
                    data = br.ReadBytes((int)file.OpenReadStream().Length);
                ByteArrayContent bytes = new ByteArrayContent(data);
                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                multiContent.Add(bytes, "file", file.FileName);
                var result = await _HttpApiClient.PostAsync("api/CompaniesKK/upload", multiContent).Result.Content.ReadAsStringAsync();
                report += $"\r\n{file.FileName}: {result}";
            }
            return report;
        }
    }
}