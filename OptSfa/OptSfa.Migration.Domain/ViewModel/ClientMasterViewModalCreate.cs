using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptSfa.Migration.Domain.ViewModel
{
public class ClientMasterViewModalList
{
    public List<ClientMasterViewModalCreate> bulkClientList { get; set; }
}

public class ClientMasterViewModalCreate
{
    public string pin { get; set; }
    public string stateMain { get; set; }
    public int? clientId { get; set; }
    public string clientCode { get; set; }
    public string clientName { get; set; }
    public string category { get; set; }
    public string regularVisit { get; set; }
    public string address { get; set; }
    public string dob { get; set; }
    public string anniversaryDate { get; set; }
    public string clientIbusiness { get; set; }
    public string visitTime { get; set; }
    public string mobile { get; set; }
    public string empIdOld { get; set; }
    public string area { get; set; }
    public string district { get; set; }
    public string clientStatus { get; set; }
    public string qualification { get; set; }
    public string remarks { get; set; }
    public string degree { get; set; }
    public string speciality { get; set; }
    public string otherInfo1 { get; set; }
    public string empId { get; set; }
    public string empName { get; set; }
    public string destrictId { get; set; }
    public string areaMain { get; set; }
    public string otherInfo2 { get; set; }
    public string otherInfo3 { get; set; }
    public string approveSm { get; set; }
    public string deleteRequest { get; set; }
    public string clientFundStatus { get; set; }
    public string visitDays { get; set; }
    public string mobile1 { get; set; }
    public string mobile2 { get; set; }
    public string clientType { get; set; }
    public string contactPerson1 { get; set; }
    public string contactPerson2 { get; set; }
    public string createDate { get; set; }
    public string dlNo { get; set; }
    public string landMark { get; set; }
    public string lat { get; set; }
    public string lon { get; set; }
    public int? noOfPatient { get; set; }
    public string whereSit { get; set; }
    public string experiences { get; set; }
    public string urbanSemiurban { get; set; }
    public string contactPersonMobile1 { get; set; }
    public string contactPersonMobile2 { get; set; }
    public string gstNo { get; set; }
    public string panNo { get; set; }
    public string aadharNo { get; set; }
    public string lastModifiedDateTime { get; set; }
    public string starRating { get; set; }
    public string propertyName { get; set; }
    public string emailId { get; set; }
    public string designation { get; set; }
    public int? isMultilocation { get; set; }
    public int? rxStatus { get; set; }
    public string imagePath { get; set; }
    public int? isPool { get; set; }
    public int? isGeo { get; set; }
    public string updatedBy { get; set; }
}

public class ClientIDToDelete
{
    public List<int> clientIDs { get; set; }
}

public class ClientSpecCatDeg
{
    public List<string> specialityList { get; set; }
    public List<string> categoryList { get; set; }
    public List<string> degreeList { get; set; }
}
}