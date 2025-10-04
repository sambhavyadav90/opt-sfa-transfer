using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Data.Context;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace OptSfa.Migration.Data.Repository
{
    public class StateMasterRepository : IStateMasterRepository
    {
        private readonly AppDbContext db;
        public StateMasterRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<StateMasterViewModel>> getAll(string? status = null, string? stateMain = null)
        {
            var conditions = new List<string>();
            var parameters = new List<object>();
            var paramIndex = 0;

            if (!string.IsNullOrEmpty(status))
            {
                conditions.Add($"sm.status = {{{paramIndex}}}");
                parameters.Add(status);
                paramIndex++;
            }

            if (!string.IsNullOrEmpty(stateMain))
            {
                conditions.Add($"sm.state_main = {{{paramIndex}}}");
                parameters.Add(stateMain);
                paramIndex++;
            }

            var whereClause = conditions.Count > 0 ? " WHERE " + string.Join(" AND ", conditions) : "";

            var sql = $@"
                SELECT 
                    sm.state_main AS stateMain, 
                    sm.state AS state, 
                    sm.state_code AS stateCode, 
                    CASE sm.status WHEN 0 THEN 'Active' ELSE 'Inactive' END AS stateStatus,
                    sm.division_id AS divisionId, 
                    sm.zone_id AS zoneId, 
                    COALESCE(zm.zone_name, '') AS zoneName, 
                    COALESCE(d.dept_name, '') AS deptName
                FROM state_master sm
                LEFT JOIN zone_master zm ON sm.zone_id = zm.zone_id
                LEFT JOIN department d ON sm.division_id = d.dept_id{whereClause}
                ORDER BY sm.state";

            return await db.Database.SqlQueryRaw<StateMasterViewModel>(sql, parameters.ToArray()).ToListAsync();
        }
    }
}