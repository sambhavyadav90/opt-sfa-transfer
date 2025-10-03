using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptSfa.Migration.Domain.Models
{
    public class TargetPercentFormula
    {
        [Key]
        public int row_id { get; set; } = 0;
        
        [Required(ErrorMessage = "Year is Required")]
        public int year { get; set; }
        [Required(ErrorMessage = "Month is Required")]
        public int month { get; set; }

        [Required(ErrorMessage = "Percentage is Required")]
        public float percentage { get; set; }
        [Required(ErrorMessage = "CreateByUser is Required")]

        public string create_by { get; set; }
        public DateTime create_date { get; set; } = DateTime.Now;
    }
}