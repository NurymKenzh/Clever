using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clever.Models
{
    public class CompanyKK
    {
        public int Id { get; set; }

        [Display(Name = "BIN")]
        public string BIN { get; set; }

        [Display(Name = "NameKK")]
        public string NameKK { get; set; }

        [Display(Name = "NameRU")]
        public string NameRU { get; set; }

        [Display(Name = "DateRegister")]
        [DataType(DataType.Date)]
        //[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateRegister { get; set; }

        [Display(Name = "OKED")]
        public string OKED { get; set; }

        [Display(Name = "ActivityKindKK")]
        public string ActivityKindKK { get; set; }

        [Display(Name = "ActivityKindRU")]
        public string ActivityKindRU { get; set; }

        [Display(Name = "OKEDSecondary")]
        public string OKEDSecondary { get; set; }

        [Display(Name = "KRP")]
        public string KRP { get; set; }

        [Display(Name = "KRPNameKK")]
        public string KRPNameKK { get; set; }

        [Display(Name = "KRPNameRU")]
        public string KRPNameRU { get; set; }

        [Display(Name = "CATO")]
        public string CATO { get; set; }

        [Display(Name = "LocalityKK")]
        public string LocalityKK { get; set; }

        [Display(Name = "LocalityRU")]
        public string LocalityRU { get; set; }

        [Display(Name = "LegalAddress")]
        public string LegalAddress { get; set; }

        [Display(Name = "HeadName")]
        public string HeadName { get; set; }

        public DateTime? ExpiredDateTime { get; set; }
    }
}
