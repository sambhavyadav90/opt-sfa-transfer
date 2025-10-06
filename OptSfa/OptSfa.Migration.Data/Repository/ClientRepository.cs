using OptSfa.Migration.Data.Context;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.Models;
using OptSfa.Migration.Domain.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace OptSfa.Migration.Data.Repository;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _db;
    public ClientRepository(AppDbContext db) => _db = db;

    public Task<List<ClientMasterViewModel>> getAll(string empId, string clientType, string areaMain, string status, int page, int pageSize)
    {
        var conditions = new List<string>();
        var parameters = new List<object>();
        var paramIndex = 0;

        if (!string.IsNullOrEmpty(status))
        {
            conditions.Add($"cm.status = {{{paramIndex}}}");
            parameters.Add(status);
            paramIndex++;
        }

        if (!string.IsNullOrEmpty(empId))
        {
            conditions.Add($"cm.emp_id = {{{paramIndex}}}");
            parameters.Add(empId);
            paramIndex++;
        }

        if (!string.IsNullOrEmpty(clientType))
        {
            conditions.Add($"cm.client_type = {{{paramIndex}}}");
            parameters.Add(clientType);
            paramIndex++;
        }

        if (!string.IsNullOrEmpty(areaMain))
        {
            conditions.Add($"cm.area_main = {{{paramIndex}}}");
            parameters.Add(areaMain);
            paramIndex++;
        }

        var whereClause = conditions.Count > 0 ? " WHERE " + string.Join(" AND ", conditions) : "";

        int offset = (page - 1) * pageSize;
        var limitClause = $" LIMIT {{{paramIndex}}}, {{{paramIndex + 1}}}";
        parameters.Add(offset);
        parameters.Add(pageSize);

        var sql = $@"
            SELECT 
                cm.client_id AS clientId,
                cm.client_code AS clientCode,
                cm.client_name AS clientName,
                cm.category AS category,
                (cm.regular_visit = 1) AS regularVisit,       
                cm.address AS address,
                IF(cm.dob = '1900-01-01','',DATE_FORMAT(cm.dob, '%d-%m-%Y')) AS dob,
                IF(cm.anniversary_date = '1900-01-01','',DATE_FORMAT(cm.anniversary_date, '%d-%m-%Y')) AS anniversaryDate,
                cm.client_ibusiness AS clientIbusiness,
                cm.mobile AS mobile,
                cm.area AS area,
                cm.status AS status,
                cm.qualification AS qualification,
                cm.degree AS degree,
                cm.speciality AS speciality,
                cm.other_info_1 AS otherInfo1,
                cm.emp_id AS empId,
                cm.area_main AS areaMain,
                cm.other_info_2 AS otherInfo2,
                cm.client_fund_status AS clientFundStatus,
                cm.client_type AS clientType,
                CAST(cm.lat AS DOUBLE) AS lat,
                CAST(cm.lon AS DOUBLE) AS lon,
                cm.no_of_patient AS noOfPatient,
                cm.email_id AS emailId,
                (cm.is_geo = 1) AS isGeo,
                cm.sub_category AS subCategory,
                cm.sub_speciality AS subSpeciality,
                cm.no_of_machine AS noOfMachine,
                cm.mapped_campaign AS mappedCampaign,
                cm.mdl_no AS mdlNo,
                IF(cm.spouse_birthday = '1900-01-01','',DATE_FORMAT(cm.spouse_birthday, '%d-%m-%Y')) AS spouseBirthday,
                cm.gender AS gender
            FROM client_master AS cm{whereClause}
            ORDER BY cm.client_name, cm.area{limitClause}";

        return _db.Database.SqlQueryRaw<ClientMasterViewModel>(sql, parameters.ToArray()).ToListAsync();
    }
}
    