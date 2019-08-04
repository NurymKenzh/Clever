using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clever.Models
{
    public class AirContaminant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NumberCAS { get; set; }
        public int? HazardClass { get; set; }
        public decimal? MaximumPermissibleConcentrationOneTimeMaximum { get; set; }
        public decimal? MaximumPermissibleConcentrationDailyAverage { get; set; }
        public decimal? ApproximateSafeExposureLevel { get; set; }
    }
}
