using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleverAPI.Models
{
    public class CompanyKK
    {
        public int Id { get; set; }
        public string BIN { get; set; }
        public string NameKK { get; set; }
        public string NameRU { get; set; }
        public DateTime? DateRegister { get; set; }
        public string OKED { get; set; }
        public string ActivityKindKK { get; set; }
        public string ActivityKindRU { get; set; }
        public string OKEDSecondary { get; set; }
        public string KRP { get; set; }
        public string KRPNameKK { get; set; }
        public string KRPNameRU { get; set; }
        public string CATO { get; set; }
        public string LocalityKK { get; set; }
        public string LocalityRU { get; set; }
        public string LegalAddress { get; set; }
        public string HeadName { get; set; }
        public DateTime? ExpiredDateTime { get; set; }
    }
}
