using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OptSfa.Migration.Data.Context;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.Models;

namespace OptSfa.Migration.Data.Repository
{
    public class MappedDbColumnRepository : IMappedDbcolumnRepository
    {
        private readonly AppDbContext db;
        public MappedDbColumnRepository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<List<MappedDbColumnTran>> getAll()
        {
            var data = await db.mappedDbColumnTrans.ToListAsync();
            return data;
        }
        public async Task<List<MappedDbColumnTran>> getByDbColumn(string? dbcolumnm, string? dbcolumnmapping)
        {
            IQueryable<MappedDbColumnTran> query = db.mappedDbColumnTrans;

            if (!string.IsNullOrEmpty(dbcolumnm) && !string.IsNullOrEmpty(dbcolumnmapping))
            {
                query = query.Where(x => x.DbColumnName == dbcolumnm && x.DbColumnMapping == dbcolumnmapping);
            }
            else if (!string.IsNullOrEmpty(dbcolumnm))
            {

                query = query.Where(x => x.DbColumnName == dbcolumnm);
            }
            else if (!string.IsNullOrEmpty(dbcolumnmapping))
            {

                query = query.Where(x => x.DbColumnMapping == dbcolumnmapping);
            }

            return await query.ToListAsync();
        }

    }
}