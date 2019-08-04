using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleverAPI.Models
{
    public class ParametersEmissionSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal EjectionNOx { get; set; }
        public decimal EjectionCO { get; set; }
    }
}
