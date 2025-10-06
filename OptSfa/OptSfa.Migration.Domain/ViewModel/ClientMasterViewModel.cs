using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptSfa.Migration.Domain.ViewModel
{
    public class ClientMasterViewModel
    {
        public int clientId { get; set; }
        public string clientCode { get; set; }
        public string clientName { get; set; }
        public string category { get; set; }
        public bool regularVisit { get; set; }
        public string address { get; set; }
        public string dob { get; set; }
        public string? anniversaryDate { get; set; }
        public string clientIbusiness { get; set; }
        public string mobile { get; set; }
        public string area { get; set; }
        public string status { get; set; }
        public string qualification { get; set; }
        public string degree { get; set; }
        public string speciality { get; set; }
        public string? otherInfo1 { get; set; }
        public string empId { get; set; }
        public string areaMain { get; set; }
        public string? otherInfo2 { get; set; }
        public string clientFundStatus { get; set; }
        public string clientType { get; set; }
        public double? lat { get; set; }
        public double? lon { get; set; }
        public int noOfPatient { get; set; }
        public string emailId { get; set; }
        public bool isGeo { get; set; }
        public string subCategory { get; set; }
        public string subSpeciality { get; set; }
        public int noOfMachine { get; set; }
        public string mappedCampaign { get; set; }
        public string mdlNo { get; set; }
        public string spouseBirthday { get; set; }
        public string gender { get; set; }
    }
}