using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptSfa.Migration.Domain.ViewModel
{
    public class EmployeeMasterViewModel
    {

        public string empId { get; set; }
        public string name { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }
        public string designationsOid { get; set; }
        public string district { get; set; }
        public int? distrectId { get; set; }
        public string State { get; set; }
        public int? stateMain { get; set; }
        public DateTime? dateJoining { get; set; }
        public string mobile { get; set; }
        public string imageePath { get; set; }
        public int? empLevel { get; set; }
        public string email { get; set; }
        public DateTime? dob { get; set; }
        public string status { get; set; }
        public string gender { get; set; }
        public int stopReporting { get; set; }
        public bool? isValid { get; set; }
        public bool isLogout { get; set; }
    }
}