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
    public class SaveDataMasterRepository : ISaveDataMasterRepository
    {
        private readonly AppDbContext db;
        public SaveDataMasterRepository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<SaveDataMaster> saveData(SaveDataMaster data)
        {
            try
            {
                var columndata = await db.mappedDbColumnTrans.ToListAsync();
                int cnt = 0;
                if (data.ItemName != null)
                {
                    string dbcolumn = "ItemName";
                    var datas = columndata.Where(x => x.DbColumnName == dbcolumn).ToList();
                    if (datas.Count > 0)
                    {
                        cnt = datas.Where(x => x.DbColumnMapping == data.ItemName).Count();
                    }
                    if (cnt == 0)
                    {
                        string insertQuery = "INSERT INTO tran_savedatacolumn_mapping (db_column_name, db_column_mapping) VALUES (@p0, @p1)";
                        await db.Database.ExecuteSqlRawAsync(insertQuery, dbcolumn, data.ItemName);
                    }
                }

                if (data.Mrp != null)
                {
                    string dbcolumn = "Mrp";
                    var datas = columndata.Where(x => x.DbColumnName == dbcolumn).ToList();
                    if (datas.Count > 0)
                        cnt = datas.Where(x => x.DbColumnMapping == data.Mrp).Count();
                    if (cnt == 0)
                    {
                        if (cnt == 0)
                        {
                            string insertQuery = "INSERT INTO tran_savedatacolumn_mapping (db_column_name, db_column_mapping) VALUES (@p0, @p1)";
                            await db.Database.ExecuteSqlRawAsync(insertQuery, dbcolumn, data.Mrp);
                        }
                    }
                }

                if (data.Qty != null)
                {
                    string dbcolumn = "Qty";
                    var datas = columndata.Where(x => x.DbColumnName == dbcolumn).ToList();
                    cnt = datas.Where(x => x.DbColumnMapping == data.Qty).Count();
                    if (cnt == 0)
                    {
                        if (cnt == 0)
                        {
                            string insertQuery = "INSERT INTO tran_savedatacolumn_mapping (db_column_name, db_column_mapping) VALUES (@p0, @p1)";
                            await db.Database.ExecuteSqlRawAsync(insertQuery, dbcolumn, data.Qty);
                        }
                    }
                }

                if (data.Total != null)
                {
                    string dbcolumn = "Total";
                    var datas = columndata.Where(x => x.DbColumnName == dbcolumn).ToList();
                    cnt = datas.Where(x => x.DbColumnMapping == data.Total).Count();
                    if (cnt == 0)
                    {
                        if (cnt == 0)
                        {
                            string insertQuery = "INSERT INTO tran_savedatacolumn_mapping (db_column_name, db_column_mapping) VALUES (@p0, @p1)";
                            await db.Database.ExecuteSqlRawAsync(insertQuery, dbcolumn, data.Total);
                        }
                    }
                }

                if (data.Tax != null)
                {
                    string dbcolumn = "Tax";
                    var datas = columndata.Where(x => x.DbColumnName == dbcolumn).ToList();
                    cnt = datas.Where(x => x.DbColumnMapping == data.Tax).Count();
                    if (cnt == 0)
                    {
                        if (cnt == 0)
                        {
                            string insertQuery = "INSERT INTO tran_savedatacolumn_mapping (db_column_name, db_column_mapping) VALUES (@p0, @p1)";
                            await db.Database.ExecuteSqlRawAsync(insertQuery, dbcolumn, data.Tax);
                        }
                    }
                }

                if (data.FinalAmount != null)
                {
                    string dbcolumn = "FinalAmount";
                    var datas = columndata.Where(x => x.DbColumnName == dbcolumn).ToList();
                    cnt = datas.Where(x => x.DbColumnMapping == data.FinalAmount).Count();
                    if (cnt == 0)
                    {
                        if (cnt == 0)
                        {
                            string insertQuery = "INSERT INTO tran_savedatacolumn_mapping (db_column_name, db_column_mapping) VALUES (@p0, @p1)";
                            await db.Database.ExecuteSqlRawAsync(insertQuery, dbcolumn, data.FinalAmount);
                        }
                    }
                }

                await db.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}