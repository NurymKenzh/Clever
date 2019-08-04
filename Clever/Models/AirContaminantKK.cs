using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clever.Models
{
    public class AirContaminantKK
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(Resources.Controllers.SharedResources), Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "NumberCAS")]
        public string NumberCAS { get; set; }

        [Display(Name = "Formula")]
        public string Formula { get; set; }

        [Display(Name = "MaximumPermissibleConcentrationOneTimeMaximum")]
        [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
        public decimal? MaximumPermissibleConcentrationOneTimeMaximum { get; set; }

        [Display(Name = "MaximumPermissibleConcentrationDailyAverage")]
        [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
        public decimal? MaximumPermissibleConcentrationDailyAverage { get; set; }

        [Display(Name = "HazardClass")]
        public int? HazardClass { get; set; }

        [Display(Name = "Code")]
        public int? Code { get; set; }

        [Display(Name = "ApproximateSafeExposureLevel")]
        [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
        public decimal? ApproximateSafeExposureLevel { get; set; }
    }
}
