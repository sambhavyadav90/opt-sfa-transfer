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
    public class HeadQuarterViewRepository : IHeadQuarterViewRepository
    {
        private readonly AppDbContext db;
        public HeadQuarterViewRepository(AppDbContext db)
        {
            this.db = db;
        }
        async Task<List<HeadQuaterMasterViewModel>> IHeadQuarterViewRepository.getAllHeadQuarters(int id)
        {
            List<HeadQuaterMasterViewModel> items;

            if (id > 0)
            {
                string sqlQuery = @"
                    SELECT dm.district_id, dm.district, dm.district_code, sm.state_main, sm.state 
                    FROM district_parent_main dm 
                    JOIN state_master sm ON (dm.state_main = sm.state_main) 
                    WHERE dm.status = 'Active' AND sm.status = 0 AND dm.state_main = {0}
                    ORDER BY sm.state, dm.district";

                items = await db.Database
                    .SqlQueryRaw<HeadQuaterMasterViewModel>(sqlQuery, id.ToString())
                    .ToListAsync();
            }
            else
            {
                string sqlQuery = @"
                    SELECT dm.district_id, dm.district, dm.district_code, sm.state_main, sm.state 
                    FROM district_parent_main dm 
                    JOIN state_master sm ON (dm.state_main = sm.state_main) 
                    WHERE dm.status = 'Active' AND sm.status = 0";

                items = await db.Database
                    .SqlQueryRaw<HeadQuaterMasterViewModel>(sqlQuery)
                    .ToListAsync();
            }

            return items;
        }
    }
}