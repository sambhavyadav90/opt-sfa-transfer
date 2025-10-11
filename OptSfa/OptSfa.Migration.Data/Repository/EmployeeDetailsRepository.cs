using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OptSfa.Migration.Data.Context;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Data.Repository
{
    public class EmployeeDetailsRepository : IEmployeeDetailsRepository
    {
        private readonly AppDbContext appDbContext;
        public EmployeeDetailsRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public  Task<List<EmployeeDetailsViewModel>> GetAllEmployees(string distrectid, string state_main, string designations_oid, string emp_id)
        {
            var parameters = new List<object>();
            var whereConditions = new List<string> { "ed.status = 'Active'" };

            if (!string.IsNullOrEmpty(distrectid))
            {
                whereConditions.Add("ed.distrect_id = {0}");
                parameters.Add(distrectid);
            }

            if (!string.IsNullOrEmpty(state_main))
            {
                whereConditions.Add("ed.state_main = {0}");
                parameters.Add(state_main);
            }

            if (!string.IsNullOrEmpty(designations_oid))
            {
                whereConditions.Add("ed.designations_oid = {0}");
                parameters.Add(designations_oid);
            }

            if (!string.IsNullOrEmpty(emp_id))
            {
                whereConditions.Add("ed.emp_id = {0}");
                parameters.Add(emp_id);
            }

            string whereClause = string.Join(" AND ", whereConditions);

            string sqlQuery = $@"
                SELECT CAST(ed.emp_id AS CHAR) AS emp_id, CAST(ed.name AS CHAR) AS name, CAST(ed.user_name AS CHAR) AS user_name, CAST(ed.user_password AS CHAR) AS user_password, CAST(ed.designations_oid AS CHAR) AS designations_oid, CAST(ed.district AS CHAR) AS district, CAST(ed.distrect_id AS CHAR) AS distrect_id, CAST(ed.state AS CHAR) AS state, CAST(ed.state_main AS CHAR) AS state_main, CAST(ed.previous_ass AS CHAR) AS doj, CAST(ed.mobile AS CHAR) AS mobile, CAST(ed.imagee_path AS CHAR) AS imagee_path, CAST(ed.emp_level AS CHAR) AS emp_level, CAST(ed.email AS CHAR) AS email, CAST(ed.dob AS CHAR) AS dob, CAST(ed.status AS CHAR) AS status, CAST(ed.gender AS CHAR) AS gender, CAST(ed.stop_reporting AS CHAR) AS stop_reporting, CAST(pm.pin AS CHAR) AS pin, CAST(pm.app_version AS CHAR) AS app_version, CAST(pm.app_version_code AS CHAR) AS app_version_code, CAST(pm.android_version AS CHAR) AS android_version, CAST(pm.imei AS CHAR) AS imei, CAST(pm.device_name AS CHAR) AS device_name, CAST(pm.width AS CHAR) AS width, CAST(pm.height AS CHAR) AS height, CAST(pm.token_id AS CHAR) AS Token_ID, CAST(pm.create_date AS CHAR) AS Create_Date, CAST(pm.isvalid AS CHAR) AS isValid, CAST(ed.isLogout AS CHAR) AS isLogout, CAST(ft.fcm_token AS CHAR) AS fcm_token 
                FROM emp_detail ed 
                LEFT JOIN (SELECT p.* FROM (SELECT *,ROW_NUMBER() OVER (PARTITION BY emp_id ORDER BY create_date DESC) AS rn FROM pin_master_new) p WHERE p.rn=1) pm ON ed.emp_id=pm.emp_id 
                LEFT JOIN (SELECT f.* FROM (SELECT *,ROW_NUMBER() OVER (PARTITION BY emp_id ORDER BY id DESC) AS rn FROM fcm_token_employee) f WHERE f.rn=1) ft ON ed.emp_id=ft.emp_id 
                WHERE {whereClause}
                ORDER BY ed.name";

            var items = appDbContext.Database
                .SqlQueryRaw<EmployeeDetailsViewModel>(sqlQuery, parameters.ToArray())
                .ToListAsync();

            return items;
        }


    }
}