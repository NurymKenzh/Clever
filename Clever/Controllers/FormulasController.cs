using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Clever.Controllers
{
    public class FormulasController : Controller
    {
        private readonly HttpApiClientController _HttpApiClient;

        public FormulasController(HttpApiClientController HttpApiClient)
        {
            _HttpApiClient = HttpApiClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FormulaGas()
        {
            return View();
        }

        public IActionResult FormulaCoal()
        {
            return View();
        }

        public async Task<string[]> ListFromRequestToAPI(string url)
        {
            HttpResponseMessage response = await _HttpApiClient.PostAsync(url, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            Regex regex = new Regex(string.Format("\\{0}.*?\\{1}", "\"text\":\"", "\","));
            OutputViewText = regex.Replace(OutputViewText, string.Empty);
            OutputViewText = OutputViewText.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"", "")
                .Replace("disabled", "").Replace(":false,", "").Replace("group", "").Replace(":null,", "").Replace("selected", "")
                .Replace("text", "").Replace("value", "").Replace(":", "");
            string[] gasSectionArray = OutputViewText.Split(new char[] { ',' });
            return gasSectionArray;
        }

        //[HttpPost]
        //public async Task<IActionResult> FormulaHeatPowerThermalPowerPlantsCoal(decimal Ac,
        //    decimal Sc,
        //    decimal Hc,
        //    decimal Wr,
        //    decimal Oc,
        //    decimal Nc,
        //    decimal AFact,
        //    decimal Q,
        //    decimal KPDKA,
        //    decimal Qri,
        //    decimal Q4)
        //{
        //    ViewBag.Ac = Ac;
        //    ViewBag.Sc = Sc;
        //    ViewBag.Hc = Hc;
        //    ViewBag.Wr = Wr;
        //    ViewBag.Oc = Oc;
        //    ViewBag.Nc = Nc;
        //    ViewBag.AFact = AFact;
        //    ViewBag.Q = Q;
        //    ViewBag.KPDKA = KPDKA;
        //    ViewBag.Qri = Qri;
        //    ViewBag.Q4 = Q4;

        //    decimal? ArW = null,
        //        SrW = null,
        //        HrW = null,
        //        OrW = null,
        //        NrW = null,
        //        Cr = null,
        //        Vro2 = null,
        //        V = null,
        //        Vn2 = null,
        //        Vh2o = null,
        //        Vr = null,
        //        Vcr = null,
        //        B = null,
        //        Bp = null;

        //    // ArW
        //    string urlArW = "api/Formulas/ArW",
        //        routeArW = "";
        //    routeArW += string.IsNullOrEmpty(routeArW) ? "?" : "&";
        //    routeArW += $"Ac={Ac.ToString()}".Replace(',', '.');
        //    routeArW += string.IsNullOrEmpty(routeArW) ? "?" : "&";
        //    routeArW += $"Wr={Wr.ToString()}".Replace(',', '.');
        //    HttpResponseMessage responseArW = await _HttpApiClient.PostAsync(urlArW + routeArW, null);
        //    string OutputViewTextArW = await responseArW.Content.ReadAsStringAsync();
        //    OutputViewTextArW = OutputViewTextArW.Replace("<br>", Environment.NewLine);
        //    try
        //    {
        //        responseArW.EnsureSuccessStatusCode();
        //    }
        //    catch
        //    {
        //        dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewTextArW);
        //        foreach (JProperty property in errors.Children())
        //        {
        //            //ModelState.AddModelError(property.Name, property.Value[0].ToString());
        //            ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
        //        }
        //        return View();
        //    }
        //    ViewBag.ArW = OutputViewTextArW.Replace('.', ',');            
        //    try
        //    {
        //        ArW = Convert.ToDecimal(OutputViewTextArW.Replace('.', ','));
        //    }
        //    catch { }
        //    try
        //    {
        //        ArW = Convert.ToDecimal(OutputViewTextArW.Replace(',', '.'));
        //    }
        //    catch { }

        //    // SrW
        //    string urlSrW = "api/Formulas/SrW",
        //        routeSrW = "";
        //    routeSrW += string.IsNullOrEmpty(routeSrW) ? "?" : "&";
        //    routeSrW += $"Sc={Sc.ToString()}".Replace(',', '.');
        //    routeSrW += string.IsNullOrEmpty(routeSrW) ? "?" : "&";
        //    routeSrW += $"Wr={Wr.ToString()}".Replace(',', '.');
        //    HttpResponseMessage responseSrW = await _HttpApiClient.PostAsync(urlSrW + routeSrW, null);
        //    string OutputViewTextSrW = await responseSrW.Content.ReadAsStringAsync();
        //    OutputViewTextSrW = OutputViewTextSrW.Replace("<br>", Environment.NewLine);
        //    try
        //    {
        //        responseSrW.EnsureSuccessStatusCode();
        //    }
        //    catch
        //    {
        //        dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewTextSrW);
        //        foreach (JProperty property in errors.Children())
        //        {
        //            //ModelState.AddModelError(property.Name, property.Value[0].ToString());
        //            ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
        //        }
        //        return View();
        //    }
        //    ViewBag.SrW = OutputViewTextSrW.Replace('.', ',');            
        //    try
        //    {
        //        SrW = Convert.ToDecimal(OutputViewTextSrW.Replace('.', ','));
        //    }
        //    catch { }
        //    try
        //    {
        //        SrW = Convert.ToDecimal(OutputViewTextSrW.Replace(',', '.'));
        //    }
        //    catch { }

        //    // HrW
        //    string urlHrW = "api/Formulas/HrW",
        //        routeHrW = "";
        //    routeHrW += string.IsNullOrEmpty(routeHrW) ? "?" : "&";
        //    routeHrW += $"Hc={Hc.ToString()}".Replace(',', '.');
        //    routeHrW += string.IsNullOrEmpty(routeHrW) ? "?" : "&";
        //    routeHrW += $"Wr={Wr.ToString()}".Replace(',', '.');
        //    HttpResponseMessage responseHrW = await _HttpApiClient.PostAsync(urlHrW + routeHrW, null);
        //    string OutputViewTextHrW = await responseHrW.Content.ReadAsStringAsync();
        //    OutputViewTextHrW = OutputViewTextHrW.Replace("<br>", Environment.NewLine);
        //    try
        //    {
        //        responseHrW.EnsureSuccessStatusCode();
        //    }
        //    catch
        //    {
        //        dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewTextHrW);
        //        foreach (JProperty property in errors.Children())
        //        {
        //            //ModelState.AddModelError(property.Name, property.Value[0].ToString());
        //            ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
        //        }
        //        return View();
        //    }
        //    ViewBag.HrW = OutputViewTextHrW.Replace('.', ',');            
        //    try
        //    {
        //        HrW = Convert.ToDecimal(OutputViewTextHrW.Replace('.', ','));
        //    }
        //    catch { }
        //    try
        //    {
        //        HrW = Convert.ToDecimal(OutputViewTextHrW.Replace(',', '.'));
        //    }
        //    catch { }

        //    // OrW
        //    string urlOrW = "api/Formulas/OrW",
        //        routeOrW = "";
        //    routeOrW += string.IsNullOrEmpty(routeOrW) ? "?" : "&";
        //    routeOrW += $"Oc={Oc.ToString()}".Replace(',', '.');
        //    routeOrW += string.IsNullOrEmpty(routeOrW) ? "?" : "&";
        //    routeOrW += $"Wr={Wr.ToString()}".Replace(',', '.');
        //    HttpResponseMessage responseOrW = await _HttpApiClient.PostAsync(urlOrW + routeOrW, null);
        //    string OutputViewTextOrW = await responseOrW.Content.ReadAsStringAsync();
        //    OutputViewTextOrW = OutputViewTextOrW.Replace("<br>", Environment.NewLine);
        //    try
        //    {
        //        responseOrW.EnsureSuccessStatusCode();
        //    }
        //    catch
        //    {
        //        dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewTextOrW);
        //        foreach (JProperty property in errors.Children())
        //        {
        //            //ModelState.AddModelError(property.Name, property.Value[0].ToString());
        //            ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
        //        }
        //        return View();
        //    }
        //    ViewBag.OrW = OutputViewTextOrW.Replace('.', ',');            
        //    try
        //    {
        //        OrW = Convert.ToDecimal(OutputViewTextOrW.Replace('.', ','));
        //    }
        //    catch { }
        //    try
        //    {
        //        OrW = Convert.ToDecimal(OutputViewTextOrW.Replace(',', '.'));
        //    }
        //    catch { }

        //    // NrW
        //    string urlNrW = "api/Formulas/NrW",
        //        routeNrW = "";
        //    routeNrW += string.IsNullOrEmpty(routeNrW) ? "?" : "&";
        //    routeNrW += $"Nc={Nc.ToString()}".Replace(',', '.');
        //    routeNrW += string.IsNullOrEmpty(routeNrW) ? "?" : "&";
        //    routeNrW += $"Wr={Wr.ToString()}".Replace(',', '.');
        //    HttpResponseMessage responseNrW = await _HttpApiClient.PostAsync(urlNrW + routeNrW, null);
        //    string OutputViewTextNrW = await responseNrW.Content.ReadAsStringAsync();
        //    OutputViewTextNrW = OutputViewTextNrW.Replace("<br>", Environment.NewLine);
        //    try
        //    {
        //        responseNrW.EnsureSuccessStatusCode();
        //    }
        //    catch
        //    {
        //        dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewTextNrW);
        //        foreach (JProperty property in errors.Children())
        //        {
        //            //ModelState.AddModelError(property.Name, property.Value[0].ToString());
        //            ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
        //        }
        //        return View();
        //    }
        //    ViewBag.NrW = OutputViewTextNrW.Replace('.', ',');            
        //    try
        //    {
        //        NrW = Convert.ToDecimal(OutputViewTextNrW.Replace('.', ','));
        //    }
        //    catch { }
        //    try
        //    {
        //        NrW = Convert.ToDecimal(OutputViewTextNrW.Replace(',', '.'));
        //    }
        //    catch { }

        //    // Cr
        //    string urlCr = "api/Formulas/Cr",
        //        routeCr = "";
        //    routeCr += string.IsNullOrEmpty(routeCr) ? "?" : "&";
        //    routeCr += $"ArW={ArW.ToString()}".Replace(',', '.');
        //    routeCr += string.IsNullOrEmpty(routeCr) ? "?" : "&";
        //    routeCr += $"SrW={SrW.ToString()}".Replace(',', '.');
        //    routeCr += string.IsNullOrEmpty(routeCr) ? "?" : "&";
        //    routeCr += $"HrW={HrW.ToString()}".Replace(',', '.');
        //    routeCr += string.IsNullOrEmpty(routeCr) ? "?" : "&";
        //    routeCr += $"OrW={OrW.ToString()}".Replace(',', '.');
        //    routeCr += string.IsNullOrEmpty(routeCr) ? "?" : "&";
        //    routeCr += $"NrW={NrW.ToString()}".Replace(',', '.');
        //    routeCr += string.IsNullOrEmpty(routeCr) ? "?" : "&";
        //    routeCr += $"Wr={Wr.ToString()}".Replace(',', '.');
        //    HttpResponseMessage responseCr = await _HttpApiClient.PostAsync(urlCr + routeCr, null);
        //    string OutputViewTextCr = await responseCr.Content.ReadAsStringAsync();
        //    OutputViewTextCr = OutputViewTextCr.Replace("<br>", Environment.NewLine);
        //    try
        //    {
        //        responseCr.EnsureSuccessStatusCode();
        //    }
        //    catch
        //    {
        //        dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewTextCr);
        //        foreach (JProperty property in errors.Children())
        //        {
        //            //ModelState.AddModelError(property.Name, property.Value[0].ToString());
        //            ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
        //        }
        //        return View();
        //    }
        //    ViewBag.Cr = OutputViewTextCr.Replace('.', ',');            
        //    try
        //    {
        //        Cr = Convert.ToDecimal(OutputViewTextCr.Replace('.', ','));
        //    }
        //    catch { }
        //    try
        //    {
        //        Cr = Convert.ToDecimal(OutputViewTextCr.Replace(',', '.'));
        //    }
        //    catch { }

        //    // Vro2
        //    string urlVro2 = "api/Formulas/Vro2",
        //        routeVro2 = "";
        //    routeVro2 += string.IsNullOrEmpty(routeVro2) ? "?" : "&";
        //    routeVro2 += $"Cr={Cr.ToString()}".Replace(',', '.');
        //    routeVro2 += string.IsNullOrEmpty(routeVro2) ? "?" : "&";
        //    routeVro2 += $"SrW={SrW.ToString()}".Replace(',', '.');
        //    HttpResponseMessage responseVro2 = await _HttpApiClient.PostAsync(urlVro2 + routeVro2, null);
        //    string OutputViewTextVro2 = await responseVro2.Content.ReadAsStringAsync();
        //    OutputViewTextVro2 = OutputViewTextVro2.Replace("<br>", Environment.NewLine);
        //    try
        //    {
        //        responseVro2.EnsureSuccessStatusCode();
        //    }
        //    catch
        //    {
        //        dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewTextVro2);
        //        foreach (JProperty property in errors.Children())
        //        {
        //            //ModelState.AddModelError(property.Name, property.Value[0].ToString());
        //            ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
        //        }
        //        return View();
        //    }
        //    ViewBag.Vro2 = OutputViewTextVro2.Replace('.', ',');
        //    try
        //    {
        //        Vro2 = Convert.ToDecimal(OutputViewTextVro2.Replace('.', ','));
        //    }
        //    catch { }
        //    try
        //    {
        //        Vro2 = Convert.ToDecimal(OutputViewTextVro2.Replace(',', '.'));
        //    }
        //    catch { }

        //    // V
        //    string urlV = "api/Formulas/V",
        //        routeV = "";
        //    routeV += string.IsNullOrEmpty(routeV) ? "?" : "&";
        //    routeV += $"Cr={Cr.ToString()}".Replace(',', '.');
        //    routeV += string.IsNullOrEmpty(routeV) ? "?" : "&";
        //    routeV += $"SrW={SrW.ToString()}".Replace(',', '.');
        //    routeV += string.IsNullOrEmpty(routeV) ? "?" : "&";
        //    routeV += $"HrW={HrW.ToString()}".Replace(',', '.');
        //    routeV += string.IsNullOrEmpty(routeV) ? "?" : "&";
        //    routeV += $"OrW={OrW.ToString()}".Replace(',', '.');
        //    HttpResponseMessage responseV = await _HttpApiClient.PostAsync(urlV + routeV, null);
        //    string OutputViewTextV = await responseV.Content.ReadAsStringAsync();
        //    OutputViewTextV = OutputViewTextV.Replace("<br>", Environment.NewLine);
        //    try
        //    {
        //        responseV.EnsureSuccessStatusCode();
        //    }
        //    catch
        //    {
        //        dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewTextV);
        //        foreach (JProperty property in errors.Children())
        //        {
        //            //ModelState.AddModelError(property.Name, property.Value[0].ToString());
        //            ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
        //        }
        //        return View();
        //    }
        //    ViewBag.V = OutputViewTextV.Replace('.', ',');
        //    try
        //    {
        //        V = Convert.ToDecimal(OutputViewTextV.Replace('.', ','));
        //    }
        //    catch { }
        //    try
        //    {
        //        V = Convert.ToDecimal(OutputViewTextV.Replace(',', '.'));
        //    }
        //    catch { }

        //    // Vn2
        //    string urlVn2 = "api/Formulas/Vn2",
        //        routeVn2 = "";
        //    routeVn2 += string.IsNullOrEmpty(routeVn2) ? "?" : "&";
        //    routeVn2 += $"V={V.ToString()}".Replace(',', '.');
        //    routeVn2 += string.IsNullOrEmpty(routeVn2) ? "?" : "&";
        //    routeVn2 += $"NrW={NrW.ToString()}".Replace(',', '.');
        //    HttpResponseMessage responseVn2 = await _HttpApiClient.PostAsync(urlVn2 + routeVn2, null);
        //    string OutputViewTextVn2 = await responseVn2.Content.ReadAsStringAsync();
        //    OutputViewTextVn2 = OutputViewTextVn2.Replace("<br>", Environment.NewLine);
        //    try
        //    {
        //        responseVn2.EnsureSuccessStatusCode();
        //    }
        //    catch
        //    {
        //        dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewTextVn2);
        //        foreach (JProperty property in errors.Children())
        //        {
        //            //ModelState.AddModelError(property.Name, property.Value[0].ToString());
        //            ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
        //        }
        //        return View();
        //    }
        //    ViewBag.Vn2 = OutputViewTextVn2.Replace('.', ',');
        //    try
        //    {
        //        Vn2 = Convert.ToDecimal(OutputViewTextVn2.Replace('.', ','));
        //    }
        //    catch { }
        //    try
        //    {
        //        Vn2 = Convert.ToDecimal(OutputViewTextVn2.Replace(',', '.'));
        //    }
        //    catch { }

        //    // Vh2o
        //    string urlVh2o = "api/Formulas/Vh2o",
        //        routeVh2o = "";
        //    routeVh2o += string.IsNullOrEmpty(routeVh2o) ? "?" : "&";
        //    routeVh2o += $"HrW={HrW.ToString()}".Replace(',', '.');
        //    routeVh2o += string.IsNullOrEmpty(routeVh2o) ? "?" : "&";
        //    routeVh2o += $"Wr={Wr.ToString()}".Replace(',', '.');
        //    routeVh2o += string.IsNullOrEmpty(routeVh2o) ? "?" : "&";
        //    routeVh2o += $"V={V.ToString()}".Replace(',', '.');
        //    HttpResponseMessage responseVh2o = await _HttpApiClient.PostAsync(urlVh2o + routeVh2o, null);
        //    string OutputViewTextVh2o = await responseVh2o.Content.ReadAsStringAsync();
        //    OutputViewTextVh2o = OutputViewTextVh2o.Replace("<br>", Environment.NewLine);
        //    try
        //    {
        //        responseVh2o.EnsureSuccessStatusCode();
        //    }
        //    catch
        //    {
        //        dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewTextVh2o);
        //        foreach (JProperty property in errors.Children())
        //        {
        //            //ModelState.AddModelError(property.Name, property.Value[0].ToString());
        //            ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
        //        }
        //        return View();
        //    }
        //    ViewBag.Vh2o = OutputViewTextVh2o.Replace('.', ',');
        //    try
        //    {
        //        Vh2o = Convert.ToDecimal(OutputViewTextVh2o.Replace('.', ','));
        //    }
        //    catch { }
        //    try
        //    {
        //        Vh2o = Convert.ToDecimal(OutputViewTextVh2o.Replace(',', '.'));
        //    }
        //    catch { }

        //    // Vr
        //    string urlVr = "api/Formulas/Vr",
        //        routeVr = "";
        //    routeVr += string.IsNullOrEmpty(routeVr) ? "?" : "&";
        //    routeVr += $"Vro2={Vro2.ToString()}".Replace(',', '.');
        //    routeVr += string.IsNullOrEmpty(routeVr) ? "?" : "&";
        //    routeVr += $"Vn2={Vn2.ToString()}".Replace(',', '.');
        //    routeVr += string.IsNullOrEmpty(routeVr) ? "?" : "&";
        //    routeVr += $"Vh2o={Vh2o.ToString()}".Replace(',', '.');
        //    routeVr += string.IsNullOrEmpty(routeVr) ? "?" : "&";
        //    routeVr += $"AFact={AFact.ToString()}".Replace(',', '.');
        //    routeVr += string.IsNullOrEmpty(routeVr) ? "?" : "&";
        //    routeVr += $"V={V.ToString()}".Replace(',', '.');
        //    HttpResponseMessage responseVr = await _HttpApiClient.PostAsync(urlVr + routeVr, null);
        //    string OutputViewTextVr = await responseVr.Content.ReadAsStringAsync();
        //    OutputViewTextVr = OutputViewTextVr.Replace("<br>", Environment.NewLine);
        //    try
        //    {
        //        responseVr.EnsureSuccessStatusCode();
        //    }
        //    catch
        //    {
        //        dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewTextVr);
        //        foreach (JProperty property in errors.Children())
        //        {
        //            //ModelState.AddModelError(property.Name, property.Value[0].ToString());
        //            ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
        //        }
        //        return View();
        //    }
        //    ViewBag.Vr = OutputViewTextVr.Replace('.', ',');
        //    try
        //    {
        //        Vr = Convert.ToDecimal(OutputViewTextVr.Replace('.', ','));
        //    }
        //    catch { }
        //    try
        //    {
        //        Vr = Convert.ToDecimal(OutputViewTextVr.Replace(',', '.'));
        //    }
        //    catch { }

        //    // Vcr
        //    string urlVcr = "api/Formulas/Vcr",
        //        routeVcr = "";
        //    routeVcr += string.IsNullOrEmpty(routeVcr) ? "?" : "&";
        //    routeVcr += $"Vr={Vr.ToString()}".Replace(',', '.');
        //    routeVcr += string.IsNullOrEmpty(routeVcr) ? "?" : "&";
        //    routeVcr += $"Vh2o={Vh2o.ToString()}".Replace(',', '.');
        //    routeVcr += string.IsNullOrEmpty(routeVcr) ? "?" : "&";
        //    routeVcr += $"V={V.ToString()}".Replace(',', '.');
        //    HttpResponseMessage responseVcr = await _HttpApiClient.PostAsync(urlVcr + routeVcr, null);
        //    string OutputViewTextVcr = await responseVcr.Content.ReadAsStringAsync();
        //    OutputViewTextVcr = OutputViewTextVcr.Replace("<br>", Environment.NewLine);
        //    try
        //    {
        //        responseVcr.EnsureSuccessStatusCode();
        //    }
        //    catch
        //    {
        //        dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewTextVcr);
        //        foreach (JProperty property in errors.Children())
        //        {
        //            //ModelState.AddModelError(property.Name, property.Value[0].ToString());
        //            ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
        //        }
        //        return View();
        //    }
        //    ViewBag.Vcr = OutputViewTextVcr.Replace('.', ',');
        //    try
        //    {
        //        Vcr = Convert.ToDecimal(OutputViewTextVcr.Replace('.', ','));
        //    }
        //    catch { }
        //    try
        //    {
        //        Vcr = Convert.ToDecimal(OutputViewTextVcr.Replace(',', '.'));
        //    }
        //    catch { }

        //    // B
        //    string urlB = "api/Formulas/B",
        //        routeB = "";
        //    routeB += string.IsNullOrEmpty(routeB) ? "?" : "&";
        //    routeB += $"Q={Q.ToString()}".Replace(',', '.');
        //    routeB += string.IsNullOrEmpty(routeB) ? "?" : "&";
        //    routeB += $"KPDKA={KPDKA.ToString()}".Replace(',', '.');
        //    routeB += string.IsNullOrEmpty(routeB) ? "?" : "&";
        //    routeB += $"Qri={Qri.ToString()}".Replace(',', '.');
        //    HttpResponseMessage responseB = await _HttpApiClient.PostAsync(urlB + routeB, null);
        //    string OutputViewTextB = await responseB.Content.ReadAsStringAsync();
        //    OutputViewTextB = OutputViewTextB.Replace("<br>", Environment.NewLine);
        //    try
        //    {
        //        responseB.EnsureSuccessStatusCode();
        //    }
        //    catch
        //    {
        //        dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewTextB);
        //        foreach (JProperty property in errors.Children())
        //        {
        //            //ModelState.AddModelError(property.Name, property.Value[0].ToString());
        //            ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
        //        }
        //        return View();
        //    }
        //    ViewBag.B = OutputViewTextB.Replace('.', ',');
        //    try
        //    {
        //        B = Convert.ToDecimal(OutputViewTextB.Replace('.', ','));
        //    }
        //    catch { }
        //    try
        //    {
        //        B = Convert.ToDecimal(OutputViewTextB.Replace(',', '.'));
        //    }
        //    catch { }

        //    // Bp
        //    string urlBp = "api/Formulas/Bp",
        //        routeBp = "";
        //    routeBp += string.IsNullOrEmpty(routeBp) ? "?" : "&";
        //    routeBp += $"Q4={Q4.ToString()}".Replace(',', '.');
        //    routeBp += string.IsNullOrEmpty(routeBp) ? "?" : "&";
        //    routeBp += $"B={B.ToString()}".Replace(',', '.');
        //    HttpResponseMessage responseBp = await _HttpApiClient.PostAsync(urlBp + routeBp, null);
        //    string OutputViewTextBp = await responseBp.Content.ReadAsStringAsync();
        //    OutputViewTextBp = OutputViewTextBp.Replace("<br>", Environment.NewLine);
        //    try
        //    {
        //        responseBp.EnsureSuccessStatusCode();
        //    }
        //    catch
        //    {
        //        dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewTextBp);
        //        foreach (JProperty property in errors.Children())
        //        {
        //            //ModelState.AddModelError(property.Name, property.Value[0].ToString());
        //            ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
        //        }
        //        return View();
        //    }
        //    ViewBag.Bp = OutputViewTextBp.Replace('.', ',');
        //    try
        //    {
        //        Bp = Convert.ToDecimal(OutputViewTextBp.Replace('.', ','));
        //    }
        //    catch { }
        //    try
        //    {
        //        Bp = Convert.ToDecimal(OutputViewTextBp.Replace(',', '.'));
        //    }
        //    catch { }


        //    return View();
        //}

        public IActionResult FormulaHeatPowerThermalPowerPlantsCoal()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaHeatPowerThermalPowerPlantsCoal(decimal Ac,
        decimal Sc,
        decimal Hc,
        decimal Wr,
        decimal Oc,
        decimal Nc,
        decimal Q,
        decimal KPDKA,
        decimal Qri,
        decimal Q4,
        decimal O2,
        decimal CNOx,
        decimal CCO,
        decimal CSO2,
        decimal CTB,
        decimal N)
        {
            ViewBag.Ac = Ac;
            ViewBag.Sc = Sc;
            ViewBag.Hc = Hc;
            ViewBag.Wr = Wr;
            ViewBag.Oc = Oc;
            ViewBag.Nc = Nc;
            ViewBag.Q = Q;
            ViewBag.KPDKA = KPDKA;
            ViewBag.Qri = Qri;
            ViewBag.Q4 = Q4;
            ViewBag.O2 = O2;
            ViewBag.CNOx = CNOx;
            ViewBag.CCO = CCO;
            ViewBag.CSO2 = CSO2;
            ViewBag.CTB = CTB;
            ViewBag.N = N;

            string url = "api/Formulas/HeatPowerThermalPowerPlantsCoal",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ac={Ac.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Sc={Sc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Hc={Hc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Wr={Wr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Oc={Oc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Nc={Nc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Q={Q.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"KPDKA={KPDKA.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qri={Qri.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Q4={Q4.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"O2={O2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CNOx={CNOx.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CCO={CCO.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CSO2={CSO2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CTB={CTB.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"N={N.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            //OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }

            JObject jo = JObject.Parse(OutputViewText);
            ViewBag.ArW = String.Format("{0:0.00}", jo["arW"]);
            ViewBag.SrW = String.Format("{0:0.00}", jo["srW"]);
            ViewBag.HrW = String.Format("{0:0.00}", jo["hrW"]);
            ViewBag.OrW = String.Format("{0:0.00}", jo["orW"]);
            ViewBag.NrW = String.Format("{0:0.00}", jo["nrW"]);
            ViewBag.Cr = String.Format("{0:0.00}", jo["cr"]);
            ViewBag.Vro2 = String.Format("{0:0.00}", jo["vro2"]);
            ViewBag.V = String.Format("{0:0.00}", jo["v"]);
            ViewBag.Vn2 = String.Format("{0:0.00}", jo["vn2"]);
            ViewBag.Vh2o = String.Format("{0:0.00}", jo["vh2o"]);
            ViewBag.Vr = String.Format("{0:0.00}", jo["vr"]);
            ViewBag.Vcr = String.Format("{0:0.00}", jo["vcr"]);
            ViewBag.B = String.Format("{0:0.00}", jo["b"]);
            ViewBag.Bp = String.Format("{0:0.00}", jo["bp"]);
            ViewBag.AFact = String.Format("{0:0.00}", jo["aFact"]);
            ViewBag.CjNOx = String.Format("{0:0.00}", jo["cjNOx"]);
            ViewBag.CjCO = String.Format("{0:0.00}", jo["cjCO"]);
            ViewBag.CjSO2 = String.Format("{0:0.00}", jo["cjSO2"]);
            ViewBag.CjTB = String.Format("{0:0.00}", jo["cjTB"]);
            ViewBag.AAbove = String.Format("{0:0.00}", jo["aAbove"]);
            ViewBag.SAbove = String.Format("{0:0.00}", jo["sAbove"]);
            ViewBag.MNOx = String.Format("{0:0.00}", jo["mnOx"]);
            ViewBag.MNO2 = String.Format("{0:0.00}", jo["mnO2"]);
            ViewBag.MNO = String.Format("{0:0.00}", jo["mno"]);
            ViewBag.MCO = String.Format("{0:0.00}", jo["mco"]);
            ViewBag.MSO2 = String.Format("{0:0.00}", jo["msO2"]);
            ViewBag.MTB = String.Format("{0:0.00}", jo["mtb"]);
            ViewBag.MNOxY = String.Format("{0:0.00}", jo["mnOxY"]);
            ViewBag.MNO2Y = String.Format("{0:0.00}", jo["mnO2Y"]);
            ViewBag.MNOY = String.Format("{0:0.00}", jo["mnoy"]);
            ViewBag.MCOY = String.Format("{0:0.00}", jo["mcoy"]);
            ViewBag.MSO2Y = String.Format("{0:0.00}", jo["msO2Y"]);
            ViewBag.MTBY = String.Format("{0:0.00}", jo["mtby"]);

            return View();
        }

        public IActionResult FormulaHeatPowerThermalPowerPlantsCoalNorm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaHeatPowerThermalPowerPlantsCoalNorm(decimal Ac,
        decimal Sc,
        decimal Hc,
        decimal Wr,
        decimal Oc,
        decimal Nc,
        decimal Q,
        decimal KPDKA,
        decimal Av,
        decimal Noc,
        decimal Qri,
        decimal Q4,
        decimal CNOx,
        decimal CCO,
        decimal CSO2,
        decimal CTB,
        decimal N,
        decimal Df,
        decimal Dn,
        decimal Nzy,
        decimal Z,
        decimal A,
        decimal At,
        decimal Kn)
        {
            ViewBag.Ac = Ac;
            ViewBag.Sc = Sc;
            ViewBag.Hc = Hc;
            ViewBag.Wr = Wr;
            ViewBag.Oc = Oc;
            ViewBag.Nc = Nc;
            ViewBag.Q = Q;
            ViewBag.KPDKA = KPDKA;
            ViewBag.Av = Av;
            ViewBag.Noc = Noc;
            ViewBag.Qri = Qri;
            ViewBag.Q4 = Q4;
            ViewBag.CNOx = CNOx;
            ViewBag.CCO = CCO;
            ViewBag.CSO2 = CSO2;
            ViewBag.CTB = CTB;
            ViewBag.N = N;
            ViewBag.Df = Df;
            ViewBag.Dn = Dn;
            ViewBag.Nzy = Nzy;
            ViewBag.Z = Z;
            ViewBag.A = A;
            ViewBag.At = At;
            ViewBag.Kn = Kn;

            string url = "api/Formulas/HeatPowerThermalPowerPlantsCoalNorm",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ac={Ac.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Sc={Sc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Hc={Hc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Wr={Wr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Oc={Oc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Nc={Nc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Q={Q.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"KPDKA={KPDKA.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Av={Av.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Noc={Noc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qri={Qri.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Q4={Q4.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CNOx={CNOx.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CCO={CCO.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CSO2={CSO2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CTB={CTB.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"N={N.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Df={Df.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dn={Dn.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Nzy={Nzy.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Z={Z.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"A={A.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"At={At.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Kn={Kn.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            //OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }

            JObject jo = JObject.Parse(OutputViewText);
            ViewBag.ArW = String.Format("{0:0.00}", jo["arW"]);
            ViewBag.SrW = String.Format("{0:0.00}", jo["srW"]);
            ViewBag.HrW = String.Format("{0:0.00}", jo["hrW"]);
            ViewBag.OrW = String.Format("{0:0.00}", jo["orW"]);
            ViewBag.NrW = String.Format("{0:0.00}", jo["nrW"]);
            ViewBag.Cr = String.Format("{0:0.00}", jo["cr"]);
            ViewBag.Vro2 = String.Format("{0:0.00}", jo["vro2"]);
            ViewBag.V = String.Format("{0:0.00}", jo["v"]);
            ViewBag.Vn2 = String.Format("{0:0.00}", jo["vn2"]);
            ViewBag.Vh2o = String.Format("{0:0.00}", jo["vh2o"]);
            ViewBag.Vr = String.Format("{0:0.00}", jo["vr"]);
            ViewBag.Vcr = String.Format("{0:0.00}", jo["vcr"]);
            ViewBag.B = String.Format("{0:0.00}", jo["b"]);
            ViewBag.Bp = String.Format("{0:0.00}", jo["bp"]);
            ViewBag.CjNOx = String.Format("{0:0.00}", jo["cjNOx"]);
            ViewBag.CjCO = String.Format("{0:0.00}", jo["cjCO"]);
            ViewBag.CjSO2 = String.Format("{0:0.00}", jo["cjSO2"]);
            ViewBag.CjTB = String.Format("{0:0.00}", jo["cjTB"]);
            ViewBag.Gv = String.Format("{0:0.00}", jo["gv"]);
            ViewBag.Kd = String.Format("{0:0.00}", jo["kd"]);
            ViewBag.Kzy = String.Format("{0:0.00}", jo["kzy"]);
            ViewBag.CjT = String.Format("{0:0.00}", jo["cjT"]);
            ViewBag.AAbove = String.Format("{0:0.00}", jo["aAbove"]);
            ViewBag.SAbove = String.Format("{0:0.00}", jo["sAbove"]);
            ViewBag.MNOx = String.Format("{0:0.00}", jo["mnOx"]);
            ViewBag.MNO2 = String.Format("{0:0.00}", jo["mnO2"]);
            ViewBag.MNO = String.Format("{0:0.00}", jo["mno"]);
            ViewBag.MCO = String.Format("{0:0.00}", jo["mco"]);
            ViewBag.MSO2 = String.Format("{0:0.00}", jo["msO2"]);
            ViewBag.MTB = String.Format("{0:0.00}", jo["mtb"]);
            ViewBag.MNOxY = String.Format("{0:0.00}", jo["mnOxY"]);
            ViewBag.MNO2Y = String.Format("{0:0.00}", jo["mnO2Y"]);
            ViewBag.MNOY = String.Format("{0:0.00}", jo["mnoy"]);
            ViewBag.MCOY = String.Format("{0:0.00}", jo["mcoy"]);
            ViewBag.MSO2Y = String.Format("{0:0.00}", jo["msO2Y"]);
            ViewBag.MTBY = String.Format("{0:0.00}", jo["mtby"]);
            ViewBag.MZMY = String.Format("{0:0.00}", jo["mzmy"]);

            return View();
        }

        public IActionResult FormulaHeatPowerThermalPowerPlantsCoalZ()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaHeatPowerThermalPowerPlantsCoalZ(decimal Ac,
        decimal Sc,
        decimal Hc,
        decimal Wr,
        decimal Oc,
        decimal Nc,
        decimal Q,
        decimal KPDKA,
        decimal Qri,
        decimal Q4,
        decimal O2,
        decimal N,
        decimal INO2,
        decimal INO,
        decimal ICO,
        decimal ISO2)
        {
            ViewBag.Ac = Ac;
            ViewBag.Sc = Sc;
            ViewBag.Hc = Hc;
            ViewBag.Wr = Wr;
            ViewBag.Oc = Oc;
            ViewBag.Nc = Nc;
            ViewBag.Q = Q;
            ViewBag.KPDKA = KPDKA;
            ViewBag.Qri = Qri;
            ViewBag.Q4 = Q4;
            ViewBag.O2 = O2;
            ViewBag.N = N;
            ViewBag.INO2 = INO2;
            ViewBag.INO = INO;
            ViewBag.ICO = ICO;
            ViewBag.ISO2 = ISO2;

            string url = "api/Formulas/HeatPowerThermalPowerPlantsCoalZ",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ac={Ac.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Sc={Sc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Hc={Hc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Wr={Wr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Oc={Oc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Nc={Nc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Q={Q.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"KPDKA={KPDKA.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qri={Qri.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Q4={Q4.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"O2={O2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"N={N.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"INO2={INO2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"INO={INO.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"ICO={ICO.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"ISO2={ISO2.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            //OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }

            JObject jo = JObject.Parse(OutputViewText);
            ViewBag.ArW = String.Format("{0:0.00}", jo["arW"]);
            ViewBag.SrW = String.Format("{0:0.00}", jo["srW"]);
            ViewBag.HrW = String.Format("{0:0.00}", jo["hrW"]);
            ViewBag.OrW = String.Format("{0:0.00}", jo["orW"]);
            ViewBag.NrW = String.Format("{0:0.00}", jo["nrW"]);
            ViewBag.Cr = String.Format("{0:0.00}", jo["cr"]);
            ViewBag.Vro2 = String.Format("{0:0.00}", jo["vro2"]);
            ViewBag.V = String.Format("{0:0.00}", jo["v"]);
            ViewBag.Vn2 = String.Format("{0:0.00}", jo["vn2"]);
            ViewBag.Vh2o = String.Format("{0:0.00}", jo["vh2o"]);
            ViewBag.Vr = String.Format("{0:0.00}", jo["vr"]);
            ViewBag.Vcr = String.Format("{0:0.00}", jo["vcr"]);
            ViewBag.B = String.Format("{0:0.00}", jo["b"]);
            ViewBag.Bp = String.Format("{0:0.00}", jo["bp"]);
            ViewBag.AFact = String.Format("{0:0.00}", jo["aFact"]);
            ViewBag.CjZNO2 = String.Format("{0:0.00}", jo["cjZNO2"]);
            ViewBag.CjZNO = String.Format("{0:0.00}", jo["cjZNO"]);
            ViewBag.CjZCO = String.Format("{0:0.00}", jo["cjZCO"]);
            ViewBag.CjZSO2 = String.Format("{0:0.00}", jo["cjZSO2"]);
            ViewBag.AAbove = String.Format("{0:0.00}", jo["aAbove"]);
            ViewBag.SAbove = String.Format("{0:0.00}", jo["sAbove"]);
            ViewBag.MNO2 = String.Format("{0:0.00}", jo["mnO2"]);
            ViewBag.MNO = String.Format("{0:0.00}", jo["mno"]);
            ViewBag.MCO = String.Format("{0:0.00}", jo["mco"]);
            ViewBag.MSO2 = String.Format("{0:0.00}", jo["msO2"]);
            ViewBag.MNO2Y = String.Format("{0:0.00}", jo["mnO2Y"]);
            ViewBag.MNOY = String.Format("{0:0.00}", jo["mnoy"]);
            ViewBag.MCOY = String.Format("{0:0.00}", jo["mcoy"]);
            ViewBag.MSO2Y = String.Format("{0:0.00}", jo["msO2Y"]);

            return View();
        }

        public async Task<IActionResult> FormulaGasVolumeEmissionValues()
        {
            string url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection5 = new SelectList(gasSectionArray);
            ViewBag.GasDensity5 = gasSection5;

            url = "api/CaloricEquivalents/FuelSelect";
            string[] fuelNameArray = await ListFromRequestToAPI(url);
            var fuelName = new SelectList(fuelNameArray);
            ViewBag.CaloricEquivalent = fuelName;

            url = "api/ParametersEmissionSources/HPAtypeSelect";
            string[] HPAtypeArray = await ListFromRequestToAPI(url);
            var typeHPA = new SelectList(HPAtypeArray);
            ViewBag.HPAtype = typeHPA;

            var gasSection10 = new SelectList(gasSectionArray);
            ViewBag.GasDensity10 = gasSection10;

            var gasSection12 = new SelectList(gasSectionArray);
            ViewBag.GasDensity12 = gasSection12;

            var gasSection13 = new SelectList(gasSectionArray);
            ViewBag.GasDensity13 = gasSection13;

            var gasSection14 = new SelectList(gasSectionArray);
            ViewBag.GasDensity14 = gasSection14;

            ViewBag.Vsr = new decimal[1];
            ViewBag.MiUv = new decimal[1];
            ViewBag.Vi = new decimal[1];
            ViewBag.Ti = new decimal[1];
            ViewBag.KiO = new decimal[1];
            ViewBag.Ni = new decimal[1];
            ViewBag.Vks = new decimal[1];
            ViewBag.VksS = new decimal[1];
            ViewBag.Vtg = new decimal[1];
            ViewBag.Vgdg = new decimal[1];
            ViewBag.Vsa = new decimal[1];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGasVolumeEmissionValues(decimal Do,
            decimal L,
            decimal Pa,
            decimal t0,
            decimal Po,
            decimal tn,
            decimal Zg,
            decimal D,
            decimal Np,
            decimal P,
            decimal t,
            decimal Tk,
            string Ror,
            decimal[] Vsr,
            decimal Pcp,
            decimal Dd,
            decimal Togm,
            decimal D1,
            decimal Bg,
            string Mi,
            decimal Bgs,
            decimal Dgs,
            string E,
            decimal Ki,
            decimal Ch2s,
            decimal Bgf,
            decimal O2dg,
            decimal Ve,
            decimal Vb,
            decimal Bpg,
            decimal[] MiUv,
            decimal[] Vi,
            decimal[] Ti,
            decimal[] KiO,
            decimal[] Ni,
            decimal R,
            decimal Tsvr,
            string RoGvs,
            decimal i1,
            decimal i2,
            decimal Du,
            decimal Fgvs,
            decimal Astrat,
            decimal Tdelta,
            decimal V1gvsov,
            decimal H,
            decimal PDK,
            decimal Eta,
            decimal K,
            string RoRGr,
            decimal VrGr,
            decimal KiMif,
            decimal Vtr,
            decimal Q3,
            decimal Rdpt,
            decimal Qnr,
            decimal Dyx,
            decimal Psi,
            decimal Ups,
            decimal CcoDg,
            decimal Bt,
            decimal Qsnr,
            decimal Yco,
            decimal Nfact,
            decimal Nnom,
            decimal Dfact,
            decimal Dnom,
            decimal Yno2Nom,
            decimal DyxNo2,
            decimal Cno2,
            decimal PsiNo2,
            decimal UpsNo2,
            decimal Bno2,
            decimal QrnNo2,
            decimal Кphg,
            string RoPhgKs,
            decimal VgrPhg,
            decimal[] Vks,
            decimal Qgrs,
            decimal КphgS,
            decimal VphgS,
            decimal MrPhgS,
            decimal[] VksS,
            decimal MsKsS,
            decimal QgrsS,
            decimal[] Vtg,
            decimal MiTg,
            decimal[] Vgdg,
            decimal CiDg,
            decimal KiDg,
            decimal CsSa,
            string RoSa,
            decimal[] Vsa)
        {
            ViewBag.Do = Do;
            ViewBag.L = L;
            ViewBag.Pa = Pa;
            ViewBag.t0 = t0;
            ViewBag.Po = Po;
            ViewBag.tn = tn;
            ViewBag.Zg = Zg;
            ViewBag.D = D;
            ViewBag.Np = Np;
            ViewBag.P = P;
            ViewBag.t = t;
            ViewBag.Tk = Tk;
            ViewBag.Ror = Ror;
            decimal[] VsrWithoutZezro = Vsr.Where(v => v != 0).ToArray();
            ViewBag.Vsr = VsrWithoutZezro;
            ViewBag.Pcp = Pcp;
            ViewBag.Dd = Dd;
            ViewBag.Togm = Togm;
            ViewBag.D1 = D1;
            ViewBag.Bg = Bg;
            ViewBag.Mi = Mi;
            ViewBag.Bgs = Bgs;
            ViewBag.Dgs = Dgs;
            ViewBag.E = E;
            ViewBag.Ki = Ki;
            ViewBag.Ch2s = Ch2s;
            ViewBag.Bgf = Bgf;
            ViewBag.O2dg = O2dg;
            ViewBag.Ve = Ve;
            ViewBag.Vb = Vb;
            ViewBag.Bpg = Bpg;
            decimal[] MiUvWithoutZero = MiUv.Where(v => v != 0).ToArray();
            ViewBag.MiUv = MiUvWithoutZero;
            decimal[] ViWithoutZero = Vi.Where(v => v != 0).ToArray();
            ViewBag.Vi = ViWithoutZero;
            decimal[] TiWithoutZero = Ti.Where(v => v != 0).ToArray();
            ViewBag.Ti = TiWithoutZero;
            decimal[] KiOWithoutZero = KiO.Where(v => v != 0).ToArray();
            ViewBag.KiO = KiOWithoutZero;
            decimal[] NiWithoutZero = Ni.Where(v => v != 0).ToArray();
            ViewBag.Ni = NiWithoutZero;
            ViewBag.R = R;
            ViewBag.Tsvr = Tsvr;
            ViewBag.RoGvs = RoGvs;
            ViewBag.i1 = i1;
            ViewBag.i2 = i2;
            ViewBag.Du = Du;
            ViewBag.Fgvs = Fgvs;
            ViewBag.Astrat = Astrat;
            ViewBag.Tdelta = Tdelta;
            ViewBag.V1gvsov = V1gvsov;
            ViewBag.H = H;
            ViewBag.PDK = PDK;
            ViewBag.Eta = Eta;
            ViewBag.K = K;
            ViewBag.RoRGr = RoRGr;
            ViewBag.VrGr = VrGr;
            ViewBag.KiMif = KiMif;
            ViewBag.Vtr = Vtr;
            ViewBag.Q3 = Q3;
            ViewBag.Rdpt = Rdpt;
            ViewBag.Qnr = Qnr;
            ViewBag.Dyx = Dyx;
            ViewBag.Psi = Psi;
            ViewBag.Ups = Ups;
            ViewBag.CcoDg = CcoDg;
            ViewBag.Bt = Bt;
            ViewBag.Qsnr = Qsnr;
            ViewBag.Yco = Yco;
            ViewBag.Nfact = Nfact;
            ViewBag.Nnom = Nnom;
            ViewBag.Dfact = Dfact;
            ViewBag.Dnom = Dnom;
            ViewBag.Yno2Nom = Yno2Nom;
            ViewBag.DyxNo2 = DyxNo2;
            ViewBag.Cno2 = Cno2;
            ViewBag.PsiNo2 = PsiNo2;
            ViewBag.UpsNo2 = UpsNo2;
            ViewBag.Bno2 = Bno2;
            ViewBag.QrnNo2 = QrnNo2;
            ViewBag.Кphg = Кphg;
            ViewBag.RoPhgKs = RoPhgKs;
            ViewBag.VgrPhg = VgrPhg;
            decimal[] VksWithoutZero = Vks.Where(v => v != 0).ToArray();
            ViewBag.Vks = VksWithoutZero;
            ViewBag.Qgrs = Qgrs;
            ViewBag.КphgS = КphgS;
            ViewBag.VphgS = VphgS;
            ViewBag.MrPhgS = MrPhgS;
            decimal[] VksSWithoutZero = VksS.Where(v => v != 0).ToArray();
            ViewBag.VksS = VksSWithoutZero;
            ViewBag.MsKsS = MsKsS;
            ViewBag.QgrsS = QgrsS;
            decimal[] VtgWithoutZero = Vtg.Where(v => v != 0).ToArray();
            ViewBag.Vtg = VtgWithoutZero;
            ViewBag.MiTg = MiTg;
            decimal[] VgdgWithoutZero = Vgdg.Where(v => v != 0).ToArray();
            ViewBag.Vgdg = VgdgWithoutZero;
            ViewBag.CiDg = CiDg;
            ViewBag.KiDg = KiDg;
            ViewBag.CsSa = CsSa;
            ViewBag.RoSa = RoSa;
            decimal[] VsaWithoutZero = Vsa.Where(v => v != 0).ToArray();
            ViewBag.Vsa = VsaWithoutZero;

            string url = "api/Formulas/GasVolumeEmissionValues",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Do={Do.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"L={L.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pa={Pa.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"t0={t0.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Po={Po.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"tn={tn.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Zg={Zg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"D={D.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Np={Np.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"P={P.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"t={t.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tk={Tk.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ror={Ror.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VsrWithoutZezro.Select(x => "Vsr=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pcp={Pcp.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dd={Dd.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Togm={Togm.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"D1={D1.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bg={Bg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Mi={Mi.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bgs={Bgs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dgs={Dgs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"E={E.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ki={Ki.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ch2s={Ch2s.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bgf={Bgf.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"O2dg={O2dg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ve={Ve.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vb={Vb.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bpg={Bpg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", MiUvWithoutZero.Select(x => "MiUv=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", ViWithoutZero.Select(x => "Vi=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", TiWithoutZero.Select(x => "Ti=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", KiOWithoutZero.Select(x => "KiO=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", NiWithoutZero.Select(x => "Ni=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"R={R.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tsvr={Tsvr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"RoGvs={RoGvs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"i1={i1.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"i2={i2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Du={Du.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Fgvs={Fgvs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Astrat={Astrat.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tdelta={Tdelta.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"V1gvsov={V1gvsov.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"H={H.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"PDK={PDK.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Eta={Eta.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"K={K.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"RoRGr={RoRGr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"VrGr={VrGr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"KiMif={KiMif.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vtr={Vtr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Q3={Q3.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Rdpt={Rdpt.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qnr={Qnr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dyx={Dyx.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Psi={Psi.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ups={Ups.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CcoDg={CcoDg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bt={Bt.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qsnr={Qsnr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Yco={Yco.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Nfact={Nfact.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Nnom={Nnom.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dfact={Dfact.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dnom={Dnom.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Yno2Nom={Yno2Nom.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"DyxNo2={DyxNo2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Cno2={Cno2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"PsiNo2={PsiNo2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"UpsNo2={UpsNo2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bno2={Bno2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"QrnNo2={QrnNo2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Кphg={Кphg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"RoPhgKs={RoPhgKs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"VgrPhg={VgrPhg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VksWithoutZero.Select(x => "Vks=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qgrs={Qgrs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"КphgS={КphgS.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"VphgS={VphgS.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"MrPhgS={MrPhgS.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VksSWithoutZero.Select(x => "VksS=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"MsKsS={MsKsS.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"QgrsS={QgrsS.ToString()}".Replace(',', '.');
            //1
            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VtgWithoutZero.Select(x => "Vtg=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"MiTg={MiTg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VgdgWithoutZero.Select(x => "Vgdg=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CiDg={CiDg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"KiDg={KiDg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CsSa={CsSa.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"RoSa={RoSa.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VsaWithoutZero.Select(x => "Vsa=" + x.ToString().Replace(',', '.')))}";

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            //OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection5 = new SelectList(gasSectionArray, Ror);
            ViewBag.GasDensity5 = gasSection5;

            url = "api/CaloricEquivalents/FuelSelect";
            string[] fuelNameArray = await ListFromRequestToAPI(url);
            var fuelName = new SelectList(fuelNameArray, E);
            ViewBag.CaloricEquivalent = fuelName;

            url = "api/ParametersEmissionSources/HPAtypeSelect";
            string[] HPAtypeArray = await ListFromRequestToAPI(url);
            var typeHPA = new SelectList(HPAtypeArray, Mi);
            ViewBag.HPAtype = typeHPA;

            var gasSection10 = new SelectList(gasSectionArray, RoGvs);
            ViewBag.GasDensity10 = gasSection10;

            var gasSection12 = new SelectList(gasSectionArray, RoRGr);
            ViewBag.GasDensity12 = gasSection12;

            var gasSection13 = new SelectList(gasSectionArray, RoPhgKs);
            ViewBag.GasDensity13 = gasSection13;

            var gasSection14 = new SelectList(gasSectionArray, RoSa);
            ViewBag.GasDensity14 = gasSection14;

            JObject jo = JObject.Parse(OutputViewText);
            ViewBag.Vstr = String.Format("{0:0.00}", jo["vstr"]);
            ViewBag.V1 = String.Format("{0:0.00}", jo["v1"]);
            ViewBag.Vsst = String.Format("{0:0.00}", jo["vsst"]);
            ViewBag.V1Ks = String.Format("{0:0.00}", jo["v1Ks"]);
            ViewBag.G1 = String.Format("{0:0.00}", jo["g1"]);
            ViewBag.Vphg = String.Format("{0:0.00}", jo["vphg"]);
            ViewBag.Vvg = String.Format("{0:0.00}", jo["vvg"]);
            ViewBag.Gi = String.Format("{0:0.00}", jo["gi"]);
            string outputText = Convert.ToString(jo["m"]);
            outputText = outputText.Replace("[", "").Replace("]", "").Replace(" ", "").Replace("\r\n", "");
            string[] M = outputText.Split(new char[] { ',' });
            if (M[0].IndexOf('E') > 0)
            {
                ViewBag.MNOx = M[0];
            }
            else
            {
                ViewBag.MNOx = Convert.ToDecimal(M[0].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }
            if (M[1].IndexOf('E') > 0)
            {
                ViewBag.MCO = M[1];
            }
            else
            {
                ViewBag.MCO = Convert.ToDecimal(M[1].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }
            outputText = Convert.ToString(jo["vgf"]);
            outputText = outputText.Replace("[", "").Replace("]", "").Replace(" ", "").Replace("\r\n", "");
            string[] Vgf = outputText.Split(new char[] { ',' });
            ViewBag.Vdg = jo["vdg"];
            if (Vgf[0].IndexOf('E') > 0)
            {
                ViewBag.VgfCO = Vgf[0];
            }
            else
            {
                ViewBag.VgfCO = Convert.ToDecimal(Vgf[0].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }
            if (Vgf[1].IndexOf('E') > 0)
            {
                ViewBag.VgfCH4 = Vgf[1];
            }
            else
            {
                ViewBag.VgfCH4 = Convert.ToDecimal(Vgf[1].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }
            if (Vgf[2].IndexOf('E') > 0)
            {
                ViewBag.VgfNO2 = Vgf[2];
            }
            else
            {
                ViewBag.VgfNO2 = Convert.ToDecimal(Vgf[2].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }
            ViewBag.Nso2 = jo["nso2"];
            ViewBag.Ddg = String.Format("{0:0.00}", jo["ddg"]);
            ViewBag.V1vp = String.Format("{0:0.00}", jo["v1vp"]);
            ViewBag.Vpg = String.Format("{0:0.00}", jo["vpg"]);
            ViewBag.W = String.Format("{0:0.00}", jo["w"]);
            ViewBag.F = String.Format("{0:0.00}", jo["f"]);
            ViewBag.V1gvs = String.Format("{0:0.00}", jo["v1gvs"]);
            ViewBag.Vgvs = String.Format("{0:0.00}", jo["vgvs"]);
            ViewBag.Fparam = String.Format("{0:0.00}", jo["fparam"]);
            ViewBag.G = String.Format("{0:0.00}", jo["g"]);
            ViewBag.Mgvs = String.Format("{0:0.00}", jo["mgvs"]);
            ViewBag.Um = String.Format("{0:0.00}", jo["um"]);
            ViewBag.Ngvs = String.Format("{0:0.00}", jo["ngvs"]);
            ViewBag.PDV = String.Format("{0:0.00}", jo["pdv"]);
            ViewBag.UmHol = String.Format("{0:0.00}", jo["umHol"]);
            ViewBag.NgvsHol = String.Format("{0:0.00}", jo["ngvsHol"]);
            ViewBag.PDVhol = String.Format("{0:0.00}", jo["pdvhol"]);
            ViewBag.Gr = String.Format("{0:0.00}", jo["gr"]);
            outputText = Convert.ToString(jo["miFact"]);
            outputText = outputText.Replace("[", "").Replace("]", "").Replace(" ", "").Replace("\r\n", "");
            string[] MiFact = outputText.Split(new char[] { ',' });
            ViewBag.MiFactNOx = Convert.ToDecimal(MiFact[0].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            ViewBag.MiFactCO = Convert.ToDecimal(MiFact[1].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            ViewBag.CcoSgt = String.Format("{0:0.00}", jo["ccoSgt"]);
            ViewBag.GwDg = String.Format("{0:0.00}", jo["gwDg"]);
            ViewBag.Gw = String.Format("{0:0.00}", jo["gw"]);
            ViewBag.Yno2 = String.Format("{0:0.00}", jo["yno2"]);
            ViewBag.Gno2 = String.Format("{0:0.00}", jo["gno2"]);
            ViewBag.GrPhg = String.Format("{0:0.00}", jo["grPhg"]);
            ViewBag.GrKs = String.Format("{0:0.00}", jo["grKs"]);
            ViewBag.GrGrs = String.Format("{0:0.00}", jo["grGrs"]);
            ViewBag.GrPhgS = String.Format("{0:0.00}", jo["grPhgS"]);
            ViewBag.GrKsS = String.Format("{0:0.00}", jo["grKsS"]);
            ViewBag.GrGrsS = String.Format("{0:0.00}", jo["grGrsS"]);
            outputText = Convert.ToString(jo["gdg"]);
            outputText = outputText.Replace("[", "").Replace("]", "").Replace(" ", "").Replace("\r\n", "");
            string[] Gdg = outputText.Split(new char[] { ',' });
            if (CiDg == 0)
            {
                ViewBag.Gtg = jo["gtg"];
                if (Gdg[0].IndexOf('E') > 0)
                {
                    ViewBag.GdgNOX = Gdg[0];
                }
                else
                {
                    ViewBag.GdgNOX = Convert.ToDecimal(Gdg[0].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                }
                if (Gdg[1].IndexOf('E') > 0)
                {
                    ViewBag.GdgCO = Gdg[1];
                }
                else
                {
                    ViewBag.GdgCO = Convert.ToDecimal(Gdg[1].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                }
                ViewBag.Gsa = jo["gsa"];
            }
            else
            {
                ViewBag.Gtg = jo["gtg"];
                if (Gdg[0].IndexOf('E') > 0)
                {
                    ViewBag.Gdg = Gdg[0];
                }
                else
                {
                    ViewBag.Gdg = Convert.ToDecimal(Gdg[0].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                }
                ViewBag.Gsa = jo["gsa"];
            }
            return View();
        }

        public IActionResult FormulaArW()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaArW(decimal Ac,
            decimal Wr)
        {
            ViewBag.Ac = Ac;
            ViewBag.Wr = Wr;

            string url = "api/Formulas/ArW",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ac={Ac.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Wr={Wr.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);                
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.ArW = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaFuelTransfer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaFuelTransfer(string givenMass,
            string targetMass,
            decimal Acg2t3, 
            decimal Acg3t2,
            decimal Wrg1t2, 
            decimal Wrg1t3, 
            decimal Wrg2t1, 
            decimal Wrg2t3,
            decimal Wrg3t1, 
            decimal Wrg3t2,
            decimal ArWg1t3, 
            decimal ArWg3t1)
        {
            ViewBag.givenMass = givenMass;
            ViewBag.targetMass = targetMass;
            ViewBag.Acg2t3 = Acg2t3;
            ViewBag.Acg3t2 = Acg3t2;
            ViewBag.Wrg1t2 = Wrg1t2;
            ViewBag.Wrg1t3 = Wrg1t3;
            ViewBag.Wrg2t1 = Wrg2t1;
            ViewBag.Wrg2t3 = Wrg2t3;
            ViewBag.Wrg3t1 = Wrg3t1;
            ViewBag.Wrg3t2 = Wrg3t2;
            ViewBag.ArWg1t3 = ArWg1t3;
            ViewBag.ArWg3t1 = ArWg3t1;
            
            string url = "",
                route = "";

            if ((givenMass == "g1" && targetMass == "t1") || (givenMass == "g2" && targetMass == "t2") || (givenMass == "g3" && targetMass == "t3"))
            {
                url = "api/Formulas/RabSuh";
                route = "";

                Wrg1t2 = 0.00M;
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Wrg1t2={Wrg1t2.ToString()}".Replace(',', '.');
            }

            if (givenMass == "g1" && targetMass == "t2")
            {
                url = "api/Formulas/RabSuh";
                route = "";

                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Wrg1t2={Wrg1t2.ToString()}".Replace(',', '.');
            }

            if (givenMass == "g1" && targetMass == "t3")
            {
                url = "api/Formulas/RabGor";
                route = "";

                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"ArWg1t3={ArWg1t3.ToString()}".Replace(',', '.');

                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Wrg1t3={Wrg1t3.ToString()}".Replace(',', '.');
            }

            if (givenMass == "g2" && targetMass == "t1")
            {
                url = "api/Formulas/SuhRab";
                route = "";

                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Wrg2t1={Wrg2t1.ToString()}".Replace(',', '.');
            }

            if (givenMass == "g2" && targetMass == "t3")
            {
                url = "api/Formulas/SuhGor";
                route = "";

                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Acg2t3={Acg2t3.ToString()}".Replace(',', '.');

                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Wrg2t3={Wrg2t3.ToString()}".Replace(',', '.');
            }

            if (givenMass == "g3" && targetMass == "t1")
            {
                url = "api/Formulas/GorRab";
                route = "";

                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"ArWg3t1={ArWg3t1.ToString()}".Replace(',', '.');

                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Wrg3t1={Wrg3t1.ToString()}".Replace(',', '.');
            }

            if (givenMass == "g3" && targetMass == "t2")
            {
                url = "api/Formulas/GorSuh";
                route = "";

                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Acg3t2={Acg3t2.ToString()}".Replace(',', '.');

                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Wrg3t2={Wrg3t2.ToString()}".Replace(',', '.');
            }

            else
            {
                url = "api/Formulas/RabSuh";
                route = "";

                Wrg1t2 = 0.00M;
                route += string.IsNullOrEmpty(route) ? "?" : "&";
                route += $"Wrg1t2={Wrg1t2.ToString()}".Replace(',', '.');
            }

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.FuelTransfer = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaSrW()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaSrW(decimal Sc,
            decimal Wr)
        {
            ViewBag.Sc = Sc;
            ViewBag.Wr = Wr;

            string url = "api/Formulas/SrW",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Sc={Sc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Wr={Wr.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.SrW = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaHrW()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaHrW(decimal Hc,
            decimal Wr)
        {
            ViewBag.Hc = Hc;
            ViewBag.Wr = Wr;

            string url = "api/Formulas/HrW",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Hc={Hc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Wr={Wr.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.HrW = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaOrW()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaOrW(decimal Oc,
            decimal Wr)
        {
            ViewBag.Oc = Oc;
            ViewBag.Wr = Wr;

            string url = "api/Formulas/OrW",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Oc={Oc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Wr={Wr.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.OrW = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaNrW()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaNrW(decimal Nc,
            decimal Wr)
        {
            ViewBag.Nc = Nc;
            ViewBag.Wr = Wr;

            string url = "api/Formulas/NrW",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Nc={Nc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Wr={Wr.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.NrW = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaCr()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaCr(decimal ArW,
            decimal SrW,
            decimal HrW,
            decimal OrW,
            decimal NrW,
            decimal Wr)
        {
            ViewBag.ArW = ArW;
            ViewBag.SrW = SrW;
            ViewBag.HrW = HrW;
            ViewBag.OrW = OrW;
            ViewBag.NrW = NrW;
            ViewBag.Wr = Wr;

            string url = "api/Formulas/Cr",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"ArW={ArW.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"SrW={SrW.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"HrW={HrW.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"OrW={OrW.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"NrW={NrW.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Wr={Wr.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.Cr = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaVro2()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaVro2(decimal Cr,
            decimal SrW)
        {
            ViewBag.Cr = Cr;
            ViewBag.SrW = SrW;

            string url = "api/Formulas/Vro2",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Cr={Cr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"SrW={SrW.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.Vro2 = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaV()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaV(decimal Cr,
            decimal SrW,
            decimal HrW,
            decimal OrW)
        {
            ViewBag.Cr = Cr;
            ViewBag.SrW = SrW;
            ViewBag.HrW = HrW;
            ViewBag.OrW = OrW;

            string url = "api/Formulas/V",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Cr={Cr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"SrW={SrW.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"HrW={HrW.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"OrW={OrW.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.V = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaVn2()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaVn2(decimal V,
            decimal NrW)
        {
            ViewBag.V = V;
            ViewBag.NrW = NrW;

            string url = "api/Formulas/Vn2",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"V={V.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"NrW={NrW.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.Vn2 = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaVh2o()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaVh2o(decimal HrW,
            decimal Wr,
            decimal V)
        {
            ViewBag.HrW = HrW;
            ViewBag.Wr = Wr;
            ViewBag.V = V;

            string url = "api/Formulas/Vh2o",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"HrW={HrW.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Wr={Wr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"V={V.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.Vh2o = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaVr()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaVr(decimal Vro2,
            decimal Vn2,
            decimal Vh2o)
        {
            ViewBag.Vro2 = Vro2;
            ViewBag.Vn2 = Vn2;
            ViewBag.Vh2o = Vh2o;

            string url = "api/Formulas/Vr",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vro2={Vro2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vn2={Vn2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vh2o={Vh2o.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.Vr = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaVcr()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaVcr(decimal Vr,
            decimal Vh2o,
            decimal V)
        {
            ViewBag.Vr = Vr;
            ViewBag.Vh2o = Vh2o;
            ViewBag.V = V;

            string url = "api/Formulas/Vcr",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vr={Vr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vh2o={Vh2o.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"V={V.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.Vcr = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaB()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaB(decimal Q,
            decimal KPDKA,
            decimal Qri)
        {
            ViewBag.Q = Q;
            ViewBag.KPDKA = KPDKA;
            ViewBag.Qri = Qri;

            string url = "api/Formulas/B",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Q={Q.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"KPDKA={KPDKA.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qri={Qri.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.B = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaBp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaBp(decimal Q4,
            decimal B)
        {
            ViewBag.Q4 = Q4;
            ViewBag.B = B;

            string url = "api/Formulas/Bp",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Q4={Q4.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"B={B.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.Bp = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaAFact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaAFact(decimal O2)
        {
            ViewBag.O2 = O2;

            string url = "api/Formulas/AFact",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"O2={O2.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.AFact = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaCjNOx()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaCjNOx(decimal CNOx,
            decimal AFact)
        {
            ViewBag.CNOx = CNOx;
            ViewBag.AFact = AFact;

            string url = "api/Formulas/CjNOx",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CNOx={CNOx.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"AFact={AFact.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.CjNOx = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaCjCO()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaCjCO(decimal CCO,
            decimal AFact)
        {
            ViewBag.CCO = CCO;
            ViewBag.AFact = AFact;

            string url = "api/Formulas/CjCO",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CCO={CCO.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"AFact={AFact.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.CjCO = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaCjSO2()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaCjSO2(decimal CSO2,
            decimal AFact)
        {
            ViewBag.CSO2 = CSO2;
            ViewBag.AFact = AFact;

            string url = "api/Formulas/CjSO2",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CSO2={CSO2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"AFact={AFact.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.CjSO2 = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaCjTB()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaCjTB(decimal CTB,
            decimal AFact)
        {
            ViewBag.CTB = CTB;
            ViewBag.AFact = AFact;

            string url = "api/Formulas/CjTB",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CTB={CTB.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"AFact={AFact.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.CjTB = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaKd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaKd(decimal Df,
            decimal Dn)
        {
            ViewBag.Df = Df;
            ViewBag.Dn = Dn;

            string url = "api/Formulas/Kd",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Df={Df.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dn={Dn.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.Kd = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaKzy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaKzy(decimal Nzy,
            decimal Z)
        {
            ViewBag.Nzy = Nzy;
            ViewBag.Z = Z;

            string url = "api/Formulas/Kzy",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Nzy={Nzy.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Z={Z.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.Kzy = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaCjT()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaCjT(decimal Df,
            decimal Dn,
            decimal Nzy,
            decimal Z,
            decimal A,
            decimal Qri,
            decimal At)
        {
            ViewBag.Df = Df;
            ViewBag.Dn = Dn;
            ViewBag.Nzy = Nzy;
            ViewBag.Z = Z;
            ViewBag.A = A;
            ViewBag.Qri = Qri;
            ViewBag.At = At;

            string url = "api/Formulas/CjT",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Df={Df.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dn={Dn.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Nzy={Nzy.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Z={Z.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"A={A.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qri={Qri.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"At={At.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.CjT = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaAAbove()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaAAbove(decimal ArW,
            decimal Qri)
        {
            ViewBag.ArW = ArW;
            ViewBag.Qri = Qri;

            string url = "api/Formulas/AAbove",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"ArW={ArW.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qri={Qri.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.AAbove = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaSAbove()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaSAbove(decimal SrW,
            decimal Qri)
        {
            ViewBag.SrW = SrW;
            ViewBag.Qri = Qri;

            string url = "api/Formulas/SAbove",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"SrW={SrW.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qri={Qri.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.SAbove = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaMNOx()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaMNOx(decimal CjNOx,
            decimal Vcr,
            decimal Bp)
        {
            ViewBag.CjNOx = CjNOx;
            ViewBag.Vcr = Vcr;
            ViewBag.Bp = Bp;

            string url = "api/Formulas/MNOx",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CjNOx={CjNOx.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vcr={Vcr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bp={Bp.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.MNOx = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaMNO2()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaMNO2(decimal MNOx)
        {
            ViewBag.MNOx = MNOx;

            string url = "api/Formulas/MNO2",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"MNOx={MNOx.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.MNO2 = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaMNO()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaMNO(decimal MNOx)
        {
            ViewBag.MNOx = MNOx;

            string url = "api/Formulas/MNO",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"MNOx={MNOx.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.MNO = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaMCO()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaMCO(decimal CjCO,
            decimal Vcr,
            decimal Bp)
        {
            ViewBag.CjCO = CjCO;
            ViewBag.Vcr = Vcr;
            ViewBag.Bp = Bp;

            string url = "api/Formulas/MCO",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CjCO={CjCO.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vcr={Vcr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bp={Bp.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.MCO = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaMSO2()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaMSO2(decimal CjSO2,
            decimal Vcr,
            decimal Bp)
        {
            ViewBag.CjSO2 = CjSO2;
            ViewBag.Vcr = Vcr;
            ViewBag.Bp = Bp;

            string url = "api/Formulas/MSO2",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CjSO2={CjSO2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vcr={Vcr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bp={Bp.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.MSO2 = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaMTB()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaMTB(decimal CjTB,
            decimal Vcr,
            decimal Bp)
        {
            ViewBag.CjTB = CjTB;
            ViewBag.Vcr = Vcr;
            ViewBag.Bp = Bp;

            string url = "api/Formulas/MTB",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CjTB={CjTB.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vcr={Vcr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bp={Bp.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.MTB = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaMNOxY()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaMNOxY(decimal CjNOx,
            decimal Vcr,
            decimal Bp,
            decimal N)
        {
            ViewBag.CjNOx = CjNOx;
            ViewBag.Vcr = Vcr;
            ViewBag.Bp = Bp;
            ViewBag.N = N;

            string url = "api/Formulas/MNOxY",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CjNOx={CjNOx.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vcr={Vcr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bp={Bp.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"N={N.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.MNOxY = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaMNO2Y()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaMNO2Y(decimal MNOxY)
        {
            ViewBag.MNOxY = MNOxY;

            string url = "api/Formulas/MNO2Y",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"MNOxY={MNOxY.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.MNO2Y = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaMNOY()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaMNOY(decimal MNOxY)
        {
            ViewBag.MNOxY = MNOxY;

            string url = "api/Formulas/MNOY",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"MNOxY={MNOxY.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.MNOY = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaMCOY()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaMCOY(decimal CjCO,
            decimal Vcr,
            decimal Bp,
            decimal N)
        {
            ViewBag.CjCO = CjCO;
            ViewBag.Vcr = Vcr;
            ViewBag.Bp = Bp;
            ViewBag.N = N;

            string url = "api/Formulas/MCOY",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CjCO={CjCO.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vcr={Vcr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bp={Bp.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"N={N.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.MCOY = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaMSO2Y()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaMSO2Y(decimal CjSO2,
            decimal Vcr,
            decimal Bp,
            decimal N)
        {
            ViewBag.CjSO2 = CjSO2;
            ViewBag.Vcr = Vcr;
            ViewBag.Bp = Bp;
            ViewBag.N = N;

            string url = "api/Formulas/MSO2Y",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CjSO2={CjSO2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vcr={Vcr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bp={Bp.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"N={N.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.MSO2Y = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaMTBY()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaMTBY(decimal CjTB,
            decimal Vcr,
            decimal Bp,
            decimal N)
        {
            ViewBag.CjTB = CjTB;
            ViewBag.Vcr = Vcr;
            ViewBag.Bp = Bp;
            ViewBag.N = N;

            string url = "api/Formulas/MTBY",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CjTB={CjTB.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vcr={Vcr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bp={Bp.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"N={N.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.MTBY = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        public IActionResult FormulaMZMY()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaMZMY(decimal Av,
            decimal Ac,
            decimal Bp,
            decimal Noc,
            decimal Kn)
        {
            ViewBag.Av = Av;
            ViewBag.Ac = Ac;
            ViewBag.Bp = Bp;
            ViewBag.Noc = Noc;
            ViewBag.Kn = Kn;

            string url = "api/Formulas/MZMY",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Av={Av.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ac={Ac.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bp={Bp.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Noc={Noc.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Kn={Kn.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            ViewBag.MZMY = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            return View();
        }

        //
        //------------------------------------------------------ГАЗ_1 (272)----------------------------------------------------------------
        //

        public async Task<IActionResult> FormulaGAS1()
        {
            string url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection = new SelectList(gasSectionArray);
            ViewBag.GasDensity = gasSection;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGAS1(decimal Do,
            decimal L,
            decimal Pa,
            decimal t0,
            decimal Po,
            decimal tn,
            decimal Pz,
            decimal Delta,
            decimal Tz,
            decimal D,
            decimal Np,
            decimal P,
            decimal t,
            decimal Tk,
            string Ror)
        {
            ViewBag.Do = Do;
            ViewBag.L = L;
            ViewBag.Pa = Pa;
            ViewBag.t0 = t0;
            ViewBag.Po = Po;
            ViewBag.tn = tn;
            ViewBag.Pz = Pz;
            ViewBag.Delta = Delta;
            ViewBag.Tz = Tz;
            ViewBag.D = D;
            ViewBag.Np = Np;
            ViewBag.P = P;
            ViewBag.t = t;
            ViewBag.Tk = Tk;
            ViewBag.Ror = Ror;

            string url = "api/Formulas/GAS1",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Do={Do.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"L={L.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pa={Pa.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"t0={t0.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Po={Po.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"tn={tn.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pz={Pz.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Delta={Delta.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tz={Tz.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"D={D.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Np={Np.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"P={P.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"t={t.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tk={Tk.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ror={Ror.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            //OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }

            url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection = new SelectList(gasSectionArray, Ror);
            ViewBag.GasDensity = gasSection;

            JObject jo = JObject.Parse(OutputViewText);
            ViewBag.Ss = jo["ss"];
            ViewBag.Vk = jo["vk"];
            ViewBag.Vstr = jo["vstr"];
            ViewBag.V1 = jo["v1"];

            return View();
        }

        public async Task<IActionResult> FormulaGAS1nonSSVk()
        {
            string url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection = new SelectList(gasSectionArray);
            ViewBag.GasDensity = gasSection;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGAS1nonSSVk(decimal Do,
            decimal L,
            decimal Pa,
            decimal t0,
            decimal Po,
            decimal tn,
            decimal Pz,
            decimal Delta,
            decimal Tz,
            decimal D,
            decimal Np,
            decimal P,
            decimal t,
            decimal Tk,
            string Ror)
        {
            ViewBag.Do = Do;
            ViewBag.L = L;
            ViewBag.Pa = Pa;
            ViewBag.t0 = t0;
            ViewBag.Po = Po;
            ViewBag.tn = tn;
            ViewBag.Pz = Pz;
            ViewBag.Delta = Delta;
            ViewBag.Tz = Tz;
            ViewBag.D = D;
            ViewBag.Np = Np;
            ViewBag.P = P;
            ViewBag.t = t;
            ViewBag.Tk = Tk;
            ViewBag.Ror = Ror;

            string url = "api/Formulas/GAS1",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Do={Do.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"L={L.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pa={Pa.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"t0={t0.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Po={Po.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"tn={tn.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pz={Pz.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Delta={Delta.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tz={Tz.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"D={D.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Np={Np.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"P={P.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"t={t.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tk={Tk.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ror={Ror.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            //OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection = new SelectList(gasSectionArray, Ror);
            ViewBag.GasDensity = gasSection;

            JObject jo = JObject.Parse(OutputViewText);
            ViewBag.Vstr = jo["vstr"];
            ViewBag.V1 = jo["v1"];

            return View();
        }

        public IActionResult FormulaSs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaSs(decimal Do)
        {
            ViewBag.Do = Do;

            string url = "api/Formulas/Ss",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Do={Do.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Ss = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Ss = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaVk()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaVk(decimal Ss,
            decimal L)
        {
            ViewBag.Ss = Ss;
            ViewBag.L = L;

            string url = "api/Formulas/Vk",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ss={Ss.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"L={L.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Vk = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Vk = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaVstr()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaVstr(decimal Vk,
            decimal Pa,
            decimal t0,
            decimal Po,
            decimal tn,
            decimal Pz,
            decimal Delta,
            decimal Tz)
        {
            ViewBag.Vk = Vk;
            ViewBag.Pa = Pa;
            ViewBag.t0 = t0;
            ViewBag.Po = Po;
            ViewBag.tn = tn;
            ViewBag.Pz = Pz;
            ViewBag.Delta = Delta;
            ViewBag.Tz = Tz;

            string url = "api/Formulas/Vstr",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vk={Vk.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pa={Pa.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"t0={t0.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Po={Po.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"tn={tn.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pz={Pz.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Delta={Delta.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tz={Tz.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Vstr = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Vstr = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public async Task<IActionResult> FormulaV1()
        {
            string url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection = new SelectList(gasSectionArray);
            ViewBag.GasDensity = gasSection;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaV1(decimal D,
            decimal Np,
            decimal P,
            decimal t,
            decimal Tk,
            string Ror)
        {
            ViewBag.D = D;
            ViewBag.Np = Np;
            ViewBag.P = P;
            ViewBag.t = t;
            ViewBag.Tk = Tk;
            ViewBag.Ror = Ror;

            string url = "api/Formulas/V1",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"D={D.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Np={Np.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"P={P.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"t={t.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tk={Tk.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ror={Ror.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.V1 = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection = new SelectList(gasSectionArray, Ror);
            ViewBag.GasDensity = gasSection;

            ViewBag.V1 = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        //
        //------------------------------------------------------ГАЗ_2 (273)----------------------------------------------------------------
        //

        public async Task<IActionResult> FormulaGAS2()
        {
            string url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection = new SelectList(gasSectionArray);
            ViewBag.GasDensity = gasSection;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGAS2(decimal Do,
            decimal L,
            decimal Pa,
            decimal t0,
            decimal Po,
            decimal tn,
            decimal Pz,
            decimal Delta,
            decimal Tz,
            decimal Pcp,
            decimal Dd,
            decimal Togm,
            string Ror)
        {
            ViewBag.Do = Do;
            ViewBag.L = L;
            ViewBag.Pa = Pa;
            ViewBag.t0 = t0;
            ViewBag.Po = Po;
            ViewBag.tn = tn;
            ViewBag.Pz = Pz;
            ViewBag.Delta = Delta;
            ViewBag.Tz = Tz;
            ViewBag.Pcp = Pcp;
            ViewBag.Dd = Dd;
            ViewBag.Togm = Togm;
            ViewBag.Ror = Ror;

            string url = "api/Formulas/GAS2",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Do={Do.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"L={L.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pa={Pa.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"t0={t0.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Po={Po.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"tn={tn.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pz={Pz.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Delta={Delta.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tz={Tz.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pcp={Pcp.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dd={Dd.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Togm={Togm.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ror={Ror.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            //OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }

            url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection = new SelectList(gasSectionArray, Ror);
            ViewBag.GasDensity = gasSection;

            JObject jo = JObject.Parse(OutputViewText);
            ViewBag.Vsst = jo["vsst"];
            ViewBag.V1Ks = jo["v1Ks"];
            ViewBag.G1 = jo["g1"];

            return View();
        }

        public IActionResult FormulaVsst()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaVsst(decimal Vk,
            decimal Pa,
            decimal t0,
            decimal Po,
            decimal tn,
            decimal Pz,
            decimal Delta,
            decimal Tz)
        {
            ViewBag.Vk = Vk;
            ViewBag.Pa = Pa;
            ViewBag.t0 = t0;
            ViewBag.Po = Po;
            ViewBag.tn = tn;
            ViewBag.Pz = Pz;
            ViewBag.Delta = Delta;
            ViewBag.Tz = Tz;

            string url = "api/Formulas/Vsst",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vk={Vk.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pa={Pa.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"t0={t0.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Po={Po.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"tn={tn.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pz={Pz.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Delta={Delta.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tz={Tz.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Vsst = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Vsst = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaV1Ks()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaV1Ks(decimal Vsst,
            decimal Pcp,
            decimal Dd,
            decimal L,
            decimal Togm)
        {
            ViewBag.Vsst = Vsst;
            ViewBag.Pcp = Pcp;
            ViewBag.Dd = Dd;
            ViewBag.L = L;
            ViewBag.Togm = Togm;

            string url = "api/Formulas/V1Ks",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vsst={Vsst.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pcp={Pcp.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dd={Dd.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"L={L.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Togm={Togm.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.V1Ks = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.V1Ks = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public async Task<IActionResult> FormulaG1()
        {
            string url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection = new SelectList(gasSectionArray);
            ViewBag.GasDensity = gasSection;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaG1(string Ror,
            decimal V1Ks)
        {
            ViewBag.Ror = Ror;
            ViewBag.V1Ks = V1Ks;

            string url = "api/Formulas/G1",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ror={Ror.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"V1Ks={V1Ks.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.G1 = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));

            url = "api/GasDensities/GasSelect";

            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection = new SelectList(gasSectionArray, Ror);

            ViewBag.GasDensity = gasSection;
            ViewBag.G1 = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        //
        //------------------------------------------------------ГАЗ_3 (286)----------------------------------------------------------------
        //

        public IActionResult FormulaVphg()
        {
            ViewBag.Vsr = new decimal[1];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaVphg(decimal[] Vsr)
        {
            decimal[] VsrWithoutZero = Vsr.Where(v => v != 0).ToArray();
            ViewBag.Vsr = VsrWithoutZero;

            string url = "api/Formulas/Vphg",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VsrWithoutZero.Select(x => "Vsr=" + x.ToString().Replace(',', '.')))}";

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Vphg = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Vphg = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        //
        //------------------------------------------------------ГАЗ_4 (287)----------------------------------------------------------------
        //

        public async Task<IActionResult> FormulaGAS4()
        {
            string url = "api/ParametersEmissionSources/HPAtypeSelect";
            string[] HPAtypeArray = await ListFromRequestToAPI(url);
            var typeHPA = new SelectList(HPAtypeArray);
            ViewBag.HPAtype = typeHPA;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGAS4(decimal D1,
            decimal Bg,
            string Mi)
        {
            ViewBag.D1 = D1;
            ViewBag.Bg = Bg;
            ViewBag.Mi = Mi;

            string url = "api/Formulas/GAS4",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"D1={D1.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bg={Bg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Mi={Mi.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            //OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            url = "api/ParametersEmissionSources/HPAtypeSelect";
            string[] HPAtypeArray = await ListFromRequestToAPI(url);
            var typeHPA = new SelectList(HPAtypeArray, Mi);
            ViewBag.HPAtype = typeHPA;

            JObject jo = JObject.Parse(OutputViewText);
            ViewBag.Vvg = jo["vvg"];

            string outputText = Convert.ToString(jo["m"]);
            outputText = outputText.Replace("[", "").Replace("]", "").Replace(" ", "").Replace("\r\n", "");
            string[] M = outputText.Split(new char[] { ',' });
            if (M[0].IndexOf('E') > 0)
            {
                ViewBag.MNOx = M[0];
            }
            else
            {
                ViewBag.MNOx = Convert.ToDecimal(M[0].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }
            if (M[1].IndexOf('E') > 0)
            {
                ViewBag.MCO = M[1];
            }
            else
            {
                ViewBag.MCO = Convert.ToDecimal(M[1].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }

            return View();
        }

        public IActionResult FormulaVvg()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaVvg(decimal D1,
            decimal Bg)
        {
            ViewBag.D1 = D1;
            ViewBag.Bg = Bg;

            string url = "api/Formulas/Vvg",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"D1={D1.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bg={Bg.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Vvg = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Vvg = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public async Task<IActionResult> FormulaM()
        {
            string url = "api/ParametersEmissionSources/HPAtypeSelect";
            string[] HPAtypeArray = await ListFromRequestToAPI(url);
            var typeHPA = new SelectList(HPAtypeArray);
            ViewBag.HPAtype = typeHPA;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaM(string Mi,
            decimal Vvg)
        {
            ViewBag.Mi = Mi;
            ViewBag.Vvg = Vvg;

            string url = "api/Formulas/M",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Mi={Mi.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vvg={Vvg.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.M = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            //ViewBag.M = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            url = "api/ParametersEmissionSources/HPAtypeSelect";
            string[] HPAtypeArray = await ListFromRequestToAPI(url);
            var typeHPA = new SelectList(HPAtypeArray, Mi);
            ViewBag.HPAtype = typeHPA;

            OutputViewText = OutputViewText.Replace("[", "").Replace("]", "");
            string[] M = OutputViewText.Split(new char[] { ',' });
            ViewBag.MNOx = Convert.ToDecimal(M[0].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            ViewBag.MCO = Convert.ToDecimal(M[1].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        //
        //------------------------------------------------------ГАЗ_5 (288)----------------------------------------------------------------
        //

        public async Task<IActionResult> FormulaGAS5()
        {
            string url = "api/CaloricEquivalents/FuelSelect";
            string[] fuelNameArray = await ListFromRequestToAPI(url);
            var fuelName = new SelectList(fuelNameArray);
            ViewBag.CaloricEquivalent = fuelName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGAS5(decimal Bgs,
            decimal Dgs,
            string E,
            decimal Ki,
            decimal Bgf,
            decimal Ch2s)
        {
            ViewBag.Bgs = Bgs;
            ViewBag.Dgs = Dgs;
            ViewBag.E = E;
            ViewBag.Ki = Ki;
            ViewBag.Bgf = Bgf;
            ViewBag.Ch2s = Ch2s;

            string url = "api/Formulas/GAS5",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bgs={Bgs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dgs={Dgs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"E={E.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ki={Ki.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bgf={Bgf.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ch2s={Ch2s.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            //OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }

            url = "api/CaloricEquivalents/FuelSelect";
            string[] fuelNameArray = await ListFromRequestToAPI(url);
            var fuelName = new SelectList(fuelNameArray, E);
            ViewBag.CaloricEquivalent = fuelName;

            JObject jo = JObject.Parse(OutputViewText);
            string outputText = Convert.ToString(jo["vgf"]);
            outputText = outputText.Replace("[", "").Replace("]", "").Replace(" ", "").Replace("\r\n", "");
            string[] Vgf = outputText.Split(new char[] { ',' });
            ViewBag.Vdg = jo["vdg"];
            if (Vgf[0].IndexOf('E') > 0)
            {
                ViewBag.VgfCO = Vgf[0];
            }
            else
            {
                ViewBag.VgfCO = Convert.ToDecimal(Vgf[0].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }
            if (Vgf[1].IndexOf('E') > 0)
            {
                ViewBag.VgfCH4 = Vgf[1];
            }
            else
            {
                ViewBag.VgfCH4 = Convert.ToDecimal(Vgf[1].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }
            if (Vgf[2].IndexOf('E') > 0)
            {
                ViewBag.VgfNO2 = Vgf[2];
            }
            else
            {
                ViewBag.VgfNO2 = Convert.ToDecimal(Vgf[2].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }
            ViewBag.Nso2 = jo["nso2"];
            return View();
        }

        public async Task<IActionResult> FormulaVdg()
        {
            string url = "api/CaloricEquivalents/FuelSelect";
            string[] fuelNameArray = await ListFromRequestToAPI(url);
            var fuelName = new SelectList(fuelNameArray);
            ViewBag.CaloricEquivalent = fuelName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaVdg(decimal Bgs,
            decimal Dgs,
            string E)
        {
            ViewBag.Bgs = Bgs;
            ViewBag.Dgs = Dgs;
            ViewBag.E = E;

            string url = "api/Formulas/Vdg",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bgs={Bgs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dgs={Dgs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"E={E.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            url = "api/CaloricEquivalents/FuelSelect";
            string[] fuelNameArray = await ListFromRequestToAPI(url);
            var fuelName = new SelectList(fuelNameArray, E);
            ViewBag.CaloricEquivalent = fuelName;

            //ViewBag.Vdg = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Vdg = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaVgf()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaVgf(decimal Ki,
            decimal Bgf)
        {
            ViewBag.Ki = Ki;
            ViewBag.Bgf = Bgf;

            string url = "api/Formulas/Vgf",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ki={Ki.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bgf={Bgf.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Vdg = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            //ViewBag.Vgf = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            //ViewBag.VgfCO = OutputViewText.Substring(',');
            //ViewBag.VgfCH4 = OutputViewText.Substring(',');
            //ViewBag.VgfNO2 = OutputViewText.Substring(',');
            OutputViewText = OutputViewText.Replace("[", "").Replace("]", "");
            string[] Vgf = OutputViewText.Split(new char[] {','});
            ViewBag.VgfCO = Convert.ToDecimal(Vgf[0].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            ViewBag.VgfCH4 = Convert.ToDecimal(Vgf[1].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            ViewBag.VgfNO2 = Convert.ToDecimal(Vgf[2].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaNso2()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaNso2(decimal Ch2s,
            decimal Bgf)
        {
            ViewBag.Ch2s = Ch2s;
            ViewBag.Bgf = Bgf;

            string url = "api/Formulas/Nso2",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ch2s={Ch2s.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bgf={Bgf.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Nso2 = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Nso2 = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        //
        //------------------------------------------------------ГАЗ_6 (296)----------------------------------------------------------------
        //

        public IActionResult FormulaDdg()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaDdg(decimal O2dg)
        {
            ViewBag.O2dg = O2dg;

            string url = "api/Formulas/Ddg",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"O2dg={O2dg.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Ddg = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Ddg = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaV1vp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaV1vp(decimal Ve,
            decimal Vb,
            decimal Ddg)
        {
            ViewBag.Ve = Ve;
            ViewBag.Vb = Vb;
            ViewBag.Ddg = Ddg;

            string url = "api/Formulas/V1vp",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ve={Ve.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vb={Vb.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ddg={Ddg.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.V1vp = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.V1vp = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaVpg()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaVpg(decimal Bpg,
            decimal V1vp)
        {
            ViewBag.Bpg = Bpg;
            ViewBag.V1vp = V1vp;

            string url = "api/Formulas/Vpg",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bpg={Bpg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"V1vp={V1vp.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Vpg = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Vpg = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        //
        //------------------------------------------------------ГАЗ_7 (299)----------------------------------------------------------------
        //

        public IActionResult FormulaGAS7()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGAS7(decimal CcoDg,
            decimal Bt,
            decimal Qsnr,
            decimal Yco)
        {
            ViewBag.CcoDg = CcoDg;
            ViewBag.Bt = Bt;
            ViewBag.Qsnr = Qsnr;
            ViewBag.Yco = Yco;

            string url = "api/Formulas/GAS7",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CcoDg={CcoDg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bt={Bt.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qsnr={Qsnr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Yco={Yco.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            //OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }

            JObject jo = JObject.Parse(OutputViewText);
            ViewBag.GwDg = jo["gwDg"];
            ViewBag.Gw = jo["gw"];

            return View();
        }

        public IActionResult FormulaGAS7withParam()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGAS7withParam(decimal Q3,
            decimal Rdpt,
            decimal Qnr,
            decimal Dyx,
            decimal Psi,
            decimal Ups,
            decimal CcoDg,
            decimal Bt,
            decimal Qsnr)
        {
            ViewBag.Q3 = Q3;
            ViewBag.Rdpt = Rdpt;
            ViewBag.Qnr = Qnr;
            ViewBag.Dyx = Dyx;
            ViewBag.Psi = Psi;
            ViewBag.Ups = Ups;
            ViewBag.CcoDg = CcoDg;
            ViewBag.Bt = Bt;
            ViewBag.Qsnr = Qsnr;

            string url = "api/Formulas/GAS7withParam",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Q3={Q3.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Rdpt={Rdpt.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qnr={Qnr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dyx={Dyx.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Psi={Psi.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ups={Ups.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CcoDg={CcoDg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bt={Bt.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qsnr={Qsnr.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            //OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }

            JObject jo = JObject.Parse(OutputViewText);
            ViewBag.CcoSgt = jo["ccoSgt"];
            ViewBag.Yco = jo["yco"];
            ViewBag.GwDg = jo["gwDg"];
            ViewBag.Gw = jo["gw"];

            return View();
        }

        public IActionResult FormulaCcoSgt()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaCcoSgt(decimal Q3,
            decimal Rdpt,
            decimal Qnr)
        {
            ViewBag.Q3 = Q3;
            ViewBag.Rdpt = Rdpt;
            ViewBag.Qnr = Qnr;

            string url = "api/Formulas/CcoSgt",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Q3={Q3.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Rdpt={Rdpt.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qnr={Qnr.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.CcoSgt = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.CcoSgt = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaYco()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaYco(decimal Dyx,
            decimal CcoSgt,
            decimal Psi,
            decimal Ups)
        {
            ViewBag.Dyx = Dyx;
            ViewBag.CcoSgt = CcoSgt;
            ViewBag.Psi = Psi;
            ViewBag.Ups = Ups;

            string url = "api/Formulas/Yco",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dyx={Dyx.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CcoSgt={CcoSgt.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Psi={Psi.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ups={Ups.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Yco = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Yco = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaGwDg()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGwDg(decimal CcoDg,
            decimal Bt)
        {
            ViewBag.CcoDg = CcoDg;
            ViewBag.Bt = Bt;

            string url = "api/Formulas/GwDg",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CcoDg={CcoDg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bt={Bt.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.GwDg = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.GwDg = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaGw()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGw(decimal Bt,
            decimal Qsnr,
            decimal Yco,
            decimal Dyx,
            decimal CcoSgt,
            decimal Psi,
            decimal Ups)
        {
            ViewBag.Bt = Bt;
            ViewBag.Qsnr = Qsnr;
            ViewBag.Yco = Yco;
            ViewBag.Dyx = Dyx;
            ViewBag.CcoSgt = CcoSgt;
            ViewBag.Psi = Psi;
            ViewBag.Ups = Ups;

            string url = "api/Formulas/Gw",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bt={Bt.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qsnr={Qsnr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Yco={Yco.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dyx={Dyx.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CcoSgt={CcoSgt.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Psi={Psi.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ups={Ups.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Gw = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Gw = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        //
        //------------------------------------------------------ГАЗ_8 (304)----------------------------------------------------------------
        //

        public IActionResult FormulaNu()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaNu(decimal Nfact,
            decimal Nnom,
            decimal Dfact,
            decimal Dnom)
        {
            ViewBag.Nfact = Nfact;
            ViewBag.Nnom = Nnom;
            ViewBag.Dfact = Dfact;
            ViewBag.Dnom = Dnom;

            string url = "api/Formulas/Nu",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Nfact={Nfact.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Nnom={Nnom.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dfact={Dfact.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dnom={Dnom.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Nu = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Nu = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaYno2()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaYno2(decimal Nfact,
            decimal Nnom,
            decimal Dfact,
            decimal Dnom,
            decimal Yno2Nom,
            decimal DyxNo2,
            decimal Cno2,
            decimal PsiNo2,
            decimal UpsNo2)
        {
            ViewBag.Nfact = Nfact;
            ViewBag.Nnom = Nnom;
            ViewBag.Dfact = Dfact;
            ViewBag.Dnom = Dnom;
            ViewBag.Yno2Nom = Yno2Nom;
            ViewBag.DyxNo2 = DyxNo2;
            ViewBag.Cno2 = Cno2;
            ViewBag.PsiNo2 = PsiNo2;
            ViewBag.UpsNo2 = UpsNo2;

            string url = "api/Formulas/Yno2",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Nfact={Nfact.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Nnom={Nnom.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dfact={Dfact.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dnom={Dnom.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Yno2Nom={Yno2Nom.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"DyxNo2={DyxNo2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Cno2={Cno2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"PsiNo2={PsiNo2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"UpsNo2={UpsNo2.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Yno2 = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Yno2 = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaGno2()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGno2(decimal Bno2,
            decimal QrnNo2,
            decimal Yno2)
        {
            ViewBag.Bno2 = Bno2;
            ViewBag.QrnNo2 = QrnNo2;
            ViewBag.Yno2 = Yno2;

            string url = "api/Formulas/Gno2",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Bno2={Bno2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"QrnNo2={QrnNo2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Yno2={Yno2.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Gno2 = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Gno2 = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        //
        //------------------------------------------------------ГАЗ_9 (305)----------------------------------------------------------------
        //

        public IActionResult FormulaGi()
        {
            ViewBag.MiVv = new decimal[1];
            ViewBag.Ti = new decimal[1];
            ViewBag.KiO = new decimal[1];
            ViewBag.Ni = new decimal[1];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGi(decimal[] MiVv,
            decimal[] Ti,
            decimal[] KiO,
            decimal[] Ni)
        {
            decimal[] MiVvWithoutZero = MiVv.Where(v => v != 0).ToArray();
            ViewBag.MiVv = MiVvWithoutZero;
            decimal[] TiWithoutZero = Ti.Where(v => v != 0).ToArray();
            ViewBag.Ti = TiWithoutZero;
            decimal[] KiOWithoutZero = KiO.Where(v => v != 0).ToArray();
            ViewBag.KiO = KiOWithoutZero;
            decimal[] NiWithoutZero = Ni.Where(v => v != 0).ToArray();
            ViewBag.Ni = NiWithoutZero;

            string url = "api/Formulas/Gi",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", MiVvWithoutZero.Select(x => "MiVv=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", TiWithoutZero.Select(x => "Ti=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", KiOWithoutZero.Select(x => "KiO=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", NiWithoutZero.Select(x => "Ni=" + x.ToString().Replace(',', '.')))}";

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Gi = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Gi = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        //
        //------------------------------------------------------ГАЗ_10 (312)----------------------------------------------------------------
        //

        public async Task<IActionResult> FormulaGAS10()
        {
            string url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection10 = new SelectList(gasSectionArray);
            ViewBag.GasDensity10 = gasSection10;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGAS10(decimal i1,
            decimal i2,
            decimal Du,
            string RoGvs,
            decimal R,
            decimal Tsvr,
            decimal Tdelta,
            decimal H,
            decimal Astrat,
            decimal Eta,
            decimal PDK,
            decimal V1gvsov,
            decimal Fgvs)
        {
            ViewBag.i1 = i1;
            ViewBag.i2 = i2;
            ViewBag.Du = Du;
            ViewBag.RoGvs = RoGvs;
            ViewBag.R = R;
            ViewBag.Tsvr = Tsvr;
            ViewBag.Tdelta = Tdelta;
            ViewBag.H = H;
            ViewBag.Astrat = Astrat;
            ViewBag.Eta = Eta;
            ViewBag.PDK = PDK;
            ViewBag.V1gvsov = V1gvsov;
            ViewBag.Fgvs = Fgvs;

            string url = "api/Formulas/GAS10",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"i1={i1.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"i2={i2.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Du={Du.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"RoGvs={RoGvs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"R={R.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tsvr={Tsvr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tdelta={Tdelta.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"H={H.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Astrat={Astrat.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Eta={Eta.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"PDK={PDK.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"V1gvsov={V1gvsov.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Fgvs={Fgvs.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            //OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection10 = new SelectList(gasSectionArray, RoGvs);
            ViewBag.GasDensity10 = gasSection10;

            JObject jo = JObject.Parse(OutputViewText);
            ViewBag.W = jo["w"];
            ViewBag.F = jo["f"];
            ViewBag.V1gvs = jo["v1gvs"];
            ViewBag.Vgvs = jo["vgvs"];
            ViewBag.G = jo["g"];
            ViewBag.Fparam = jo["fparam"];
            ViewBag.Mgvs = jo["mgvs"];
            ViewBag.Um = jo["um"];
            ViewBag.Ngvs = jo["ngvs"];
            ViewBag.PDV = jo["pdv"];

            return View();
        }

        public IActionResult FormulaW()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaW(decimal i1,
            decimal i2)
        {
            ViewBag.i1 = i1;
            ViewBag.i2 = i2;

            string url = "api/Formulas/W",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"i1={i1.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"i2={i2.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.W = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.W = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaF()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaF(decimal Du)
        {
            ViewBag.Du = Du;

            string url = "api/Formulas/F",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Du={Du.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.F = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.F = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaV1gvs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaV1gvs(decimal F,
            decimal W)
        {
            ViewBag.F = F;
            ViewBag.W = W;

            string url = "api/Formulas/V1gvs",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"F={F.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"W={W.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.V1gvs = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.V1gvs = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaVgvs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaVgvs(decimal Du,
            decimal W)
        {
            ViewBag.Du = Du;
            ViewBag.W = W;

            string url = "api/Formulas/Vgvs",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Du={Du.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"W={W.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Vgvs = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Vgvs = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public async Task<IActionResult> FormulaG()
        {
            string url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection10 = new SelectList(gasSectionArray);
            ViewBag.GasDensity10 = gasSection10;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaG(decimal V1gvs,
            string RoGvs)
        {
            ViewBag.V1gvs = V1gvs;
            ViewBag.RoGvs = RoGvs;

            string url = "api/Formulas/G",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"V1gvs={V1gvs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"RoGvs={RoGvs.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.G = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection10 = new SelectList(gasSectionArray, RoGvs);
            ViewBag.GasDensity10 = gasSection10;
            ViewBag.G = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaFparam()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaFparam(decimal W,
            decimal Du,
            decimal R,
            decimal Tsvr)
        {
            ViewBag.W = W;
            ViewBag.Du = Du;
            ViewBag.R = R;
            ViewBag.Tsvr = Tsvr;

            string url = "api/Formulas/Fparam",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"W={W.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Du={Du.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"R={R.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tsvr={Tsvr.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Fparam = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Fparam = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaMgvs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaMgvs(decimal Fparam)
        {
            ViewBag.Fparam = Fparam;

            string url = "api/Formulas/Mgvs",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Fparam={Fparam.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Mgvs = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Mgvs = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaUm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaUm(decimal V1gvs,
            decimal Tdelta,
            decimal H)
        {
            ViewBag.V1gvs = V1gvs;
            ViewBag.Tdelta = Tdelta;
            ViewBag.H = H;

            string url = "api/Formulas/Um",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"V1gvs={V1gvs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tdelta={Tdelta.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"H={H.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Um = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Um = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaNgvs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaNgvs(decimal Um)
        {
            ViewBag.Um = Um;

            string url = "api/Formulas/Ngvs",
                route = "";
            
            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Um={Um.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Ngvs = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Ngvs = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaPDV()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaPDV(decimal PDK,
            decimal H,
            decimal V1gvsov,
            decimal Tdelta,
            decimal Astrat,
            decimal Fgvs,
            decimal Mgvs,
            decimal Ngvs,
            decimal Eta)
        {
            ViewBag.PDK = PDK;
            ViewBag.H = H;
            ViewBag.V1gvsov = V1gvsov;
            ViewBag.Tdelta = Tdelta;
            ViewBag.Astrat = Astrat;
            ViewBag.Fgvs = Fgvs;
            ViewBag.Mgvs = Mgvs;
            ViewBag.Ngvs = Ngvs;
            ViewBag.Eta = Eta;

            string url = "api/Formulas/PDV",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"PDK={PDK.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"H={H.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"V1gvsov={V1gvsov.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tdelta={Tdelta.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Astrat={Astrat.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Fgvs={Fgvs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Mgvs={Mgvs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Ngvs={Ngvs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Eta={Eta.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.PDV = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.PDV = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        //
        //------------------------------------------------------ГАЗ_11 (313)----------------------------------------------------------------
        //

        public IActionResult FormulaUmHol()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaUmHol(decimal W,
            decimal Du,
            decimal H)
        {
            ViewBag.W = W;
            ViewBag.Du = Du;
            ViewBag.H = H;

            string url = "api/Formulas/UmHol",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"W={W.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Du={Du.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"H={H.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.UmHol = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.UmHol = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaNgvsHol()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaNgvsHol(decimal UmHol)
        {
            ViewBag.UmHol = UmHol;

            string url = "api/Formulas/NgvsHol",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"UmHol={UmHol.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.NgvsHol = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.NgvsHol = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaPDVhol()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaPDVhol(decimal PDK,
            decimal H,
            decimal V1gvsov,
            decimal Astrat,
            decimal Fgvs,
            decimal Du,
            decimal NgvsHol,
            decimal Eta)
        {
            ViewBag.PDK = PDK;
            ViewBag.H = H;
            ViewBag.V1gvsov = V1gvsov;
            ViewBag.Astrat = Astrat;
            ViewBag.Fgvs = Fgvs;
            ViewBag.Du = Du;
            ViewBag.NgvsHol = NgvsHol;
            ViewBag.Eta = Eta;

            string url = "api/Formulas/PDVhol",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"PDK={PDK.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"H={H.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"V1gvsov={V1gvsov.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Astrat={Astrat.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Fgvs={Fgvs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Du={Du.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"NgvsHol={NgvsHol.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Eta={Eta.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.PDVhol = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.PDVhol = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        //
        //------------------------------------------------------ГАЗ_12 (314)----------------------------------------------------------------
        //

        public async Task<IActionResult> FormulaGr()
        {
            string url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection12 = new SelectList(gasSectionArray);
            ViewBag.GasDensity12 = gasSection12;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGr(decimal K,
            string RoRGr,
            decimal VrGr)
        {
            ViewBag.K = K;
            ViewBag.RoRGr = RoRGr;
            ViewBag.VrGr = VrGr;

            string url = "api/Formulas/Gr",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"K={K.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"RoRGr={RoRGr.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"VrGr={VrGr.ToString()}".Replace(',', '.');            

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Gr = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection12 = new SelectList(gasSectionArray, RoRGr);
            ViewBag.GasDensity12 = gasSection12;

            ViewBag.Gr = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaMiFact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaMiFact(decimal KiMif,
            decimal Vtr)
        {
            ViewBag.KiMif = KiMif;
            ViewBag.Vtr = Vtr;

            string url = "api/Formulas/MiFact",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"KiMif={KiMif.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Vtr={Vtr.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.MiFact = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            OutputViewText = OutputViewText.Replace("[", "").Replace("]", "");
            string[] MiFact = OutputViewText.Split(new char[] { ',' });
            ViewBag.MiFactNOx = Convert.ToDecimal(MiFact[0].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            ViewBag.MiFactCO = Convert.ToDecimal(MiFact[1].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        //
        //------------------------------------------------------ГАЗ_13 (315)----------------------------------------------------------------
        //

        public async Task<IActionResult> FormulaGAS13()
        {
            string url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection13 = new SelectList(gasSectionArray);
            ViewBag.GasDensity13 = gasSection13;
            ViewBag.Vks = new decimal[1];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGAS13(decimal Кphg,
            string RoPhgKs,
            decimal VgrPhg,
            decimal[] Vks,
            decimal Qgrs)
        {
            ViewBag.Кphg = Кphg;
            ViewBag.RoPhgKs = RoPhgKs;
            ViewBag.VgrPhg = VgrPhg;
            decimal[] VksWithoutZero = Vks.Where(v => v != 0).ToArray();
            ViewBag.Vks = VksWithoutZero;
            ViewBag.Qgrs = Qgrs;

            string url = "api/Formulas/GAS13",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Кphg={Кphg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"RoPhgKs={RoPhgKs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"VgrPhg={VgrPhg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VksWithoutZero.Select(x => "Vks=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qgrs={Qgrs.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            //OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection13 = new SelectList(gasSectionArray, RoPhgKs);
            ViewBag.GasDensity13 = gasSection13;

            JObject jo = JObject.Parse(OutputViewText);
            ViewBag.GrPhg = jo["grPhg"];
            ViewBag.GrKs = jo["grKs"];
            ViewBag.GrGrs = jo["grGrs"];

            return View();
        }

        public IActionResult FormulaGAS13withS()
        {
            ViewBag.VksS = new decimal[1];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGAS13withS(decimal КphgS,
            decimal VphgS,
            decimal MrPhgS,
            decimal[] VksS,
            decimal MsKsS,
            decimal QgrsS)
        {
            ViewBag.КphgS = КphgS;
            ViewBag.VphgS = VphgS;
            ViewBag.MrPhgS = MrPhgS;
            decimal[] VksSWithoutZero = VksS.Where(v => v != 0).ToArray();
            ViewBag.VksS = VksSWithoutZero;
            ViewBag.MsKsS = MsKsS;
            ViewBag.QgrsS = QgrsS;

            string url = "api/Formulas/GAS13withS",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"КphgS={КphgS.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"VphgS={VphgS.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"MrPhgS={MrPhgS.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VksSWithoutZero.Select(x => "VksS=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"MsKsS={MsKsS.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"QgrsS={QgrsS.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            //OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }

            JObject jo = JObject.Parse(OutputViewText);
            ViewBag.GrPhgS = jo["grPhgS"];
            ViewBag.GrKsS = jo["grKsS"];
            ViewBag.GrGrsS = jo["grGrsS"];

            return View();
        }

        public async Task<IActionResult> FormulaGrPhg()
        {
            string url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection13 = new SelectList(gasSectionArray);
            ViewBag.GasDensity13 = gasSection13;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGrPhg(decimal Кphg,
            string RoPhgKs,
            decimal VgrPhg)
        {
            ViewBag.Кphg = Кphg;
            ViewBag.RoPhgKs = RoPhgKs;
            ViewBag.VgrPhg = VgrPhg;

            string url = "api/Formulas/GrPhg",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Кphg={Кphg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"RoPhgKs={RoPhgKs.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"VgrPhg={VgrPhg.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.CcoSgt = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection13 = new SelectList(gasSectionArray, RoPhgKs);
            ViewBag.GasDensity13 = gasSection13;
            ViewBag.GrPhg = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public async Task<IActionResult> FormulaGrKs()
        {
            string url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection13 = new SelectList(gasSectionArray);
            ViewBag.GasDensity13 = gasSection13;
            ViewBag.Vks = new decimal[1];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGrKs(decimal[] Vks,
            string RoPhgKs)
        {
            decimal[] VksWithoutZero = Vks.Where(v => v != 0).ToArray();
            ViewBag.Vks = VksWithoutZero;
            ViewBag.RoPhgKs = RoPhgKs;

            string url = "api/Formulas/GrKs",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VksWithoutZero.Select(x => "Vks=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"RoPhgKs={RoPhgKs.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.CcoSgt = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection13 = new SelectList(gasSectionArray, RoPhgKs);
            ViewBag.GasDensity13 = gasSection13;
            ViewBag.GrKs = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaGrGrs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGrGrs(decimal Qgrs)
        {
            ViewBag.Qgrs = Qgrs;

            string url = "api/Formulas/GrGrs",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Qgrs={Qgrs.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.CcoSgt = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.GrGrs = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaGrPhgS()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGrPhgS(decimal КphgS,
            decimal VphgS,
            decimal MrPhgS)
        {
            ViewBag.КphgS = КphgS;
            ViewBag.VphgS = VphgS;
            ViewBag.MrPhgS = MrPhgS;

            string url = "api/Formulas/GrPhgS",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"КphgS={КphgS.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"VphgS={VphgS.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"MrPhgS={MrPhgS.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.CcoSgt = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.GrPhgS = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaGrKsS()
        {
            ViewBag.VksS = new decimal[1];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGrKsS(decimal[] VksS,
            decimal MsKsS)
        {
            decimal[] VksSWithoutZero = VksS.Where(v => v != 0).ToArray();
            ViewBag.VksS = VksSWithoutZero;
            ViewBag.MsKsS = MsKsS;

            string url = "api/Formulas/GrKsS",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VksSWithoutZero.Select(x => "VksS=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"MsKsS={MsKsS.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.CcoSgt = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.GrKsS = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaGrGrsS()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGrGrsS(decimal QgrsS)
        {
            ViewBag.QgrsS = QgrsS;

            string url = "api/Formulas/GrGrsS",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"QgrsS={QgrsS.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.CcoSgt = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.GrGrsS = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        //
        //------------------------------------------------------ГАЗ_14 (317)----------------------------------------------------------------
        //

        public async Task<IActionResult> FormulaGAS14()
        {
            string url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection14 = new SelectList(gasSectionArray);
            ViewBag.GasDensity14 = gasSection14;

            ViewBag.Vtg = new decimal[1];
            ViewBag.Vgdg = new decimal[1];
            ViewBag.Vsa = new decimal[1];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGAS14(decimal[] Vtg,
            decimal MiTg,
            decimal[] Vgdg,
            decimal CiDg,
            decimal KiDg,
            decimal CsSa,
            string RoSa,
            decimal[] Vsa)
        {
            decimal[] VtgWithoutZero = Vtg.Where(v => v != 0).ToArray();
            ViewBag.Vtg = VtgWithoutZero;
            ViewBag.MiTg = MiTg;
            decimal[] VgdgWithoutZero = Vgdg.Where(v => v != 0).ToArray();
            ViewBag.Vgdg = VgdgWithoutZero;
            ViewBag.CiDg = CiDg;
            ViewBag.KiDg = KiDg;
            ViewBag.CsSa = CsSa;
            ViewBag.RoSa = RoSa;
            decimal[] VsaWithoutZero = Vsa.Where(v => v != 0).ToArray();
            ViewBag.Vsa = VsaWithoutZero;

            string url = "api/Formulas/GAS14",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VtgWithoutZero.Select(x => "Vtg=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"MiTg={MiTg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VgdgWithoutZero.Select(x => "Vgdg=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CiDg={CiDg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"KiDg={KiDg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CsSa={CsSa.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"RoSa={RoSa.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VsaWithoutZero.Select(x => "Vsa=" + x.ToString().Replace(',', '.')))}";

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            //OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection14 = new SelectList(gasSectionArray, RoSa);
            ViewBag.GasDensity14 = gasSection14;

            JObject jo = JObject.Parse(OutputViewText);
            string outputText = Convert.ToString(jo["gdg"]);
            outputText = outputText.Replace("[", "").Replace("]", "").Replace(" ", "").Replace("\r\n", "");
            string[] Gdg = outputText.Split(new char[] { ',' });
            if (CiDg == 0)
            {
                ViewBag.Gtg = jo["gtg"];
                if(Gdg[0].IndexOf('E') > 0)
                {
                    ViewBag.GdgNOX = Gdg[0];
                }
                else
                {
                    ViewBag.GdgNOX = Convert.ToDecimal(Gdg[0].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                }
                if (Gdg[1].IndexOf('E') > 0)
                {
                    ViewBag.GdgCO = Gdg[1];
                }
                else
                {
                    ViewBag.GdgCO = Convert.ToDecimal(Gdg[1].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                }
                ViewBag.Gsa = jo["gsa"];
            }
            else
            {
                ViewBag.Gtg = jo["gtg"];
                if (Gdg[0].IndexOf('E') > 0)
                {
                    ViewBag.Gdg = Gdg[0];
                }
                else
                {
                    ViewBag.Gdg = Convert.ToDecimal(Gdg[0].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                }
                ViewBag.Gsa = jo["gsa"];
            }
            //ViewBag.Gtg = jo["gtg"];
            //ViewBag.Gdg = jo["gdg"];
            //ViewBag.Gsa = jo["gsa"];

            return View();
        }

        public IActionResult FormulaGtg()
        {
            ViewBag.Vtg = new decimal[1];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGtg(decimal[] Vtg,
            decimal MiTg)
        {
            decimal[] VtgWithoutZero = Vtg.Where(v => v != 0).ToArray();
            ViewBag.Vtg = VtgWithoutZero;
            ViewBag.MiTg = MiTg;

            string url = "api/Formulas/Gtg",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VtgWithoutZero.Select(x => "Vtg=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"MiTg={MiTg.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Vgdg = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Gtg = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaGdg()
        {
            ViewBag.Vgdg = new decimal[1];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGdg(decimal[] Vgdg,
            decimal CiDg,
            decimal KiDg)
        {
            decimal[] VgdgWithoutZero = Vgdg.Where(v => v != 0).ToArray();
            ViewBag.Vgdg = VgdgWithoutZero;
            ViewBag.CiDg = CiDg;
            ViewBag.KiDg = KiDg;

            string url = "api/Formulas/Gdg",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VgdgWithoutZero.Select(x => "Vgdg=" + x.ToString().Replace(',', '.')))}";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CiDg={CiDg.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"KiDg={KiDg.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Vgdg = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            //ViewBag.Vgf = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            //ViewBag.VgfCO = OutputViewText.Substring(',');
            //ViewBag.VgfCH4 = OutputViewText.Substring(',');
            //ViewBag.VgfNO2 = OutputViewText.Substring(',');
            OutputViewText = OutputViewText.Replace("[", "").Replace("]", "");
            string[] Gdg = OutputViewText.Split(new char[] { ',' });
            if (CiDg == 0)
            {
                ViewBag.GdgNOX = Convert.ToDecimal(Gdg[0].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                ViewBag.GdgCO = Convert.ToDecimal(Gdg[1].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }
            else
            {
                ViewBag.Gdg = Convert.ToDecimal(Gdg[0].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }
            return View();
        }

        public async Task<IActionResult> FormulaGsa()
        {
            string url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection14 = new SelectList(gasSectionArray);
            ViewBag.GasDensity14 = gasSection14;

            ViewBag.Vsa = new decimal[1];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaGsa(decimal CsSa,
            string RoSa,
            decimal[] Vsa)
        {
            ViewBag.CsSa = CsSa;
            ViewBag.RoSa = RoSa;
            decimal[] VsaWithoutZero = Vsa.Where(v => v != 0).ToArray();
            ViewBag.Vsa = VsaWithoutZero;

            string url = "api/Formulas/Gsa",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"CsSa={CsSa.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"RoSa={RoSa.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"{string.Join("&", VsaWithoutZero.Select(x => "Vsa=" + x.ToString().Replace(',', '.')))}";

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Nso2 = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            url = "api/GasDensities/GasSelect";
            string[] gasSectionArray = await ListFromRequestToAPI(url);
            var gasSection14 = new SelectList(gasSectionArray, RoSa);
            ViewBag.GasDensity14 = gasSection14;

            ViewBag.Gsa = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        //
        //------------------------------------------------------Графики----------------------------------------------------------------
        //
        public IActionResult FormulaTog()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaTog(decimal Pcp,
            decimal Dd,
            decimal L,
            decimal Togm)
        {
            ViewBag.Pcp = Pcp;
            ViewBag.Dd = Dd;
            ViewBag.L = L;
            ViewBag.Togm = Togm;

            string url = "api/Formulas/Tog",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pcp={Pcp.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dd={Dd.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"L={L.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Togm={Togm.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Gno2 = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Tog = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaZg()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaZg(decimal Pz,
            decimal Delta,
            decimal Tz)
        {
            ViewBag.Pz = Pz;
            ViewBag.Delta = Delta;
            ViewBag.Tz = Tz;

            string url = "api/Formulas/Zg",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pz={Pz.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Delta={Delta.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Tz={Tz.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Gno2 = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Zg = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }

        public IActionResult FormulaVgraf()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormulaVgraf(decimal Pv,
            decimal Dv,
            decimal Lv)
        {
            ViewBag.Pv = Pv;
            ViewBag.Dv = Dv;
            ViewBag.Lv = Lv;

            string url = "api/Formulas/Vgraf",
                route = "";

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Pv={Pv.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Dv={Dv.ToString()}".Replace(',', '.');

            route += string.IsNullOrEmpty(route) ? "?" : "&";
            route += $"Lv={Lv.ToString()}".Replace(',', '.');

            HttpResponseMessage response = await _HttpApiClient.PostAsync(url + route, null);

            string OutputViewText = await response.Content.ReadAsStringAsync();
            OutputViewText = OutputViewText.Replace("<br>", Environment.NewLine);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                dynamic errors = JsonConvert.DeserializeObject<dynamic>(OutputViewText);
                foreach (JProperty property in errors.Children())
                {
                    //ModelState.AddModelError(property.Name, property.Value[0].ToString());
                    ViewBag.Errors = $"{property.Name}: {property.Value[0].ToString()}";
                }
                return View();
            }
            //ViewBag.Gno2 = String.Format("{0:0.00}", Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
            ViewBag.Vgraf = Convert.ToDecimal(OutputViewText.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            return View();
        }
    }
}