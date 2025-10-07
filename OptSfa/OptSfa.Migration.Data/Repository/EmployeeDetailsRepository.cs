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
                SELECT ed.emp_id,ed.name,ed.user_name,ed.user_password,ed.designations_oid,ed.district,ed.distrect_id,ed.state,ed.state_main,ed.previous_ass AS doj,ed.mobile,ed.imagee_path,ed.emp_level,ed.email,ed.dob,ed.status,ed.gender,ed.stop_reporting,pm.pin,pm.app_version,pm.app_version_code,pm.android_version,pm.imei,pm.device_name,pm.width,pm.height,pm.token_id,pm.create_date,pm.isvalid,ed.isLogout,ft.fcm_token 
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