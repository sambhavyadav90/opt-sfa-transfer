using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptSfa.Migration.Domain.Models
{
    public class EmployeeMaster
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

    [NotMapped]
    public string pin { get; set; }
    [NotMapped]
    public string appVersion { get; set; }
    [NotMapped]
    public string appVersionCode { get; set; }
    [NotMapped]
    public string androidVersion { get; set; }
    [NotMapped]
    public string imei { get; set; }
    [NotMapped]
    public string deviceName { get; set; }
    [NotMapped]
    public string width { get; set; }
    [NotMapped]
    public string height { get; set; }
    [NotMapped]
    public string tokenId { get; set; }
    [NotMapped]
    public DateTime createDate { get; set; }
    [NotMapped]
    public bool? isValid { get; set; }
    public bool isLogout { get; set; }
    [NotMapped]
    public string fcmToken { get; set; }
    [NotMapped]
    public string apiUrl { get; set; }
    }
}