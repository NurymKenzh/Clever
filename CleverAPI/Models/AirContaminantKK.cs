using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleverAPI.Models
{
    public class AirContaminantKK
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NumberCAS { get; set; }
        public string Formula { get; set; }
        public decimal? MaximumPermissibleConcentrationOneTimeMaximum { get; set; }
        public decimal? MaximumPermissibleConcentrationDailyAverage { get; set; }
        public int? HazardClass { get; set; }
        public int? Code { get; set; }
        public decimal? ApproximateSafeExposureLevel { get; set; }
    }
}
