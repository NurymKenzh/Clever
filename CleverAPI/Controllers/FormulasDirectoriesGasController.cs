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
        [HttpPost("Tog")]
        public decimal CalcTog(decimal Pcp,
            decimal Dd,
            decimal L,
            decimal Togm)
        {
            try
            {
                decimal Ddk1 = -0.0544M * Dd * Dd + 0.3727M * Dd - 0.3163M,
                    Ddk2 = 6.6531M * (decimal)Math.Pow((double)Dd, -2.045),
                    DdY = Pcp * 2,
                    DdX = (decimal)Math.Log((double)DdY / (double)Ddk1) / Ddk2,
                    lk = 0.0669M * L - 0.0054M,
                    lY = lk * DdX;
                decimal Tog = 0;
                if (Togm == 0.4M)
                {
                    Tog = 20 * lY;
                }
                if (Togm == 0.6M)
                {
                    Tog = 14.545M * lY;
                }
                return Tog;
            }
            catch
            {
                return 0;
            }
        }

        [HttpPost("Zg")]
        public decimal CalcZg(decimal Pz,
            decimal Delta,
            decimal Tz)
        {
            try
            {
                decimal DeltaK = -26.889M * Convert.ToDecimal(Math.Pow(Convert.ToDouble(Delta), 3)) + 49.857M * (Delta * Delta) - 32.116M * Delta + 6.4426M,
                    DeltaY = DeltaK * Pz + 59.2445M,
                    Tk = 0.000000001M * Convert.ToDecimal(Math.Pow(Convert.ToDouble(Tz), 5)) - 0.0000002M * Convert.ToDecimal(Math.Pow(Convert.ToDouble(Tz), 4)) + 0.00001M * Convert.ToDecimal(Math.Pow(Convert.ToDouble(Tz), 3)) + 0.000007M * (Tz * Tz) + 0.0035M * Tz + 0.6589M,
                    Tk2 = (-0.0053M * (Tz * Tz) - 0.313M * Tz + 19.17M) * -1 + 8,
                    Tx = Tk * DeltaY + Tk2;
                if (Tx < 60)
                {
                    decimal TVur = -0.0114M * (Tx * Tx) + 1.1637M * Tx + 3.3877M,
                        Zg = 0.0025M * TVur + 0.85M;
                    return Zg;
                }
                else
                {
                    decimal TVur = 0.0051M * (Tx * Tx) - 0.0127M * Tx + 15.334M,
                        Zg = 0.0025M * TVur + 0.85M;
                    return Zg;
                }
            }
            catch
            {
                return 0;
            }
        }

        [HttpPost("Vgraf")]
        public decimal CalcVgraf(decimal Pv,
            decimal Dv,
            decimal Lv)
        {
            try
            {
                if(Dv == 720)
                {
                    decimal Vk = 0.004M * Pv - 0.002M,
                        Vk2 = -0.001M * Pv - 0.146M,
                        Vgraf = Vk * Lv + Vk2;
                    return Vgraf;
                }
                if(Dv == 1020)
                {
                    decimal Vk = 0.0087M * Pv - 0.0053M,
                        Vk2 = -0.4832M * (Pv * Pv) + 5.4147M * Pv - 15.261M,
                        Vgraf = (Vk * Lv - Vk2) - 1.47M;
                    return Vgraf;
                }
                if(Dv == 1220)
                {
                    decimal Vk = 0.01M * Pv - 0.0038M,
                        Vk2 = 0.0167M * (Pv * Pv) - 0.117M * Pv + 0.1008M,
                        Vgraf = Vk * Lv + Vk2;
                    return Vgraf;
                }
                if(Dv == 1420)
                {
                    decimal Vk = 0.0174M * Pv - 0.0188M,
                        Vk2 = 0.0167M * (Pv * Pv) - 0.1832M * Pv + 0.1662M,
                        Vgraf = Vk * Lv + Vk2;
                    return Vgraf;
                }
                else
                {
                    decimal Vgraf = 0;
                    return Vgraf;
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}