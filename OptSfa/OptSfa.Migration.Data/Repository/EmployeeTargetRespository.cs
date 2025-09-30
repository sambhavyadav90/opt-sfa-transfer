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
    public class EmployeeTargetRespository : IEmployeeTargetRepository
    {
        private readonly AppDbContext db;
        public EmployeeTargetRespository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<EmployeeTargetViewModel>> getAllEmployeeTarget(string empId, string itemType, string itemStatus)
        {
            string sqlQuery = $@"
        SELECT 
            item_name, 
            item_newid, 
            item_pack_size, 
            item_code, 
            pts, 
            ptr, 
            mrp, 
            sample_rate AS nrv, 
            purchase_rate
        FROM 
            item_details
        WHERE 
            state_main IN (
                SELECT state_main 
                FROM emp_detail 
                WHERE emp_id = '{empId}'
            )
            AND TYPE = '{itemType}'
            AND item_status = '{itemStatus}'
        ORDER BY 
            item_name";

            var items = await db.Database
                .SqlQueryRaw<EmployeeTargetViewModel>(sqlQuery) // no string.Format
                .ToListAsync();

            return items;
        }

    }
}