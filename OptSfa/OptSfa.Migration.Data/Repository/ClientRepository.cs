using OptSfa.Migration.Data.Context;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.Models;
using OptSfa.Migration.Domain.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptSfa.Migration.Data.Repository;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _db;
    public ClientRepository(AppDbContext db) => _db = db;
    public async Task<ClientMasterViewModalList> createClient(string id, ClientMasterViewModalList data)
    {try
        {

            // Fetch employee details using raw SQL
            var empSql = @"
        SELECT 
            emp_id AS empId,
            name,
            user_name AS userName,
            user_password AS userPassword,
            designations_oid AS designationsOid,
            district,
            CAST(distrect_id AS SIGNED) AS distrectId,
            State,
            CAST(state_main AS SIGNED) AS stateMain,
            date_joining AS dateJoining,
            mobile,
            imagee_path AS imageePath,
            CAST(emp_level AS SIGNED) AS empLevel,
            email,
            dob,
            status,
            gender,
            CAST(stop_reporting AS SIGNED) AS stopReporting,
            isLogout
        FROM emp_detail 
        WHERE emp_id = {0}";

            var empDetails = await _db.Database.SqlQueryRaw<EmployeeMaster>(empSql, id).SingleOrDefaultAsync();
            if (empDetails == null)
            {
                throw new Exception("Invalid empId");
            }
            var entities = data.bulkClientList ?? new List<ClientMasterViewModalCreate>();

            var entity = entities.First();

            // Set fields from employee
            entity.stateMain = empDetails.stateMain?.ToString() ?? string.Empty;
            entity.district = empDetails.district ?? string.Empty;
            entity.destrictId = empDetails.distrectId?.ToString() ?? string.Empty;
            entity.empId = id;
            entity.empIdOld = id;
            entity.clientStatus = "True";
            entity.approveSm = empDetails.empLevel?.ToString() ?? string.Empty;
            entity.deleteRequest = string.Empty;
            entity.createDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            entity.lastModifiedDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var dobValue = string.IsNullOrEmpty(entity.dob) ? new DateTime(1900, 1, 1) : DateTime.Parse(entity.dob);
            var anniversaryValue = string.IsNullOrEmpty(entity.anniversaryDate) ? new DateTime(1900, 1, 1) : DateTime.Parse(entity.anniversaryDate);
            var createDateValue = DateTime.Parse(entity.createDate);
            var lastModifiedValue = DateTime.Parse(entity.lastModifiedDateTime);

            var parameters = new object[]
            {
            entity.pin ?? string.Empty, entity.stateMain ?? string.Empty, entity.clientCode ?? string.Empty, entity.clientName ?? string.Empty, entity.category ?? string.Empty,
            entity.regularVisit ?? string.Empty, entity.address ?? string.Empty, dobValue, anniversaryValue, entity.clientIbusiness ?? string.Empty,
            entity.visitTime ?? string.Empty, entity.mobile ?? string.Empty, entity.empIdOld ?? string.Empty, entity.area ?? string.Empty, entity.district ?? string.Empty,
            entity.clientStatus ?? string.Empty, entity.qualification ?? string.Empty, entity.remarks ?? string.Empty, entity.degree ?? string.Empty, entity.speciality ?? string.Empty,
            entity.otherInfo1 ?? string.Empty, entity.empId ?? string.Empty, entity.destrictId ?? string.Empty, entity.areaMain ?? string.Empty, entity.otherInfo2 ?? string.Empty,
            entity.otherInfo3 ?? string.Empty, entity.approveSm ?? string.Empty, entity.deleteRequest ?? string.Empty, entity.clientFundStatus ?? "False", entity.visitDays ?? string.Empty,
            entity.mobile1 ?? string.Empty, entity.mobile2 ?? string.Empty, entity.clientType ?? string.Empty, entity.contactPerson1 ?? string.Empty, entity.contactPerson2 ?? string.Empty,
            createDateValue, entity.dlNo ?? string.Empty, entity.landMark ?? string.Empty, entity.lat ?? string.Empty, entity.lon ?? string.Empty,
            entity.noOfPatient ?? 0, entity.whereSit ?? string.Empty, entity.experiences ?? string.Empty, entity.urbanSemiurban ?? string.Empty,
            entity.contactPersonMobile1 ?? string.Empty, entity.contactPersonMobile2 ?? string.Empty, entity.gstNo ?? string.Empty, entity.panNo ?? string.Empty,
            entity.aadharNo ?? string.Empty, lastModifiedValue, entity.starRating ?? string.Empty, entity.propertyName ?? string.Empty,
            entity.emailId ?? string.Empty, entity.designation ?? string.Empty, entity.isMultilocation ?? 0, entity.rxStatus ?? 0, entity.imagePath ?? string.Empty,
            entity.isPool ?? 0, entity.isGeo ?? 0, entity.updatedBy ?? string.Empty
            };

            var insertSql = @"
            INSERT INTO client_master 
            (pin, state_main, client_code, client_name, Category, regular_visit, address, dob, anniversary_date, 
             client_ibusiness, visit_time, mobile, emp_id_old, area, district, status, Qualification, remarks, 
             Degree, speciality, other_info_1, emp_id, destrict_id, area_main, other_info_2, other_info_3, 
             approve_sm, delete_request, client_fund_status, visit_days, mobile_1, mobile_2, client_type, 
             contact_person_1, contact_person_2, create_date, dl_no, land_mark, lat, lon, no_of_patient, 
             where_sit, experiences, urban_semiurban, contact_person_mobile_1, contact_person_mobile_2, 
             gst_no, pan_no, aadhar_no, last_modified_date_time, star_rating, property_name, email_id, 
             designation, is_multilocation, rx_status, image_path, is_pool, is_geo, updated_by) 
            VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, 
                   {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, 
                   {30}, {31}, {32}, {33}, {34}, {35}, {36}, {37}, {38}, {39}, {40}, {41}, {42}, {43}, 
                   {44}, {45}, {46}, {47}, {48}, {49}, {50}, {51}, {52}, {53}, {54}, {55}, {56}, {57}, 
                   {58}, {59})";
            try
            {

                await _db.Database.ExecuteSqlRawAsync(insertSql, parameters);

            }
            catch (System.Exception)
            {

                throw;
            }
            var lastIdSql = "SELECT LAST_INSERT_ID() AS `Value`";
            int insertedId = await _db.Database.SqlQueryRaw<int>(lastIdSql).SingleAsync();

            if (string.IsNullOrWhiteSpace(entity.clientCode))
            {
                var updateSql = "UPDATE client_master SET client_code = {0} WHERE client_id = {1} AND (client_code = '' OR client_code IS NULL OR client_code = '0')";
                await _db.Database.ExecuteSqlRawAsync(updateSql, insertedId.ToString(), insertedId);
            }

            // Fetch the created entity for return
            var selectSql = @"
            SELECT 
                cm.pin AS pin,
                cm.state_main AS stateMain,
                cm.client_id AS clientId,
                cm.client_code AS clientCode,
                cm.client_name AS clientName,
                cm.Category AS category,
                cm.regular_visit AS regularVisit,
                cm.address AS address,
                IF(cm.dob = '1900-01-01','',DATE_FORMAT(cm.dob, '%d-%m-%Y')) AS dob,
                IF(cm.anniversary_date = '1900-01-01','',DATE_FORMAT(cm.anniversary_date, '%d-%m-%Y')) AS anniversaryDate,
                cm.client_ibusiness AS clientIbusiness,
                cm.visit_time AS visitTime,
                cm.mobile AS mobile,
                cm.emp_id_old AS empIdOld,
                cm.area AS area,
                cm.district AS district,
                cm.status AS clientStatus,
                cm.Qualification AS qualification,
                cm.remarks AS remarks,
                cm.Degree AS degree,
                cm.speciality AS speciality,
                cm.other_info_1 AS otherInfo1,
                cm.emp_id AS empId,
                NULL AS empName,
                cm.destrict_id AS destrictId,
                cm.area_main AS areaMain,
                cm.other_info_2 AS otherInfo2,
                cm.other_info_3 AS otherInfo3,
                cm.approve_sm AS approveSm,
                cm.delete_request AS deleteRequest,
                cm.client_fund_status AS clientFundStatus,
                cm.visit_days AS visitDays,
                cm.mobile_1 AS mobile1,
                cm.mobile_2 AS mobile2,
                cm.client_type AS clientType,
                cm.contact_person_1 AS contactPerson1,
                cm.contact_person_2 AS contactPerson2,
                DATE_FORMAT(cm.create_date, '%Y-%m-%d %H:%i:%s') AS createDate,
                cm.dl_no AS dlNo,
                cm.land_mark AS landMark,
                cm.lat AS lat,
                cm.lon AS lon,
                cm.no_of_patient AS noOfPatient,
                cm.where_sit AS whereSit,
                cm.experiences AS experiences,
                cm.urban_semiurban AS urbanSemiurban,
                cm.contact_person_mobile_1 AS contactPersonMobile1,
                cm.contact_person_mobile_2 AS contactPersonMobile2,
                cm.gst_no AS gstNo,
                cm.pan_no AS panNo,
                cm.aadhar_no AS aadharNo,
                DATE_FORMAT(cm.last_modified_date_time, '%Y-%m-%d %H:%i:%s') AS lastModifiedDateTime,
                cm.star_rating AS starRating,
                cm.property_name AS propertyName,
                cm.email_id AS emailId,
                cm.designation AS designation,
                CAST(cm.is_multilocation AS SIGNED) AS isMultilocation,
                CAST(cm.rx_status AS SIGNED) AS rxStatus,
                cm.image_path AS imagePath,
                CAST(cm.is_pool AS SIGNED) AS isPool,
                CAST(cm.is_geo AS SIGNED) AS isGeo,
                cm.updated_by AS updatedBy
            FROM client_master AS cm 
            WHERE cm.client_id = {0}";

            var created = await _db.Database.SqlQueryRaw<ClientMasterViewModalCreate>(selectSql, insertedId).SingleOrDefaultAsync();
            return data;
    }
        catch (System.Exception ex)
        {


            throw;
        }
    }
    public async Task<List<ClientMasterViewModel>> getAll(string empId, string clientType, string areaMain, string status, int page, int pageSize)
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

        return await _db.Database.SqlQueryRaw<ClientMasterViewModel>(sql, parameters.ToArray()).ToListAsync();
    }
}