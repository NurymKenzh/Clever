using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleverAPI.Models
{
    public class CATO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string NameKK { get; set; }
        public string NameRU { get; set; }
        public DateTime? ExpiredDateTime { get; set; }
        public int? Parent { get; set; }
        public int? AreaType { get; set; }
        public int? EgovId { get; set; }
    }
}
