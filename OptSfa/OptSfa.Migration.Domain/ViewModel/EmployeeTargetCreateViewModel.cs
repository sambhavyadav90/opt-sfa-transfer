using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OptSfa.Migration.Domain.ViewModel
{
    public class EmployeeTargetCreateViewModel
    {
        [Required(ErrorMessage = "EmployeeId is required")]
        public string empId { get; set; }
        [Required(ErrorMessage = "Year is required")]
        public int year { get; set; }
        [Required(ErrorMessage = "Month is required")]
        public int month { get; set; }
        [Required(ErrorMessage = "Item name is required")]

        public string itemName { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int itemQuantity { get; set; }
        public DateTime createDate { get; set; }

        public float pts { get; set; } = 0.0f;
        public float ptr { get; set; } = 0.0f;
        public float mrp { get; set; } = 0.0f;
        public float nrv { get; set; } = 0.0f;
        public float value { get; set; }
        public float purchaseRate { get; set; } = 0.0f;
    }
}