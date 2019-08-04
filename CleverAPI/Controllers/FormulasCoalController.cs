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
        /// Расчет зольности из сухого значения
        /// </summary>
        /// <param name="Ac">
        /// Зольность (сухая)
        /// </param>
        /// <param name="Wr">
        /// Содержание влаги
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("ArW")]
        public decimal CalcArW(decimal Ac,
            decimal Wr)
        {
            return Ac * (100 - Wr) / 100;
        }

        /// <summary>
        /// Расчет зольности из рабочего значения в сухое
        /// </summary>
        /// <param name="Wrg1t2">
        /// Содержание влаги
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("RabSuh")]
        public decimal CalcRabSuh(decimal Wrg1t2)
        {
            if (Wrg1t2 == 0)
            {
                return 1;
            }
            else
            {
                return 100 / (100 - Wrg1t2);
            }
        }

        /// <summary>
        /// Расчет зольности из рабочего значения в горючее
        /// </summary>
        /// <param name="ArWg1t3">
        /// Зольность (сухая)
        /// </param>
        /// <param name="Wrg1t3">
        /// Содержание влаги
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("RabGor")]
        public decimal CalcRabGor(decimal ArWg1t3,
            decimal Wrg1t3)
        {
            return 100 / (100 - Wrg1t3 - ArWg1t3);
        }

        /// <summary>
        /// Расчет зольности из сухого значения в рабочее
        /// </summary>
        /// <param name="Wrg2t1">
        /// Содержание влаги
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("SuhRab")]
        public decimal CalcSuhRab(decimal Wrg2t1)
        {
            return (100 - Wrg2t1) / 100;
        }

        /// <summary>
        /// Расчет зольности из сухого значения в горючее
        /// </summary>
        /// <param name="Acg2t3">
        /// Зольность (сухая)
        /// </param>
        /// <param name="Wrg2t3">
        /// Содержание влаги
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("SuhGor")]
        public decimal CalcSuhGor(decimal Acg2t3,
            decimal Wrg2t3)
        {
            return 100 / (100 - CalcArW(Acg2t3, Wrg2t3));
        }

        /// <summary>
        /// Расчет зольности из горючего значения в рабочее
        /// </summary>
        /// <param name="ArWg3t1">
        /// Зольность (сухая)
        /// </param>
        /// <param name="Wrg3t1">
        /// Содержание влаги
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GorRab")]
        public decimal CalcGorRab(decimal ArWg3t1,
            decimal Wrg3t1)
        {
            return (100 - Wrg3t1 - ArWg3t1) / 100;
        }

        /// <summary>
        /// Расчет зольности из горючего значения в сухое
        /// </summary>
        /// <param name="Acg3t2">
        /// Зольность (сухая)
        /// </param>
        /// <param name="Wrg3t2">
        /// Содержание влаги
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("GorSuh")]
        public decimal CalcGorSuh(decimal Acg3t2,
            decimal Wrg3t2)
        {
            return (100 - CalcArW(Acg3t2, Wrg3t2)) / 100;
        }

        /// <summary>
        /// Теплоэнергетика. ТЭС и котельные. Уголь. Замеры при фактических условиях.
        /// </summary>
        /// <param name="Ac"></param>
        /// <param name="Sc"></param>
        /// <param name="Hc"></param>
        /// <param name="Wr"></param>
        /// <param name="Oc"></param>
        /// <param name="Nc"></param>
        /// <param name="Q"></param>
        /// <param name="KPDKA"></param>
        /// <param name="Qri"></param>
        /// <param name="Q4"></param>
        /// <param name="O2"></param>
        /// <param name="CNOx"></param>
        /// <param name="CCO"></param>
        /// <param name="CSO2"></param>
        /// <param name="CTB"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("HeatPowerThermalPowerPlantsCoal")]
        public ActionResult CalcHeatPowerThermalPowerPlantsCoal(decimal Ac,
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
            decimal ArW = CalcArW(Ac, Wr),
                SrW = CalcSrW(Sc, Wr),
                HrW = CalcHrW(Hc, Wr),
                OrW = CalcOrW(Oc, Wr),
                NrW = CalcNrW(Nc, Wr),
                Cr = CalcCr(ArW, SrW, HrW, OrW, NrW, Wr),
                Vro2 = CalcVro2(Cr, SrW),
                V = CalcV(Cr, SrW, HrW, OrW),
                Vn2 = CalcVn2(V, NrW),
                Vh2o = CalcVh2o(HrW, Wr, V),
                Vr = CalcVr(Vro2, Vn2, Vh2o),
                Vcr = CalcVcr(Vr, Vh2o, V),
                B = CalcB(Q, KPDKA, Qri),
                Bp = CalcBp(Q4, B),
                AFact = CalcAFact(O2),
                CjNOx = CalcCjNOx(CNOx, AFact),
                CjCO = CalcCjCO(CCO, AFact),
                CjSO2 = CalcCjSO2(CSO2, AFact),
                CjTB = CalcCjTB(CTB, AFact),
                AAbove = CalcAAbove(ArW, Qri),
                SAbove = CalcSAbove(SrW, Qri),
                MNOx = CalcMNOx(CjNOx, Vcr, Bp),
                MNO2 = CalcMNO2(MNOx),
                MNO = CalcMNO(MNOx),
                MCO = CalcMCO(CjCO, Vcr, Bp),
                MSO2 = CalcMSO2(CjSO2, Vcr, Bp),
                MTB = CalcMTB(CjTB, Vcr, Bp),
                MNOxY = CalcMNOxY(CjNOx, Vcr, Bp, N),
                MNO2Y = CalcMNO2Y(MNOxY),
                MNOY = CalcMNOY(MNOxY),
                MCOY = CalcMCOY(CjCO, Vcr, Bp, N),
                MSO2Y = CalcMSO2Y(CjSO2, Vcr, Bp, N),
                MTBY = CalcMTBY(CjTB, Vcr, Bp, N);


            return Json(new
            {
                ArW,
                SrW,
                HrW,
                OrW,
                NrW,
                Cr,
                Vro2,
                V,
                Vn2,
                Vh2o,
                Vr,
                Vcr,
                B,
                Bp,
                AFact,
                CjNOx,
                CjCO,
                CjSO2,
                CjTB,
                AAbove,
                SAbove,
                MNOx,
                MNO2,
                MNO,
                MCO,
                MSO2,
                MTB,
                MNOxY,
                MNO2Y,
                MNOY,
                MCOY,
                MSO2Y,
                MTBY
            });
        }

        /// <summary>
        /// Теплоэнергетика. ТЭС и котельные. Уголь. Замеры при нормальных условиях.
        /// </summary>
        /// <param name="Ac"></param>
        /// <param name="Sc"></param>
        /// <param name="Hc"></param>
        /// <param name="Wr"></param>
        /// <param name="Oc"></param>
        /// <param name="Nc"></param>
        /// <param name="Q"></param>
        /// <param name="KPDKA"></param>
        /// <param name="Av"></param>
        /// <param name="Noc"></param>
        /// <param name="Qri"></param>
        /// <param name="Q4"></param>
        /// <param name="CNOx"></param>
        /// <param name="CCO"></param>
        /// <param name="CSO2"></param>
        /// <param name="CTB"></param>
        /// <param name="N"></param>
        /// <param name="Df"></param>
        /// <param name="Dn"></param>
        /// <param name="Nzy"></param>
        /// <param name="Z"></param>
        /// <param name="A"></param>
        /// <param name="At"></param>
        /// <param name="Kn"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("HeatPowerThermalPowerPlantsCoalNorm")]
        public ActionResult CalcHeatPowerThermalPowerPlantsCoalNorm(decimal Ac,
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
            decimal ArW = CalcArW(Ac, Wr),
                SrW = CalcSrW(Sc, Wr),
                HrW = CalcHrW(Hc, Wr),
                OrW = CalcOrW(Oc, Wr),
                NrW = CalcNrW(Nc, Wr),
                Cr = CalcCr(ArW, SrW, HrW, OrW, NrW, Wr),
                Vro2 = CalcVro2(Cr, SrW),
                V = CalcV(Cr, SrW, HrW, OrW),
                Vn2 = CalcVn2(V, NrW),
                Vh2o = CalcVh2o(HrW, Wr, V),
                Vr = CalcVr(Vro2, Vn2, Vh2o),
                Vcr = CalcVcr(Vr, Vh2o, V),
                B = CalcB(Q, KPDKA, Qri),
                Bp = CalcBp(Q4, B),
                CjNOx = CalcCjNOx(CNOx, ANorm),
                CjCO = CalcCjCO(CCO, ANorm),
                CjSO2 = CalcCjSO2(CSO2, ANorm),
                CjTB = CalcCjTB(CTB, ANorm),
                Gv = CalcGv(Av, Ac),
                Kd = CalcKd(Df, Dn),
                Kzy = CalcKzy(Nzy, Z),
                CjT = CalcCjT(Df, Dn, Nzy, Z, A, Qri, At),
                AAbove = CalcAAbove(ArW, Qri),
                SAbove = CalcSAbove(SrW, Qri),
                MNOx = CalcMNOx(CjNOx, Vcr, Bp),
                MNO2 = CalcMNO2(MNOx),
                MNO = CalcMNO(MNOx),
                MCO = CalcMCO(CjCO, Vcr, Bp),
                MSO2 = CalcMSO2(CjSO2, Vcr, Bp),
                MTB = CalcMTB(CjTB, Vcr, Bp),
                MNOxY = CalcMNOxY(CjNOx, Vcr, Bp, N),
                MNO2Y = CalcMNO2Y(MNOxY),
                MNOY = CalcMNOY(MNOxY),
                MCOY = CalcMCOY(CjCO, Vcr, Bp, N),
                MSO2Y = CalcMSO2Y(CjSO2, Vcr, Bp, N),
                MTBY = CalcMTBY(CjTB, Vcr, Bp, N),
                MZMY = CalcMZMY(Av, Ac, Bp, Noc, Kn);


            return Json(new
            {
                ArW,
                SrW,
                HrW,
                OrW,
                NrW,
                Cr,
                Vro2,
                V,
                Vn2,
                Vh2o,
                Vr,
                Vcr,
                B,
                Bp,
                CjNOx,
                CjCO,
                CjSO2,
                CjTB,
                Gv,
                Kd,
                Kzy,
                CjT,
                AAbove,
                SAbove,
                MNOx,
                MNO2,
                MNO,
                MCO,
                MSO2,
                MTB,
                MNOxY,
                MNO2Y,
                MNOY,
                MCOY,
                MSO2Y,
                MTBY,
                MZMY
            });
        }

        /// <summary>
        /// Теплоэнергетика. ТЭС и котельные. Уголь. Замеры при фактических условиях и при использовании приборов, измеряющих объемную концентрацию
        /// </summary>
        /// <param name="Ac"></param>
        /// <param name="Sc"></param>
        /// <param name="Hc"></param>
        /// <param name="Wr"></param>
        /// <param name="Oc"></param>
        /// <param name="Nc"></param>
        /// <param name="Q"></param>
        /// <param name="KPDKA"></param>
        /// <param name="Qri"></param>
        /// <param name="Q4"></param>
        /// <param name="O2"></param>
        /// <param name="N"></param>
        /// <param name="INO2"></param>
        /// <param name="ICO"></param>
        /// <param name="ISO2"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("HeatPowerThermalPowerPlantsCoalZ")]
        public ActionResult CalcHeatPowerThermalPowerPlantsCoalZ(decimal Ac,
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
            decimal ArW = CalcArW(Ac, Wr),
                SrW = CalcSrW(Sc, Wr),
                HrW = CalcHrW(Hc, Wr),
                OrW = CalcOrW(Oc, Wr),
                NrW = CalcNrW(Nc, Wr),
                Cr = CalcCr(ArW, SrW, HrW, OrW, NrW, Wr),
                Vro2 = CalcVro2(Cr, SrW),
                V = CalcV(Cr, SrW, HrW, OrW),
                Vn2 = CalcVn2(V, NrW),
                Vh2o = CalcVh2o(HrW, Wr, V),
                Vr = CalcVr(Vro2, Vn2, Vh2o),
                Vcr = CalcVcr(Vr, Vh2o, V),
                B = CalcB(Q, KPDKA, Qri),
                Bp = CalcBp(Q4, B),
                AFact = CalcAFact(O2),

                CjZNO2 = CalcCjZNO2(INO2, AFact),
                CjZNO = CalcCjZNO(INO, AFact),
                CjZCO = CalcCjZCO(ICO, AFact),
                CjZSO2 = CalcCjZSO2(ISO2, AFact),

                AAbove = CalcAAbove(ArW, Qri),
                SAbove = CalcSAbove(SrW, Qri),
                //
                MNO2 = CalcMNO2Z(CjZNO2, Vcr, Bp),
                MNO = CalcMNOZ(CjZNO, Vcr, Bp),
                MCO = CalcMCO(CjZCO, Vcr, Bp),
                MSO2 = CalcMSO2(CjZSO2, Vcr, Bp),
                //
                MNO2Y = CalcMNO2YZ(CjZNO2, Vcr, Bp, N),
                MNOY = CalcMNOYZ(CjZNO, Vcr, Bp, N),
                MCOY = CalcMCOY(CjZCO, Vcr, Bp, N),
                MSO2Y = CalcMSO2Y(CjZSO2, Vcr, Bp, N);


            return Json(new
            {
                ArW,
                SrW,
                HrW,
                OrW,
                NrW,
                Cr,
                Vro2,
                V,
                Vn2,
                Vh2o,
                Vr,
                Vcr,
                B,
                Bp,
                AFact,
                CjZNO2,
                CjZNO,
                CjZCO,
                CjZSO2,
                AAbove,
                SAbove,
                //MNOx,
                MNO2,
                MNO,
                MCO,
                MSO2,
                //MNOxY,
                MNO2Y,
                MNOY,
                MCOY,
                MSO2Y
            });
        }

        /// <summary>
        /// Газ. Расчёт выбросов объёма газа
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
        /// <param name="Pcp"></param>
        /// <param name="Dd"></param>
        /// <param name="Togm"></param>
        /// <param name="Vsr"></param>
        /// <param name="D1"></param>
        /// <param name="Bg"></param>
        /// <param name="Mi"></param>
        /// <param name="Bgs"></param>
        /// <param name="Dgs"></param>
        /// <param name="E"></param>
        /// <param name="Ki"></param>
        /// <param name="Ch2s"></param>
        /// <param name="Bgf"></param>
        /// <param name="O2dg"></param>
        /// <param name="Ve"></param>
        /// <param name="Vb"></param>
        /// <param name="Bpg"></param>
        /// <param name="MiUv"></param>
        /// <param name="Vi"></param>
        /// <param name="Ti"></param>
        /// <param name="KiO"></param>
        /// <param name="Ni"></param>
        /// <param name="R"></param>
        /// <param name="Tsvr"></param>
        /// <param name="RoGvs"></param>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        /// <param name="Du"></param>
        /// <param name="Fgvs"></param>
        /// <param name="Astrat"></param>
        /// <param name="Tdelta"></param>
        /// <param name="V1gvsov"></param>
        /// <param name="H"></param>
        /// <param name="PDK"></param>
        /// <param name="Eta"></param>
        /// <param name="K"></param>
        /// <param name="RoRGr"></param>
        /// <param name="VrGr"></param>
        /// <param name="KiMif"></param>
        /// <param name="Vtr"></param>
        /// <param name="Q3"></param>
        /// <param name="Rdpt"></param>
        /// <param name="Qnr"></param>
        /// <param name="Dyx"></param>
        /// <param name="Psi"></param>
        /// <param name="Ups"></param>
        /// <param name="CcoDg"></param>
        /// <param name="Bt"></param>
        /// <param name="Qsnr"></param>
        /// <param name="Yco"></param>
        /// <param name="Nfact"></param>
        /// <param name="Nnom"></param>
        /// <param name="Dfact"></param>
        /// <param name="Dnom"></param>
        /// <param name="Yno2Nom "></param>
        /// <param name="DyxNo2 "></param>
        /// <param name="Cno2 "></param>
        /// <param name="PsiNo2 "></param>
        /// <param name="UpsNo2 "></param>
        /// <param name="Bno2"></param>
        /// <param name="QrnNo2"></param>
        /// <param name="Кphg"></param>
        /// <param name="RoPhgKs"></param>
        /// <param name="VgrPhg"></param>
        /// <param name="Vks"></param>
        /// <param name="Qgrs"></param>
        /// <param name="КphgS"></param>
        /// <param name="VphgS"></param>
        /// <param name="MrPhgS"></param>
        /// <param name="VksS"></param>
        /// <param name="MsKsS"></param>
        /// <param name="QgrsS"></param>
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
        [HttpPost("GasVolumeEmissionValues")]
        public ActionResult CalcGasVolumeEmissionValues(decimal Do,
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
            decimal[] MiVv = CalcMiVv(MiUv, Vi);
            decimal Ss = CalcSs(Do),
                Vk = CalcVk(Ss, L),
                Vstr = CalcVstr(Vk, Pa, t0, Po, tn, Pz, Delta, Tz),
                V1 = CalcV1(D, Np, P, t, Tk, Ror),
                VkKs = CalcVk(Ss, L),
                Vsst = CalcVsst(Vk, Pa, t0, Po, tn, Pz, Delta, Tz),
                V1Ks = CalcV1Ks(Vsst, Pcp, Dd, L, Togm),
                G1 = CalcG1(Ror, V1Ks),
                Vphg = CalcVphg(Vsr),
                Vvg = CalcVvg(D1, Bg),
                Gi = CalcGi(MiVv, Ti, KiO, Ni),
                Vdg = CalcVdg(Bgs, Dgs, E),
                Nso2 = CalcNso2(Ch2s, Bgf),
                Ddg = CalcDdg(O2dg),
                V1vp = CalcV1vp(Ve, Vb, Ddg),
                Vpg = CalcVpg(Bpg, V1vp),
                W = CalcW(i1, i2),
                F = CalcF(Du),
                V1gvs = CalcV1gvs(F, W),
                Vgvs = CalcVgvs(Du, W),
                Fparam = CalcFparam(W, Du, R, Tsvr),
                G = CalcG(V1gvs, RoGvs),
                Mgvs = CalcMgvs(Fparam),
                Um = CalcUm(V1gvs, Tdelta, H),
                Ngvs = CalcNgvs(Um),
                PDV = CalcPDV(PDK, H, V1gvsov, Tdelta, Astrat, Fgvs, Mgvs, Ngvs, Eta),
                UmHol = CalcUmHol(W, Du, H),
                NgvsHol = CalcNgvsHol(UmHol),
                PDVhol = CalcPDVhol(PDK, H, V1gvsov, Astrat, Fgvs, Du, NgvsHol, Eta),
                Gr = CalcGr(K, RoRGr, VrGr),
                CcoSgt = CalcCcoSgt(Q3, Rdpt, Qnr),
                GwDg = CalcGwDg(CcoDg, Bt),
                Gw = CalcGw(Bt, Qsnr, Yco, Dyx, CcoSgt, Psi, Ups),
                Yno2 = CalcYno2(Nfact, Nnom, Dfact, Dnom, Yno2Nom, DyxNo2, Cno2, PsiNo2, UpsNo2),
                Gno2 = CalcGno2(Bno2, QrnNo2, Yno2),
                GrPhg = CalcGrPhg(Кphg, RoPhgKs, VgrPhg),
                GrKs = CalcGrKs(Vks, RoPhgKs),
                GrGrs = CalcGrGrs(Qgrs),
                GrPhgS = CalcGrPhgS(КphgS, VphgS, MrPhgS),
                GrKsS = CalcGrKsS(VksS, MsKsS),
                GrGrsS = CalcGrGrsS(QgrsS),
                Gtg = CalcGtg(Vtg, MiTg),
                Gsa = CalcGsa(CsSa, RoSa, Vsa);

            decimal[]
                Vgf = CalcVgf(Ki, Bgf),
                MiFact = CalcMiFact(KiMif, Vtr),
                Gdg = CalcGdg(Vgdg, CiDg, KiDg),
                M = CalcM(Mi, Vvg);

            return Json(new
            {
                Vstr,
                V1,
                Vsst,
                V1Ks,
                G1,
                Vphg,
                Vvg,
                Gi,
                M,
                Vdg,
                Vgf,
                Nso2,
                Ddg,
                V1vp,
                Vpg,
                W,
                F,
                V1gvs,
                Vgvs,
                Fparam,
                G,
                Mgvs,
                Um,
                Ngvs,
                PDV,
                UmHol,
                NgvsHol,
                PDVhol,
                Gr,
                MiFact,
                CcoSgt,
                GwDg,
                Gw,
                Yno2,
                Gno2,
                GrPhg,
                GrKs,
                GrGrs,
                GrPhgS,
                GrKsS,
                GrGrsS,
                Gtg,
                Gdg,
                Gsa
            });
        }



        /// <summary>
        /// Расчет содержания серы из сухого
        /// </summary>
        /// <param name="Sc">
        /// Содержание серы (сухое)
        /// </param>
        /// <param name="Wr">
        /// Содержание влаги
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("SrW")]
        public decimal CalcSrW(decimal Sc,
            decimal Wr)
        {
            return Sc * (100 - Wr) / 100;
        }

        /// <summary>
        /// Расчет содержания водорода из сухого
        /// </summary>
        /// <param name="Hc">
        /// Содержание водорода (сухое)
        /// </param>
        /// <param name="Wr">
        /// Содержание влаги
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("HrW")]
        public decimal CalcHrW(decimal Hc,
            decimal Wr)
        {
            return Hc * (100 - Wr) / 100;
        }

        /// <summary>
        /// Расчет содержания кислорода из сухого
        /// </summary>
        /// <param name="Oc">
        /// Содержание кислорода (сухое)
        /// </param>
        /// <param name="Wr">
        /// Содержание влаги
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("OrW")]
        public decimal CalcOrW(decimal Oc,
            decimal Wr)
        {
            return Oc * (100 - Wr) / 100;
        }

        /// <summary>
        /// Расчет содержания азота из сухого
        /// </summary>
        /// <param name="Nc">
        /// Содержание азота (сухое)
        /// </param>
        /// <param name="Wr">
        /// Содержание влаги
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("NrW")]
        public decimal CalcNrW(decimal Nc,
            decimal Wr)
        {
            return Nc * (100 - Wr) / 100;
        }

        /// <summary>
        /// Расчет содержания углерода
        /// </summary>
        /// <param name="ArW">
        /// Зольность (рабочая)
        /// </param>
        /// <param name="SrW">
        /// Содержание серы (рабочее)
        /// </param>
        /// <param name="HrW">
        /// Содержание водорода (рабочее)
        /// </param>
        /// <param name="OrW">
        /// Содержание кислорода (рабочее)
        /// </param>
        /// <param name="NrW">
        /// Содержание азота (рабочее)
        /// </param>
        /// <param name="Wr">
        /// Содержание влаги
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Cr")]
        public decimal CalcCr(decimal ArW,
            decimal SrW,
            decimal HrW,
            decimal OrW,
            decimal NrW,
            decimal Wr)
        {
            return 100 - ArW - SrW - HrW - OrW - NrW - Wr;
        }

        /// <summary>
        /// Объем трехатомных газов
        /// </summary>
        /// <param name="Cr">
        /// Содержание углерода (рабочее)
        /// </param>
        /// <param name="SrW">
        /// Содержание серы (рабочее)
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Vro2")]
        public decimal CalcVro2(decimal Cr,
            decimal SrW)
        {
            return 1.866M * (Cr + 0.375M * SrW) / 100;
        }

        /// <summary>
        /// Теоретическое количество сухого воздуха
        /// </summary>
        /// <param name="Cr">
        /// Содержание углерода (рабочее)
        /// </param>
        /// <param name="SrW">
        /// Содержание серы (рабочее)
        /// </param>
        /// <param name="HrW">
        /// Содержание водорода (рабочее)
        /// </param>
        /// <param name="OrW">
        /// Содержание кислорода (рабочее)
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("V")]
        public decimal CalcV(decimal Cr,
            decimal SrW,
            decimal HrW,
            decimal OrW)
        {
            return 0.0889M * (Cr + 0.375M * SrW) + 0.265M * HrW - 0.0333M * OrW;
        }

        /// <summary>
        /// Объем азота
        /// </summary>
        /// <param name="V">
        /// Теоретическое количество сухого воздуха
        /// </param>
        /// <param name="NrW">
        /// Содержание азота (рабочее)
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Vn2")]
        public decimal CalcVn2(decimal V,
            decimal NrW)
        {
            return 0.79M * V + 0.8M * NrW / 100;
        }

        /// <summary>
        /// Объем водяных паров
        /// </summary>
        /// <param name="HrW">
        /// Содержание водорода (рабочее)
        /// </param>
        /// <param name="Wr">
        /// Содержание влаги
        /// </param>
        /// <param name="V">
        /// Теоретическое количество сухого воздуха
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Vh2o")]
        public decimal CalcVh2o(decimal HrW,
            decimal Wr,
            decimal V)
        {
            return 0.111M * HrW + 0.0124M * Wr + 0.0161M * V;
        }

        /// <summary>
        /// Теоретическое количество дымовых газов
        /// </summary>
        /// <param name="Vro2">
        /// Объем трехатомных газов
        /// </param>
        /// <param name="Vn2">
        /// Объем азота
        /// </param>
        /// <param name="Vh2o">
        /// Объем водяных паров
        /// </param>
        /// <param name="V">
        /// Теоретическое количество сухого воздуха
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Vr")]
        public decimal CalcVr(decimal Vro2,
            decimal Vn2,
            decimal Vh2o)
        {
            return Vro2 + Vn2 + Vh2o;
        }

        /// <summary>
        /// Объем сухих дымовых газов
        /// </summary>
        /// <param name="Vr">
        /// Теоретическое количество дымовых газов
        /// </param>
        /// <param name="Vh2o">
        /// Объем водяных паров
        /// </param>
        /// <param name="V">
        /// Теоретическое количество сухого воздуха
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Vcr")]
        public decimal CalcVcr(decimal Vr,
            decimal Vh2o,
            decimal V)
        {
            return Vr - Vh2o + (1.4M - 1) * V * 0.984M;
        }

        /// <summary>
        /// Полный расход топлива на котел
        /// </summary>
        /// <param name="Q">
        /// Полная нагрузка котла
        /// </param>
        /// <param name="KPDKA">
        /// КПД котлоагрегата
        /// </param>
        /// <param name="Qri">
        /// Теплота сгорания натурального топлива (низшая)
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("B")]
        public decimal CalcB(decimal Q,
            decimal KPDKA,
            decimal Qri)
        {
            return Q / KPDKA / Qri * 1000;
        }

        /// <summary>
        /// Расчетный расход топлива
        /// </summary>
        /// <param name="Q4">
        /// Потери тепла вследствие механического недожога топлива
        /// </param>
        /// <param name="B">
        /// Полный расход топлива на котел
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Bp")]
        public decimal CalcBp(decimal Q4,
            decimal B)
        {
            return (1 - Q4 / 100) * B;
        }

        /// <summary>
        /// Коэффициент избытка воздуха в месте отбора пробы
        /// </summary>
        /// <param name="OrW">
        /// Измеренная концентрация кислорода в месте отбора пробы дымовых газов
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("AFact")]
        public decimal CalcAFact(decimal OrW)
        {
            return 21 / (21 - OrW);
        }

        /// <summary>
        /// Массовая концентрация NOx
        /// </summary>
        /// <param name="CNOx">
        /// Массовая доля NOx
        /// </param>
        /// <param name="AFact">
        /// Коэффициент избытка воздуха в месте отбора пробы
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("CjNOx")]
        public decimal CalcCjNOx(decimal CNOx,
            decimal AFact)
        {
            return CNOx * AFact / 1.4M;
        }

        /// <summary>
        /// Массовая концентрация CO
        /// </summary>
        /// <param name="CCO">
        /// Массовая доля CO
        /// </param>
        /// <param name="AFact">
        /// Коэффициент избытка воздуха в месте отбора пробы
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("CjCO")]
        public decimal CalcCjCO(decimal CCO,
            decimal AFact)
        {
            return CCO * AFact / 1.4M;
        }

        /// <summary>
        /// Массовая концентрация SO2
        /// </summary>
        /// <param name="CSO2">
        /// Массовая доля SO2
        /// </param>
        /// <param name="AFact">
        /// Коэффициент избытка воздуха в месте отбора пробы
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("CjSO2")]
        public decimal CalcCjSO2(decimal CSO2,
            decimal AFact)
        {
            return CSO2 * AFact / 1.4M;
        }

        /// <summary>
        /// Массовая концентрация TB
        /// </summary>
        /// <param name="CTB">
        /// Массовая доля TB
        /// </param>
        /// <param name="AFact">
        /// Коэффициент избытка воздуха в месте отбора пробы
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("CjTB")]
        public decimal CalcCjTB(decimal CTB,
            decimal AFact)
        {
            return CTB * AFact / 1.4M;
        }

        /// <summary>
        /// Массовая концентрация NO2 (при использовании приборов, измеряющих объемную концентрацию)
        /// </summary>
        /// <param name="INO2"></param>
        /// <param name="AFact"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("CjZNO2")]
        public decimal CalcCjZNO2(decimal INO2,
            decimal AFact)
        {
            return INO2 * PNO2 * AFact / ANorm;
        }

        /// <summary>
        /// Массовая концентрация NO (при использовании приборов, измеряющих объемную концентрацию)
        /// </summary>
        /// <param name="INO"></param>
        /// <param name="AFact"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("CjZNO")]
        public decimal CalcCjZNO(decimal INO,
            decimal AFact)
        {
            return INO * PNO * AFact / ANorm;
        }

        /// <summary>
        /// Массовая концентрация CO (при использовании приборов, измеряющих объемную концентрацию)
        /// </summary>
        /// <param name="ICO"></param>
        /// <param name="AFact"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("CjZCO")]
        public decimal CalcCjZCO(decimal ICO,
            decimal AFact)
        {
            return ICO * PCO * AFact / ANorm;
        }

        /// <summary>
        /// Массовая концентрация SO2 (при использовании приборов, измеряющих объемную концентрацию)
        /// </summary>
        /// <param name="ISO2"></param>
        /// <param name="AFact"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("CjZSO2")]
        public decimal CalcCjZSO2(decimal ISO2,
            decimal AFact)
        {
            return ISO2 * PSO2 * AFact / ANorm;
        }

        /// <summary>
        /// Коэффициент, учитывающий нагрузку котла
        /// </summary>
        /// <param name="Df"></param>
        /// Фактическая нагрузка котла
        /// <param name="Dn"></param>
        /// Номинальная нагрузка котла
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Kd")]
        public decimal CalcKd(decimal Df,
            decimal Dn)
        {
            return Df / Dn * 1.1M;
        }

        /// <summary>
        /// Коэффициент, учитывающий степень улавливания бензапирена золоуловителями
        /// </summary>
        /// <param name="Nzy "></param>
        /// КПД золоулавливания
        /// <param name="Z"></param>
        /// Коэффициент, учитывающий снижение улавливающей способности бензапирена ЗУУ
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Kzy")]
        public decimal CalcKzy(decimal Nzy,
            decimal Z)
        {
            return 1 - Nzy * Z / 100;
        }

        /// <summary>
        /// Концентрация бензапирена в сухих дымовых газах
        /// </summary>
        /// <param name="Df"></param>
        /// Фактическая нагрузка котла
        /// <param name="Dn"></param>
        /// Номинальная нагрузка котла
        /// <param name="Nzy "></param>
        /// КПД золоулавливания
        /// <param name="Z"></param>
        /// Коэффициент, учитывающий снижение улавливающей способности бензапирена ЗУУ
        /// <param name="A"></param>
        /// Коэффициент, характеризующий конструкцию нижней части топки
        /// <param name="Qri"></param>
        /// Теплота сгорания натурального топлива (низшая)
        /// <param name="At"></param>
        /// Коэффициент избытка воздуха в продуктах сгорания на выходе из топки
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("CjT")]
        public decimal CalcCjT(decimal Df,
            decimal Dn,
            decimal Nzy,
            decimal Z,
            decimal A,
            decimal Qri,
            decimal At)
        {
            decimal AValue;
            if (A == 1)
            {
                AValue = 0.378M;
            }
            else
            {
                AValue = 0.521M;
            }
            return CalcKd(Df, Dn) * CalcKzy(Nzy, Z) * AValue * Qri / Convert.ToDecimal(Math.Exp(1.5 * Convert.ToDouble(At)));
        }

        /// <summary>
        /// Приведенное содержание золы, %*кг/МДж
        /// </summary>
        /// <param name="ArW">
        /// Зольность (рабочая)
        /// </param>
        /// <param name="Qri">
        /// Теплота сгорания натурального топлива (низшая)
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("AAbove")]
        public decimal CalcAAbove(decimal ArW,
            decimal Qri)
        {
            return ArW / (Qri / 239.006M);
        }

        /// <summary>
        /// Приведенное содержание серы, %*кг/МДж
        /// </summary>
        /// <param name="SrW">
        /// Содержание серы (рабочее)
        /// </param>
        /// <param name="Qri">
        /// Теплота сгорания натурального топлива (низшая)
        /// </param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("SAbove")]
        public decimal CalcSAbove(decimal SrW,
            decimal Qri)
        {
            try
            {
                return SrW / (Qri / 239.006M);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Выброс NOx
        /// </summary>
        /// <param name="CjNOx"></param>
        /// <param name="Vcr"></param>
        /// <param name="Bp"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MNOx")]
        public decimal CalcMNOx(decimal CjNOx,
            decimal Vcr,
            decimal Bp)
        {
            return CjNOx * Vcr * Bp * 0.000278M;
        }

        /// <summary>
        /// Выброс NO2
        /// </summary>
        /// <param name="MNOx"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MNO2")]
        public decimal CalcMNO2(decimal MNOx)
        {
            return 0.8M * MNOx;
        }

        /// <summary>
        /// Выброс NO
        /// </summary>
        /// <param name="MNOx"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MNO")]
        public decimal CalcMNO(decimal MNOx)
        {
            return 0.13M * MNOx;
        }

        /// <summary>
        /// Выброс NO2Z
        /// </summary>
        // GET: api/Formulas
        [HttpPost("MNO2Z")]
        public decimal CalcMNO2Z(decimal CjZNO2,
            decimal Vcr,
            decimal Bp)
        {
            return CjZNO2 * Vcr * Bp * 0.000278M;
        }

        /// <summary>
        /// Выброс NOZ
        /// </summary>
        // GET: api/Formulas
        [HttpPost("MNOZ")]
        public decimal CalcMNOZ(decimal CjZNO,
            decimal Vcr,
            decimal Bp)
        {
            return CjZNO * Vcr * Bp * 0.000278M;
        }

        /// <summary>
        /// Выброс CO
        /// </summary>
        /// <param name="CjCO"></param>
        /// <param name="Vcr"></param>
        /// <param name="Bp"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MCO")]
        public decimal CalcMCO(decimal CjCO,
            decimal Vcr,
            decimal Bp)
        {
            return CjCO * Vcr * Bp * 0.000278M;
        }

        /// <summary>
        /// Выброс SO2
        /// </summary>
        /// <param name="CjSO2"></param>
        /// <param name="Vcr"></param>
        /// <param name="Bp"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MSO2")]
        public decimal CalcMSO2(decimal CjSO2,
            decimal Vcr,
            decimal Bp)
        {
            return CjSO2 * Vcr * Bp * 0.000278M;
        }

        /// <summary>
        /// Выброс пыли неорганической
        /// </summary>
        /// <param name="CjTB"></param>
        /// <param name="Vcr"></param>
        /// <param name="Bp"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MTB")]
        public decimal CalcMTB(decimal CjTB,
            decimal Vcr,
            decimal Bp)
        {
            return CjTB * Vcr * Bp * 0.000278M;
        }

        /// <summary>
        /// Годовой выброс NOx
        /// </summary>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MNOxY")]
        public decimal CalcMNOxY(decimal CjNOx,
            decimal Vcr,
            decimal Bp,
            decimal N)
        {
            return 0.000001M * CjNOx * Vcr * Bp * N;
        }

        /// <summary>
        /// Годовой выброс NO2
        /// </summary>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MNO2Y")]
        public decimal CalcMNO2Y(decimal MNOxY)
        {
            return 0.8M * MNOxY;
        }

        /// <summary>
        /// Годовой выброс NO
        /// </summary>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MNOY")]
        public decimal CalcMNOY(decimal MNOxY)
        {
            return 0.13M * MNOxY;
        }

        /// <summary>
        /// Годовой выброс NO2
        /// </summary>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MNO2YZ")]
        public decimal CalcMNO2YZ(decimal CjZNO2,
            decimal Vcr,
            decimal Bp,
            decimal N)
        {
            return 0.000001M * CjZNO2 * Vcr * Bp * N;
        }

        /// <summary>
        /// Годовой выброс NO
        /// </summary>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MNOYZ")]
        public decimal CalcMNOYZ(decimal CjZNO,
            decimal Vcr,
            decimal Bp,
            decimal N)
        {
            return 0.000001M * CjZNO * Vcr * Bp * N;
        }

        /// <summary>
        /// Годовой выброс CO
        /// </summary>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MCOY")]
        public decimal CalcMCOY(decimal CjCO,
            decimal Vcr,
            decimal Bp,
            decimal N)
        {
            return 0.000001M * CjCO * Vcr * Bp * N;
        }

        /// <summary>
        /// Годовой выброс SO2
        /// </summary>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MSO2Y")]
        public decimal CalcMSO2Y(decimal CjSO2,
            decimal Vcr,
            decimal Bp,
            decimal N)
        {
            return 0.000001M * CjSO2 * Vcr * Bp * N;
        }

        /// <summary>
        /// Годовой выброс пыли неорганической
        /// </summary>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MTBY")]
        public decimal CalcMTBY(decimal CjTB,
            decimal Vcr,
            decimal Bp,
            decimal N)
        {
            return 0.000001M * CjTB * Vcr * Bp * N;
        }

        /// <summary>
        /// Мазутная зола в пересчете на ванадий, г/тонн
        /// </summary>
        /// <param name="Av"></param>
        /// <param name="Ac"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("Gv")]
        public decimal CalcGv(decimal Av,
            decimal Ac)
        {
            if (Av != 0)
            {
                return Av * 10000;
            }
            else
            {
                return 2222 * Ac / 3600;
            }
        }

        /// <summary>
        /// Годовой выброс мазутной золы
        /// </summary>
        /// <param name="Av"></param>
        /// <param name="Ac"></param>
        /// <param name="Bp"></param>
        /// <param name="Noc"></param>
        /// <param name="Kn"></param>
        /// <returns></returns>
        // GET: api/Formulas
        [HttpPost("MZMY")]
        public decimal CalcMZMY(decimal Av,
            decimal Ac,
            decimal Bp,
            decimal Noc,
            decimal Kn)
        {
            decimal NocValue, KnValue;
            if (Noc == 1)
            {
                NocValue = 0.07M;
            }
            else
            {
                NocValue = 0.05M;
            }
            if (Kn == 1)
            {
                KnValue = 0.000278M;
            }
            else
            {
                KnValue = 0.000001M;
            }
            return CalcGv(Av, Ac) * Bp * (1 - NocValue) * (1 - Nyy / 100) * KnValue;
        }
    }
}