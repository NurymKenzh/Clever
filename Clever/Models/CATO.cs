using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clever.Models
{
    public class CATO
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(Resources.Controllers.SharedResources), Name = "Code")]
        public string Code { get; set; }

        [Display(ResourceType = typeof(Resources.Controllers.SharedResources), Name = "NameKK")]
        public string NameKK { get; set; }

        [Display(ResourceType = typeof(Resources.Controllers.SharedResources), Name = "NameRU")]
        public string NameRU { get; set; }

        [Display(Name = "ExpiredDateTime")]
        public DateTime? ExpiredDateTime { get; set; }

        [Display(Name = "Parent")]
        public int? Parent { get; set; }

        [Display(Name = "AreaType")]
        public int? AreaType { get; set; }

        [Display(Name = "EgovId")]
        public int? EgovId { get; set; }
    }
}
