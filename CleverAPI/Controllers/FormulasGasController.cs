using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleverAPI.Controllers
{
    public partial class FormulasController : Controller
    {
        /// <summary>
        /// ------------------------------------------ГАЗ_1 (272)---------------------------------------------
        /// </summary>
        /// <param name="Do"></param>
        /// <param name="L"></param>
        /// <param name="Pa"></param>
        /// <param name="t0"></param>
        /// <param name="Po"></param>
        /// <param name="tn"></param>
        /// <param name="Pz"></param>
        /// <param name="Delta"></param>
        /// <param name="Tz"></param>
        /// <param name="D"></param>
        /// <param name="Np"></param>
        /// <param name="P"></param>
        /// <param name="t"></param>
        /// <param name="Tk"></param>
        /// <param name="Ror"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GAS1")]
        public ActionResult CalcGAS1(decimal Do,
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
            decimal
            Ss = CalcSs(Do),
            Vk = CalcVk(Ss, L),
            Vstr = CalcVstr(Vk, Pa, t0, Po, tn, Pz, Delta, Tz),
            V1 = CalcV1(D, Np, P, t, Tk, Ror);

            return Json(new
            {
                Ss,
                Vk,
                Vstr,
                V1
            });
        }

        /// <summary>
        /// Площадь сечения
        /// </summary>
        /// <param name="Do"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Ss")]
        public decimal CalcSs(decimal Do)
        {
            try
            {
                return Convert.ToDecimal(Math.PI) * Convert.ToDecimal(Math.Pow(Convert.ToDouble(Do / 1000), 2)) / 4;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Геометрический объем метанольниц, шлейфов и соединительных газопроводов
        /// </summary>
        /// <param name="Ss"></param>
        /// <param name="L"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Vk")]
        public decimal CalcVk(decimal Ss,
            decimal L)
        {
            try
            {
                return Ss * L;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Объем выброса при стравливании газа из метанольниц, шлейфов и соединительных газопроводов на свечу
        /// </summary>
        /// <param name="Vk"></param>
        /// <param name="Pa"></param>
        /// <param name="t0"></param>
        /// <param name="Po"></param>
        /// <param name="tn"></param>
        /// <param name="Pz"></param>
        /// <param name="Delta"></param>
        /// <param name="Tz"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Vstr")]
        public decimal CalcVstr(decimal Vk,
            decimal Pa,
            decimal t0,
            decimal Po,
            decimal tn,
            decimal Pz,
            decimal Delta,
            decimal Tz)
        {
            try
            {
                return Vk * (Pa * (t0 + 273) / (Po * (tn + 273) * CalcZg(Pz, Delta, Tz)));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Объем стравливаемого  газа, выбрасываемого в атмосферу при продувке скважин
        /// </summary>
        /// <param name="D"></param>
        /// <param name="Np"></param>
        /// <param name="P"></param>
        /// <param name="t"></param>
        /// <param name="Tk"></param>
        /// <param name="Ror"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("V1")]
        public decimal CalcV1(decimal D,
            decimal Np,
            decimal P,
            decimal t,
            decimal Tk,
            string Ror)
        {
            try
            {
                int id = _context.GasDensity.Where(d => d.Name == Ror).First().Id;
                decimal? Ro = _context.GasDensity.Where(d => d.Id == id).First().Value;
                Ro = Ro == null ? 0 : Ro;
                return 326 * Convert.ToDecimal(Math.Pow(Convert.ToDouble(D), 2)) * Np * (P * t / Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(Ro * Tk))));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ------------------------------------------ГАЗ_2 (273)---------------------------------------------
        /// </summary>
        /// <param name="Do"></param>
        /// <param name="L"></param>
        /// <param name="Pa"></param>
        /// <param name="t0"></param>
        /// <param name="Po"></param>
        /// <param name="tn"></param>
        /// <param name="Pz"></param>
        /// <param name="Delta"></param>
        /// <param name="Tz"></param>
        /// <param name="Pcp"></param>
        /// <param name="Dd"></param>
        /// <param name="Togm"></param>
        /// <param name="Ror"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GAS2")]
        public ActionResult CalcGAS2(decimal Do,
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
            decimal
            Ss = CalcSs(Do),
            VkKs = CalcVk(Ss, L),
            Vsst = CalcVsst(VkKs, Pa, t0, Po, tn, Pz, Delta, Tz),
            V1Ks = CalcV1Ks(Vsst, Pcp, Dd, L, Togm),
            G1 = CalcG1(Ror, V1Ks);

            return Json(new
            {
                Vsst,
                V1Ks,
                G1
            });
        }

        /// <summary>
        /// Количество газа, стравливаемое при остановке и разгрузке одного компрессора
        /// </summary>
        /// <param name="Vk"></param>
        /// <param name="Pa"></param>
        /// <param name="t0"></param>
        /// <param name="Po"></param>
        /// <param name="tn"></param>
        /// <param name="Pz"></param>
        /// <param name="Delta"></param>
        /// <param name="Tz"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Vsst")]
        public decimal CalcVsst(decimal Vk,
            decimal Pa,
            decimal t0,
            decimal Po,
            decimal tn,
            decimal Pz,
            decimal Delta,
            decimal Tz)
        {
            try
            {
                return (Vk * Pa * (t0 + 273)) / (Po * (tn + 273) * CalcZg(Pz, Delta, Tz));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Объем газа, стравливаемый в атмосферу при разгрузке компрессора в единицу времени
        /// </summary>
        /// <param name="Vsst"></param>
        /// <param name="Pcp"></param>
        /// <param name="Dd"></param>
        /// <param name="L"></param>
        /// <param name="Togm"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("V1Ks")]
        public decimal CalcV1Ks(decimal Vsst,
            decimal Pcp,
            decimal Dd,
            decimal L,
            decimal Togm)
        {
            try
            {
                return Vsst / (CalcTog(Pcp, Dd, L, Togm) * 60);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Весовое количество газа, стравливаемое в атмосферу при разгрузке компрессора в единицу времени
        /// </summary>
        /// <param name="Ror"></param>
        /// <param name="V1Ks"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("G1")]
        public decimal CalcG1(string Ror,
            decimal V1Ks)
        {
            try
            {
                // плотность газа теперь не вводится, а берется из таблицы
                // Pon - плотность газа
                // плотность газа надо убрать из входных параметров функции и в Clever в интерфейсе
                // не помню, какая именно переменная - плотность (может не Po)
                //decimal? Ro = _context.GasDensity.FirstOrDefault(d => d.Name.ToLower().Contains(Ror))?.Value;
                int id = _context.GasDensity.Where(d => d.Name == Ror).First().Id;
                decimal? Ro = _context.GasDensity.Where(d => d.Id == id).First().Value;
                Ro = Ro == null ? 0 : Ro;
                return V1Ks * Convert.ToDecimal(Ro);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ------------------------------------------ГАЗ_3 (286)---------------------------------------------
        /// 
        /// Годовые потери газа в атмосферу при испытании скважин на ПХГ, вышедших из капитального ремонта или бурения
        /// </summary>
        /// <param name="Vsr"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Vphg")]
        public decimal CalcVphg(decimal[] Vsr)
        {
            try
            {
                return Vsr.Sum();
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ------------------------------------------ГАЗ_4 (287)---------------------------------------------
        /// </summary>
        /// <param name="D1"></param>
        /// <param name="Bg"></param>
        /// <param name="Mi"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GAS4")]
        public ActionResult CalcGAS2(decimal D1,
            decimal Bg,
            string Mi)
        {
            decimal Vvg = CalcVvg(D1, Bg);
            decimal[] M = CalcM(Mi, Vvg);

            return Json(new
            {
                Vvg,
                M
            });
        }

        /// <summary>
        /// Объем выхлопных газов
        /// </summary>
        /// <param name="D1"></param>
        /// <param name="Bg"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Vvg")]
        public decimal CalcVvg(decimal D1,
            decimal Bg)
        {
            try
            {
                return D1 * K * Bg;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Количество вредных веществ в выхлопных газах ГПА
        /// </summary>
        /// <param name="Mi"></param>
        /// <param name="Vvg"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("M")]
        public decimal[] CalcM(string Mi,
            decimal Vvg)
        {
            decimal[] M = new decimal[2];
            try
            {
                int id = _context.ParametersEmissionSource.Where(d => d.Name == Mi).First().Id;
                decimal? MiNOx = _context.ParametersEmissionSource.Where(d => d.Id == id).First().EjectionNOx;
                MiNOx = MiNOx == null ? 0 : MiNOx;
                decimal? MiCO = _context.ParametersEmissionSource.Where(d => d.Id == id).First().EjectionCO;
                MiCO = MiCO == null ? 0 : MiCO;
                M[0] = Convert.ToDecimal(MiNOx) * Vvg * Convert.ToDecimal(Math.Pow(10, -6)) * 24 * 365;
                M[1] = Convert.ToDecimal(MiCO) * Vvg * Convert.ToDecimal(Math.Pow(10, -6)) * 24 * 365;
                return M;
            }
            catch
            {
                M = null;
                return M;
            }
        }

        /// <summary>
        /// ------------------------------------------ГАЗ_5 (288)---------------------------------------------
        /// </summary>
        /// <param name="Bgs"></param>
        /// <param name="Dgs"></param>
        /// <param name="E"></param>
        /// <param name="Ki"></param>
        /// <param name="Bgf"></param>
        /// <param name="Ch2s"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GAS5")]
        public ActionResult CalcGAS5(decimal Bgs,
            decimal Dgs,
            string E,
            decimal Ki,
            decimal Bgf,
            decimal Ch2s)
        {
            decimal Vdg = CalcVdg(Bgs, Dgs, E);
            decimal[] Vgf = CalcVgf(Ki, Bgf);
            decimal Nso2 = CalcNso2(Ch2s, Bgf);

            return Json(new
            {
                Vdg,
                Vgf,
                Nso2
            });
        }

        /// <summary>
        /// Объём дымовых газов, величину которого рассчитывают по уравнениям процесса сгорания
        /// </summary>
        /// <param name="Bgs"></param>
        /// <param name="Dgs"></param>
        /// <param name="E"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Vdg")]
        public decimal CalcVdg(decimal Bgs,
            decimal Dgs,
            string E)
        {
            try
            {
                int id = _context.CaloricEquivalent.Where(d => d.Name == E).First().Id;
                decimal? Ekv = _context.CaloricEquivalent.Where(d => d.Id == id).First().Value;
                Ekv = Ekv == null ? 0 : Ekv;
                return 7.84M * Bgs * Dgs * Convert.ToDecimal(Ekv);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Количество ВВ выбрасываемых в атмосферу при сжигании газа на факеле
        /// </summary>
        /// <param name="Ki"></param>
        /// <param name="Bgf"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Vgf")]
        public decimal[] CalcVgf(decimal Ki,
            decimal Bgf)
        {
            decimal[] Vgf = new decimal[3];
            try
            {
                if (Ki == 1)
                {
                    Vgf[0] = 0.02M * Bgf;
                    Vgf[1] = 0.0005M * Bgf;
                    Vgf[2] = 0.003M * Bgf;
                    return Vgf;
                }
                else
                {
                    Vgf[0] = 0.057M * Bgf;
                    Vgf[1] = 0.015M * Bgf;
                    Vgf[2] = 0.001M * Bgf;
                    return Vgf;
                }
            }
            catch
            {
                Vgf = null;
                return Vgf;
            }
        }

        /// <summary>
        /// Выброс SO2 определяют по содержанию серосодержащих в сжигаемом газе
        /// </summary>
        /// <param name="Ch2s"></param>
        /// <param name="Bgf"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Nso2")]
        public decimal CalcNso2(decimal Ch2s,
            decimal Bgf)
        {
            try
            {
                return 1.88M * Ch2s * Bgf * Convert.ToDecimal(Math.Pow(10, -2));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        ///------------------------------------------ГАЗ_6 (296)---------------------------------------------
        /// 
        /// Коэффициент избытка воздуха в дымовых газах
        /// </summary>
        /// <param name="O2dg"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Ddg")]
        public decimal CalcDdg(decimal O2dg)
        {
            try
            {
                return (21 / (21 - O2dg));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Суммарный объём влажных продуктов полного сгорания при сжигании м3 природного газа
        /// </summary>
        /// <param name="Ve"></param>
        /// <param name="Vb"></param>
        /// <param name="Ddg"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("V1vp")]
        public decimal CalcV1vp(decimal Ve,
            decimal Vb,
            decimal Ddg)
        {
            try
            {
                return Ve + Vb * (Ddg - 1);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Суммарный объём влажных продуктов полного сгорания при сжигании м3 природного газа
        /// </summary>
        /// <param name="Bpg"></param>
        /// <param name="V1vp"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Vpg")]
        public decimal CalcVpg(decimal Bpg,
            decimal V1vp)
        {
            try
            {
                return Bpg * V1vp;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ------------------------------------------ГАЗ_7 (299)---------------------------------------------
        /// </summary>
        /// <param name="CcoDg"></param>
        /// <param name="Bt"></param>
        /// <param name="Qsnr"></param>
        /// <param name="Yco"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GAS7")]
        public ActionResult CalcGAS7(decimal CcoDg,
            decimal Bt,
            decimal Qsnr,
            decimal Yco)
        {
            decimal
                GwDg = CalcGwDg(CcoDg, Bt),
                Gw = CalcGw(Bt, Qsnr, Yco, 0, 0, 0, 0);

            return Json(new
            {
                GwDg,
                Gw
            });
        }

        /// <summary>
        /// </summary>
        /// <param name="Q3"></param>
        /// <param name="Rdpt"></param>
        /// <param name="Qnr"></param>
        /// <param name="Dyx"></param>
        /// <param name="Psi"></param>
        /// <param name="Ups"></param>
        /// <param name="CcoDg"></param>
        /// <param name="Bt"></param>
        /// <param name="Qsnr"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GAS7withParam")]
        public ActionResult CalcGAS7withParam(decimal Q3,
            decimal Rdpt,
            decimal Qnr,
            decimal Dyx,
            decimal Psi,
            decimal Ups,
            decimal CcoDg,
            decimal Bt,
            decimal Qsnr)
        {
            decimal
                CcoSgt = CalcCcoSgt(Q3, Rdpt, Qnr),
                Yco = CalcYco(Dyx, CcoSgt, Psi, Ups),
                GwDg = CalcGwDg(CcoDg, Bt),
                Gw = CalcGw(Bt, Qsnr, Yco, 0, 0, 0, 0);

            return Json(new
            {
                CcoSgt,
                Yco,
                GwDg,
                Gw
            });
        }

        /// <summary>
        /// Выход окиси углерода при сжигании газообразного топлива
        /// </summary>
        /// <param name="Q3"></param>
        /// <param name="Rdpt"></param>
        /// <param name="Qnr"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("CcoSgt")]
        public decimal CalcCcoSgt(decimal Q3,
            decimal Rdpt,
            decimal Qnr)
        {
            try
            {
                return Q3 * Rdpt * Qnr;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Параметр, зависимый от вида топлива, конструкции топочного устройства и характеризующий количество окиси углерода, образующегося на 1 ГДж тепла, выделяемого при горении топлива, если имеются результаты непосредственного определения содержания CО в дымовых газах
        /// </summary>
        /// <param name="Dyx"></param>
        /// <param name="CcoSgt"></param>
        /// <param name="Psi"></param>
        /// <param name="Ups"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Yco")]
        public decimal CalcYco(decimal Dyx,
            decimal CcoSgt,
            decimal Psi,
            decimal Ups)
        {
            try
            {
                return 12.5M * Dyx * CcoSgt * Psi * Ups;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        ///  Количество окиси углерода , выбрасываемое в атмосферу с дымовыми газами котлоагрегата
        /// </summary>
        /// <param name="CcoDg"></param>
        /// <param name="Bt"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GwDg")]
        public decimal CalcGwDg(decimal CcoDg,
            decimal Bt)
        {
            try
            {
                return 0.001M * CcoDg * Bt;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Количество выбросов окиси углерода
        /// </summary>
        /// <param name="Bt"></param>
        /// <param name="Qsnr"></param>
        /// <param name="Yco"></param>
        /// 
        /// <param name="Dyx"></param>
        /// <param name="CcoSgt"></param>
        /// <param name="Psi"></param>
        /// <param name="Ups"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Gw")]
        public decimal CalcGw(decimal Bt,
            decimal Qsnr,
            decimal Yco,
            decimal Dyx,
            decimal CcoSgt,
            decimal Psi,
            decimal Ups)
        {
            try
            {
                if (Yco == 0)
                {
                    return 0.001M * Bt * Qsnr * 0.00419M * CalcYco(Dyx, CcoSgt, Psi, Ups);
                }
                else
                {
                    return 0.001M * Bt * Qsnr * 0.00419M * Yco;
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ------------------------------------------ГАЗ_8 (304)---------------------------------------------
        /// 
        /// Параметр
        /// </summary>
        /// <param name="Nfact"></param>
        /// <param name="Nnom"></param>
        /// <param name="Dfact"></param>
        /// <param name="Dnom"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Nu")]
        public decimal CalcNu(decimal Nfact,
            decimal Nnom,
            decimal Dfact,
            decimal Dnom)
        {
            try
            {
                if (Nnom != 0)
                {
                    return Nfact / Nnom;
                }
                else
                {
                    return Dfact / Dnom;
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Параметр, характеризующий количество NOx образующих на 1 ГДж тепла, выделяемого при горении топлива
        /// </summary>
        /// <param name="Nfact"></param>
        /// <param name="Nnom"></param>
        /// <param name="Dfact"></param>
        /// <param name="Dnom"></param>
        /// <param name="Yno2Nom "></param>
        /// <param name="DyxNo2 "></param>
        /// <param name="Cno2 "></param>
        /// <param name="PsiNo2 "></param>
        /// <param name="UpsNo2 "></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Yno2")]
        public decimal CalcYno2(decimal Nfact,
            decimal Nnom,
            decimal Dfact,
            decimal Dnom,
            decimal Yno2Nom,
            decimal DyxNo2,
            decimal Cno2,
            decimal PsiNo2,
            decimal UpsNo2)
        {
            try
            {
                if (Nnom != 0 && Nnom == Nfact)
                {
                    return 0.061M + 0.014M * Convert.ToDecimal(Math.Log(Convert.ToDouble(Nnom)));
                }
                if (Dnom != 0 && Dnom == Dfact)
                {
                    return 0.061M + 0.014M * Convert.ToDecimal(600 * Math.Log(Convert.ToDouble(Dnom))); ;
                }
                if (Nnom == 0 && Dnom == 0)
                {
                    return 20.5M * DyxNo2 * Cno2 * PsiNo2 * UpsNo2;
                }
                else
                {
                    return Yno2Nom * Convert.ToDecimal(Math.Pow(Convert.ToDouble(CalcNu(Nfact, Nnom, Dfact, Dnom)), 0.25));
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Количество NOx в пересчёте на NO2, выбрасываемых в атмосферу с дымовыми газами котлоагрегатов
        /// </summary>
        /// <param name="Bno2"></param>
        /// <param name="QrnNo2"></param>
        /// <param name="Yno2"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Gno2")]
        public decimal CalcGno2(decimal Bno2,
            decimal QrnNo2,
            decimal Yno2)
        {
            try
            {
                return 20.5M * Bno2 * QrnNo2 * Yno2;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ------------------------------------------ГАЗ_9 (305)---------------------------------------------
        /// 
        /// Количество вредных веществ в выхлопных газах ГПА
        /// </summary>
        /// <param name="MiUv"></param>
        /// <param name="Vi"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MiVv")]
        public decimal[] CalcMiVv(decimal[] MiUv,
            decimal[] Vi)
        {
            decimal[] MiVv = new decimal[MiUv.Length];
            try
            {
                for (int i = 0; i < MiUv.Length; i++)
                {
                    MiVv[i] = MiUv[i] * Vi[i];
                }
                return MiVv;
            }
            catch
            {
                MiVv[0] = 0;
                return MiVv;
            }
        }

        /// <summary>
        /// Количество вредных веществ в выхлопных газах ГПА
        /// </summary>
        /// <param name="MiVv"></param>
        /// <param name="Ti"></param>
        /// <param name="KiO"></param>
        /// <param name="Ni"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Gi")]
        public decimal CalcGi(decimal[] MiVv,
            decimal[] Ti,
            decimal[] KiO,
            decimal[] Ni)
        {
            decimal Gi = 0;
            try
            {
                for (int i = 0; i < MiVv.Length; i++)
                {
                    Gi = Gi + (MiVv[i] * Ti[i] * KiO[i] * Ni[i] * Convert.ToDecimal(Math.Pow(10, -6)));
                }
                return Gi;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ------------------------------------------ГАЗ_10 (312)---------------------------------------------
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="i2"></param> 
        /// <param name="Du"></param>
        /// <param name="RoGvs"></param>
        /// <param name="R"></param>
        /// <param name="Tsvr"></param> 
        /// <param name="Tdelta"></param>
        /// <param name="H"></param>
        /// <param name="Astrat"></param>
        /// <param name="Eta"></param> 
        /// <param name="PDK"></param>
        /// <param name="V1gvsov"></param>
        /// <param name="Fgvs"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GAS10")]
        public ActionResult CalcGAS10(decimal i1,
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
            decimal W = CalcW(i1, i2),
            F = CalcF(Du),
            V1gvs = CalcV1gvs(F, W),
            Vgvs = CalcVgvs(Du, W),
            G = CalcG(V1gvs, RoGvs),
            Fparam = CalcFparam(W, Du, R, Tsvr),
            Mgvs = CalcMgvs(Fparam),
            Um = CalcUm(V1gvs, Tdelta, H),
            Ngvs = CalcNgvs(Um),
            PDV = CalcPDV(PDK, H, V1gvsov, Tdelta, Astrat, Fgvs, Mgvs, Ngvs, Eta);

            return Json(new
            {
                W,
                F,
                V1gvs,
                Vgvs,
                G,
                Fparam,
                Mgvs,
                Um,
                Ngvs,
                PDV
            });
        }

        /// <summary>
        /// Средняя скорость выхода газовоздушной смеси из устья источника выброса
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("W")]
        public decimal CalcW(decimal i1,
            decimal i2)
        {
            try
            {
                return 91.5M * Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(i1 - i2)));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Площадь поперечного сечения устья свечи
        /// </summary>
        /// <param name="Du"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("F")]
        public decimal CalcF(decimal Du)
        {
            try
            {
                return (Convert.ToDecimal(Math.PI) * Convert.ToDecimal(Math.Pow(Convert.ToDouble(Du), 2))) / 4;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Объем гвс при критических скоростях выброса
        /// </summary>
        /// <param name="F"></param>
        /// <param name="W"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("V1gvs")]
        public decimal CalcV1gvs(decimal F,
            decimal W)
        {
            try
            {
                return F * W;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Объем газовоздушной смеси
        /// </summary>
        /// <param name="Du"></param>
        /// <param name="W"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Vgvs")]
        public decimal CalcVgvs(decimal Du,
            decimal W)
        {
            try
            {
                return Convert.ToDecimal(Math.PI) * Convert.ToDecimal(Math.Pow(Convert.ToDouble(Du), 2)) * W / 4;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Весовой расход газовоздушной смеси
        /// </summary>
        /// <param name="V1gvs"></param>
        /// <param name="RoGvs"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("G")]
        public decimal CalcG(decimal V1gvs,
            string RoGvs)
        {
            try
            {
                int id = _context.GasDensity.Where(d => d.Name == RoGvs).First().Id;
                decimal? Ro = _context.GasDensity.Where(d => d.Id == id).First().Value;
                Ro = Ro == null ? 0 : Ro;
                return V1gvs * Convert.ToDecimal(Ro);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Параметр
        /// </summary>
        /// <param name="W"></param>
        /// <param name="Du"></param>
        /// <param name="R"></param>
        /// <param name="Tsvr"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Fparam")]
        public decimal CalcFparam(decimal W,
            decimal Du,
            decimal R,
            decimal Tsvr)
        {
            try
            {
                return Convert.ToDecimal(Math.Pow(10, 3)) * (((W * W) * Du) / ((R * R) * Tsvr));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Безразмерный коэффициент
        /// </summary>
        /// <param name="Fparam"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Mgvs")]
        public decimal CalcMgvs(decimal Fparam)
        {
            try
            {
                return 1 / (0.67M + 0.1M * Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(Fparam))) + 0.34M * Convert.ToDecimal(Math.Pow(Convert.ToDouble(Fparam), 1 / 3f)));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Коэффициент максимальный
        /// </summary>
        /// <param name="V1gvs"></param>
        /// <param name="Tdelta"></param>
        /// <param name="H"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Um")]
        public decimal CalcUm(decimal V1gvs,
            decimal Tdelta,
            decimal H)
        {
            try
            {
                return 0.65M * Convert.ToDecimal(Math.Pow(((Convert.ToDouble(V1gvs) * Convert.ToDouble(Tdelta)) / Convert.ToDouble(H)), 1/3f));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Безразмерный коэффициент
        /// </summary>
        /// <param name="Um"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Ngvs")]
        public decimal CalcNgvs(decimal Um)
        {
            try
            {
                if (Um < 0.3M || Um == 0.3M)
                {
                    return 3;
                }
                if (Um > 0.3M && Um < 2)
                {
                    return 3 - Convert.ToDecimal(Math.Sqrt((Convert.ToDouble(Um - 0.3M)) * (Convert.ToDouble(4.36M - Um))));
                }
                else
                {
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Предельно допустимый нагретый выброс вредного вещества в атмосферу
        /// </summary>
        /// <param name="PDK"></param>
        /// <param name="H"></param>
        /// <param name="V1gvsov"></param>
        /// <param name="Tdelta"></param>
        /// <param name="Astrat"></param>
        /// <param name="Fgvs"></param>
        /// <param name="Mgvs"></param>
        /// <param name="Ngvs"></param>
        /// <param name="Eta"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("PDV")]
        public decimal CalcPDV(decimal PDK,
            decimal H,
            decimal V1gvsov,
            decimal Tdelta,
            decimal Astrat,
            decimal Fgvs,
            decimal Mgvs,
            decimal Ngvs,
            decimal Eta)
        {
            try
            {
                double root = Convert.ToDouble(V1gvsov * Tdelta),
                    power = Math.Pow(root, 1/3f);
                return (PDK * (H * H) * Convert.ToDecimal(power)) / (Astrat * Fgvs * Mgvs * Ngvs * Eta);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ------------------------------------------ГАЗ_11 (313)---------------------------------------------
        /// 
        /// Коэффициент максимальный (для случая холодного выброса)
        /// </summary>
        /// <param name="W"></param>
        /// <param name="Du"></param>
        /// <param name="H"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("UmHol")]
        public decimal CalcUmHol(decimal W,
            decimal Du,
            decimal H)
        {
            try
            {
                return W * Du / H;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Безразмерный коэффициент (для случая холодного выброса)
        /// </summary>
        /// <param name="UmHol"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("NgvsHol")]
        public decimal CalcNgvsHol(decimal UmHol)
        {
            try
            {
                if (UmHol < 0.3M || UmHol == 0.3M)
                {
                    return 3;
                }
                if (UmHol > 0.3M && UmHol < 2)
                {
                    return 3 - Convert.ToDecimal(Math.Sqrt((Convert.ToDouble(UmHol - 0.3M)) * (Convert.ToDouble(4.36M - UmHol))));
                }
                else
                {
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Предельно допустимый выброс вредных веществ для случая холодного выброса
        /// </summary>
        /// <param name="PDK"></param>
        /// <param name="H"></param>
        /// <param name="V1gvsov"></param>
        /// <param name="Astrat"></param>
        /// <param name="Fgvs"></param>
        /// <param name="Du"></param>
        /// <param name="NgvsHol"></param>
        /// <param name="Eta"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("PDVhol")]
        public decimal CalcPDVhol(decimal PDK,
            decimal H,
            decimal V1gvsov,
            decimal Astrat,
            decimal Fgvs,
            decimal Du,
            decimal NgvsHol,
            decimal Eta)
        {
            try
            {
                return (8 * PDK * H * Convert.ToDecimal(Math.Pow(Convert.ToDouble(H), 1/3f)) * V1gvsov) / (Astrat * Fgvs * Du * NgvsHol * Eta);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ------------------------------------------ГАЗ_12 (314)---------------------------------------------
        /// 
        /// Годовой выброс газа в атмосферу
        /// </summary>
        /// <param name="K"></param>
        /// <param name="RoRGr"></param>
        /// <param name="VrGr"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Gr")]
        public decimal CalcGr(decimal K,
            string RoRGr,
            decimal VrGr)
        {
            try
            {
                int id = _context.GasDensity.Where(d => d.Name == RoRGr).First().Id;
                decimal? Ro = _context.GasDensity.Where(d => d.Id == id).First().Value;
                Ro = Ro == null ? 0 : Ro;
                return K * Convert.ToDecimal(Ro) * VrGr * Convert.ToDecimal(Math.Pow(10, -5)) * Convert.ToDecimal(Math.Pow(10, 9));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Фактические выбросы NOx и CO от ГПА
        /// </summary>
        /// <param name="KiMif"></param>
        /// <param name="Vtr"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MiFact")]
        public decimal[] CalcMiFact(decimal KiMif,
            decimal Vtr)
        {
            decimal[] MiFact = new decimal[2];
            try
            {
                if (KiMif == 1)
                {
                    MiFact[0] = 2.83M * Convert.ToDecimal(Math.Pow(10, -3)) * Vtr;
                    MiFact[1] = 3.12M * Convert.ToDecimal(Math.Pow(10, -3)) * Vtr;
                    return MiFact;
                }
                else
                {
                    MiFact[0] = 8.2M * Convert.ToDecimal(Math.Pow(10, -3)) * Vtr;
                    MiFact[1] = 2.9M * Convert.ToDecimal(Math.Pow(10, -3)) * Vtr;
                    return MiFact;
                }
            }
            catch
            {
                MiFact = null;
                return MiFact;
            }
        }

        /// <summary>
        /// ------------------------------------------ГАЗ_13 (315)---------------------------------------------
        /// </summary>
        /// <param name="Кphg"></param>
        /// <param name="RoPhgKs"></param>
        /// <param name="VgrPhg"></param>
        /// <param name="Vks"></param>
        /// <param name="Qgrs"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GAS13")]
        public ActionResult CalcGAS13(decimal Кphg,
            string RoPhgKs,
            decimal VgrPhg,
            decimal[] Vks,
            decimal Qgrs)
        {
            decimal
                GrPhg = CalcGrPhg(Кphg, RoPhgKs, VgrPhg),
                GrKs = CalcGrKs(Vks, RoPhgKs),
                GrGrs = CalcGrGrs(Qgrs);

            return Json(new
            {
                GrPhg,
                GrKs,
                GrGrs
            });
        }

        /// <summary>
        /// </summary>
        /// <param name="КphgS"></param>
        /// <param name="VphgS"></param>
        /// <param name="MrPhgS"></param>
        /// <param name="VksS"></param>
        /// <param name="MsKsS"></param>
        /// <param name="QgrsS"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GAS13withS")]
        public ActionResult CalcGAS13withS(decimal КphgS,
            decimal VphgS,
            decimal MrPhgS,
            decimal[] VksS,
            decimal MsKsS,
            decimal QgrsS)
        {
            decimal
                GrPhgS = CalcGrPhgS(КphgS, VphgS, MrPhgS),
                GrKsS = CalcGrKsS(VksS, MsKsS),
                GrGrsS = CalcGrGrsS(QgrsS);

            return Json(new
            {
                GrPhgS,
                GrKsS,
                GrGrsS
            });
        }

        /// <summary>
        /// Годовые выбросы газа ПХГ
        /// </summary>
        /// <param name="Кphg"></param>
        /// <param name="RoPhgKs"></param>
        /// <param name="VgrPhg"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GrPhg")]
        public decimal CalcGrPhg(decimal Кphg,
            string RoPhgKs,
            decimal VgrPhg)
        {
            try
            {
                int id = _context.GasDensity.Where(d => d.Name == RoPhgKs).First().Id;
                decimal? Ro = _context.GasDensity.Where(d => d.Id == id).First().Value;
                Ro = Ro == null ? 0 : Ro;
                return Кphg * Convert.ToDecimal(Ro) * VgrPhg * Convert.ToDecimal(Math.Pow(10, 9)) * Convert.ToDecimal(Math.Pow(10, -5));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Годовые выбросы газа КС
        /// </summary>
        /// <param name="Vks"></param>
        /// <param name="RoPhgKs"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GrKs")]
        public decimal CalcGrKs(decimal[] Vks,
            string RoPhgKs)
        {
            try
            {
                int id = _context.GasDensity.Where(d => d.Name == RoPhgKs).First().Id;
                decimal? Ro = _context.GasDensity.Where(d => d.Id == id).First().Value;
                Ro = Ro == null ? 0 : Ro;
                return Convert.ToDecimal(Math.Pow(10, -3)) * Convert.ToDecimal(Ro) * Vks.Sum();
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Годовые выбросы газа ГРС
        /// </summary>
        /// <param name="Qgrs"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GrGrs")]
        public decimal CalcGrGrs(decimal Qgrs)
        {
            try
            {
                return 0.31M * Convert.ToDecimal(Math.Pow(Convert.ToDouble(Qgrs) * Math.Pow(10, 6), 3 / 4f));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Годовые выбросы сераорганических веществ для ПХГ
        /// </summary>
        /// <param name="КphgS"></param>
        /// <param name="VphgS"></param>
        /// <param name="MrPhgS"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GrPhgS")]
        public decimal CalcGrPhgS(decimal КphgS,
            decimal VphgS,
            decimal MrPhgS)
        {
            try
            {
                return КphgS * VphgS * Convert.ToDecimal(Math.Pow(10, 9)) * MrPhgS * Convert.ToDecimal(Math.Pow(10, -8));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Годовые выбросы сераорганических веществ для КС
        /// </summary>
        /// <param name="VksS"></param>
        /// <param name="MsKsS"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GrKsS")]
        public decimal CalcGrKsS(decimal[] VksS,
            decimal MsKsS)
        {
            try
            {
                return Convert.ToDecimal(Math.Pow(10, -6)) * MsKsS * VksS.Sum();
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Годовые выбросы сераорганических веществ для ГРС
        /// </summary>
        /// <param name="QgrsS"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GrGrsS")]
        public decimal CalcGrGrsS(decimal QgrsS)
        {
            try
            {
                return 0.7M * Convert.ToDecimal(Math.Pow(Convert.ToDouble(QgrsS) * Math.Pow(10, 6), 4 / 3f)) * Convert.ToDecimal(Math.Pow(10, -5));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ------------------------------------------ГАЗ_14 (317)---------------------------------------------
        /// </summary>
        /// <param name="Vtg"></param>
        /// <param name="MiTg"></param>
        /// <param name="Vgdg"></param>
        /// <param name="CiDg "></param>
        /// <param name="KiDg "></param>
        /// <param name="CsSa "></param>
        /// <param name="RoSa "></param>
        /// <param name="Vsa"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GAS14")]
        public ActionResult CalcGAS14(decimal[] Vtg,
            decimal MiTg,
            decimal[] Vgdg,
            decimal CiDg,
            decimal KiDg,
            decimal CsSa,
            string RoSa,
            decimal[] Vsa)
        {
            decimal Gtg = CalcGtg(Vtg, MiTg);
            decimal[] Gdg = CalcGdg(Vgdg, CiDg, KiDg);
            decimal Gsa = CalcGsa(CsSa, RoSa, Vsa);

            return Json(new
            {
                Gtg,
                Gdg,
                Gsa
            });
        }

        /// <summary>
        /// Годовые выбросы продуктов сгорания топливного газа (NOх, CO, SО2) при работе j-ro типа ГПА
        /// </summary>
        /// <param name="Vtg"></param>
        /// <param name="MiTg"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Gtg")]
        public decimal CalcGtg(decimal[] Vtg,
            decimal MiTg)
        {
            try
            {
                return Vtg.Sum() * MiTg * Convert.ToDecimal(Math.Pow(10, -6));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Годовые выбросы продуктов сгорания дымовых газов в камерах сгорания ГПА
        /// </summary>
        /// <param name="Vgdg"></param>
        /// <param name="CiDg "></param>
        /// <param name="KiDg "></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Gdg")]
        public decimal[] CalcGdg(decimal[] Vgdg,
            decimal CiDg,
            decimal KiDg)
        {
            decimal[] Gdg = new decimal[2];
            try
            {
                if (KiDg == 1)
                {
                    Gdg[0] = Vgdg.Sum() * 2.83M * Convert.ToDecimal(Math.Pow(10, -3)) * 0.0864M;
                    Gdg[1] = Vgdg.Sum() * 3.12M * Convert.ToDecimal(Math.Pow(10, -3)) * 0.0864M;
                    return Gdg;
                }
                if (KiDg == 2)
                {
                    Gdg[0] = Vgdg.Sum() * 0.2M * Convert.ToDecimal(Math.Pow(10, -4)) * 0.0864M;
                    Gdg[1] = Vgdg.Sum() * 2.3M * Convert.ToDecimal(Math.Pow(10, -3)) * 0.0864M;
                    return Gdg;
                }
                else
                {
                    Gdg[0] = Vgdg.Sum() * CiDg * Convert.ToDecimal(Math.Pow(10, -6));
                    Gdg[1] = 0;
                    return Gdg;
                }
            }
            catch
            {
                Gdg = null;
                return Gdg;
            }
        }

        /// <summary>
        /// Годовые выбросы сернистого ангидрида для топливных газов, содержащих сераорганические соединения
        /// </summary>
        /// <param name="CsSa "></param>
        /// <param name="RoSa "></param>
        /// <param name="Vsa"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Gsa")]
        public decimal CalcGsa(decimal CsSa,
            string RoSa,
            decimal[] Vsa)
        {
            try
            {
                int id = _context.GasDensity.Where(d => d.Name == RoSa).First().Id;
                decimal? Ro = _context.GasDensity.Where(d => d.Id == id).First().Value;
                Ro = Ro == null ? 0 : Ro;
                return 0.025M * Convert.ToDecimal(Math.Pow(10, -2)) * CsSa * Convert.ToDecimal(Ro) * Vsa.Sum();
            }
            catch
            {
                return 0;
            }
        }

        //[HttpPost("Tog")]
        //public decimal CalcTog(decimal Pcp,
        //    decimal Dd,
        //    decimal l,
        //    decimal m)
        //{
        //    try
        //    {
        //        Pcp = 5;
        //        Dd = 5;

        //        decimal lfk = 0.0665M * l - 0.0015M;
        //        decimal lf = 

        //        return 20.5M;
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}
    }
}