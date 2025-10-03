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
        public async Task<List<HeadQuarterViewModel>> getAllHeadQuarters()
        {
            string sqlQuery = $@"SELECT state, district_id AS districtId, district 
            FROM state_master  sm JOIN district_parent_main  
            dp ON(sm.state_main = dp.state_main) 
            WHERE sm.status = 0 AND dp.status = 'Active' ORDER BY state,district";

            var items = await db.Database
                .SqlQueryRaw<HeadQuarterViewModel>(sqlQuery)
                .ToListAsync();

            return items;
        }
    }
}