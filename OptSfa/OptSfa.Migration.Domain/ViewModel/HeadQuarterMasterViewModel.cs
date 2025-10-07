using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptSfa.Migration.Domain.ViewModel
{
    public class HeadQuaterMasterViewModel
    {
        public int? district_id { get; set; }
        public string? district { get; set; }
        public string? district_code { get; set; }
        public int? state_main { get; set; }
        public string? state { get; set; }
    }
}