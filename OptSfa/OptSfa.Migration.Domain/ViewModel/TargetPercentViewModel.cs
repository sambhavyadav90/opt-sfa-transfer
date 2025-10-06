using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptSfa.Migration.Domain.ViewModel
{
    public class TargetPercentViewModel
    {
        public int RowCode { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public float Rate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}