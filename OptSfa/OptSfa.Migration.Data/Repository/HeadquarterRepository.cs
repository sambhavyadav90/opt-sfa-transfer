using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OptSfa.Migration.Data.Context;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.Models;
using OptSfa.Migration.Domain.ViewModel;
using MySqlConnector;

namespace OptSfa.Migration.Data.Repository
{
    public class HeadquarterRepository : IHeadquarterRepository
    {
        private readonly AppDbContext db;
        public HeadquarterRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<HeadQuaterMasterViewModel>> getAllHeadquaters(int id)
        {
            string sqlQuery = @"
                SELECT dm.district_id, dm.district, dm.district_code, sm.state_main, sm.state 
                FROM district_parent_main dm 
                JOIN state_master sm ON (dm.state_main = sm.state_main) 
                WHERE dm.status = 'Active' AND sm.status = 0";

            var parameters = new List<MySqlParameter>();

            if (id > 0)
            {
                sqlQuery += " AND dm.state_main = @stateMain";
                parameters.Add(new MySqlParameter("@stateMain", id));
            }

            sqlQuery += " ORDER BY sm.state, dm.district";

            var items = await db.Database
                .SqlQueryRaw<HeadQuaterMasterViewModel>(sqlQuery, parameters.ToArray())
                .ToListAsync();

            return items ?? new List<HeadQuaterMasterViewModel>();
        }
    }
}