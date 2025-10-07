using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptSfa.Migration.Domain.ViewModel
{
    public class EmployeeDetailsViewModel
    {
        public string? emp_id { get; set; }
        public string? name { get; set; }
        public string? user_name { get; set; }
        public string? user_password { get; set; }
        public string? designations_oid { get; set; }
        public string? district { get; set; }
        public string? distrect_id { get; set; }
        public string? state { get; set; }
        public string? state_main { get; set; }
        public string? doj { get; set; }
        public string? mobile { get; set; }
        public string? imagee_path { get; set; }
        public string? emp_level { get; set; }
        public string? email { get; set; }
        public DateTime? dob { get; set; }
        public string? status { get; set; }
        public string? gender { get; set; }
        public int? stop_reporting { get; set; }
        public int? pin { get; set; }
        public int? app_version { get; set; }
        public int? app_version_code { get; set; }
        public string? android_version { get; set; }
        public string? imei { get; set; }
        public string? device_name { get; set; }
        public string? width { get; set; }
        public string? height { get; set; }
        public string? Token_ID { get; set; }
        public DateTime? Create_Date { get; set; }
        public bool? isValid { get; set; }
        public bool? isLogout { get; set; }
        public string? fcm_token { get; set; }
    }
}