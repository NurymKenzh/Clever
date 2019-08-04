using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleverAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleverAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Formulas")]
    public partial class FormulasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormulasController(ApplicationDbContext context)
        {
            _context = context;
        }

        private const decimal O2Norm = 6M,
            ANorm = 1.4M,
            PNO2 = 2.05M,
            PNO = 1.34M,
            PCO = 1.25M,
            PSO2 = 2.85M,
            K = 10M,
            Nyy = 0.998M;
    }
}