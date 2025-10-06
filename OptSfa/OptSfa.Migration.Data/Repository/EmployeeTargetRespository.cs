using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using OptSfa.Migration.Data.Context;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Data.Repository
{
    public class EmployeeTargetRespository : IEmployeeTargetRepository
    {
        private readonly AppDbContext db;
        private string lastLoadedEmpId;
        private Dictionary<string, string> productDetail;

        public EmployeeTargetRespository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task setProductDetail(string empId)
        {
            if (!string.IsNullOrEmpty(lastLoadedEmpId) && empId == lastLoadedEmpId)
            {
                return;
            }
            Console.WriteLine("making set product detail call for employee id", empId);
            lastLoadedEmpId = empId;

            productDetail = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            string sqlQuery = @"
                SELECT item_name, item_newid
                FROM item_details
                WHERE state_main IN (
                    SELECT state_main 
                    FROM emp_detail 
                    WHERE emp_id = @empId
                )
                AND TYPE = 'Product'
                AND item_status = 'Active'
                ORDER BY item_name;";

            var items = await db.Database
                .SqlQueryRaw<ItemDetailViewModel>(sqlQuery, new MySqlParameter("@empId", empId ?? string.Empty))
                .ToListAsync();

            foreach (var item in items ?? Enumerable.Empty<ItemDetailViewModel>())
            {
                if (!string.IsNullOrEmpty(item?.item_newid) && !string.IsNullOrEmpty(item?.item_name))
                {
                    productDetail[item.item_newid] = item.item_name;
                }
            }
        }

        public string? GetItemNewIdByName(string itemName)
        {
            if (productDetail == null || productDetail.Count == 0 || string.IsNullOrEmpty(itemName))
            {
                return null;
            }

            var match = productDetail
                .FirstOrDefault(x => x.Value.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            return string.IsNullOrEmpty(match.Key) ? null : match.Key;
        }

        public async Task<bool> createEmployee(EmployeeTargetCreateListRequest inputdata)
        {
            var dataList = inputdata?.data;
            if (dataList == null || !dataList.Any())
            {
                return false;
            }

            var empId = dataList.FirstOrDefault()?.empId;
            if (string.IsNullOrEmpty(empId))
            {
                return false;
            }

            await setProductDetail(empId);

            var parameters = new List<MySqlParameter>();
            var valuesClauses = new List<string>();

            for (int i = 0; i < dataList.Count; i++)
            {
                var data = dataList[i];
                var itemname = data?.itemName ?? string.Empty;
                var returneditemid = GetItemNewIdByName(itemname);
                var itemid = returneditemid ?? string.Empty;
                var other5Value = data?.value ?? 0f;
                var itemcode = string.Empty;
                var prefix = $"p{i}_";

                parameters.Add(new MySqlParameter($"@{prefix}empId", empId));
                parameters.Add(new MySqlParameter($"@{prefix}year", data?.year ?? 0));
                parameters.Add(new MySqlParameter($"@{prefix}month", data?.month ?? 0));
                parameters.Add(new MySqlParameter($"@{prefix}itemId", itemid));
                parameters.Add(new MySqlParameter($"@{prefix}itemCode", itemcode));
                parameters.Add(new MySqlParameter($"@{prefix}itemQuantity", data?.itemQuantity ?? 0f));
                parameters.Add(new MySqlParameter($"@{prefix}other1", string.Empty));
                parameters.Add(new MySqlParameter($"@{prefix}other2", string.Empty));
                parameters.Add(new MySqlParameter($"@{prefix}other3", string.Empty));
                parameters.Add(new MySqlParameter($"@{prefix}other4", data?.pts ?? 0f));
                parameters.Add(new MySqlParameter($"@{prefix}other5", other5Value));
                parameters.Add(new MySqlParameter($"@{prefix}ptr", data?.ptr ?? 0f));
                parameters.Add(new MySqlParameter($"@{prefix}mrp", data?.mrp ?? 0f));
                parameters.Add(new MySqlParameter($"@{prefix}nrv", data?.nrv ?? 0f));
                parameters.Add(new MySqlParameter($"@{prefix}purchaseRate", data?.purchaseRate ?? 0f));

                valuesClauses.Add($@"(@{prefix}empId, @{prefix}year, @{prefix}month, @{prefix}itemId, @{prefix}itemCode, @{prefix}itemQuantity, NOW(), @{prefix}other1, @{prefix}other2, @{prefix}other3, @{prefix}other4, @{prefix}other5, @{prefix}ptr, @{prefix}mrp, @{prefix}nrv, @{prefix}purchaseRate)");
            }

            string sqlQuery = $@"
                INSERT IGNORE INTO employee_wise_target 
                (emp_id, year, month, item_id, item_code, item_quantity, create_date, other1, other2, other3, other4, other5, ptr, mrp, nrv, purchase_rate)
                VALUES 
                {string.Join(", ", valuesClauses)}";

            try
            {
                int rowsAffected = await db.Database
                    .ExecuteSqlRawAsync(sqlQuery, parameters.ToArray());

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing batch query: {ex.Message}", ex);
            }
        }

        public async Task<List<EmployeeTargetViewModel>> getAllEmployeeTarget(string empId)
        {
            if (string.IsNullOrEmpty(empId))
            {
                return new List<EmployeeTargetViewModel>();
            }

            string sqlQuery = $@"
                SELECT 
                    item_name, 
                    item_newid, 
                    item_pack_size, 
                    item_code, 
                    pts, 
                    ptr, 
                    mrp, 
                    CAST(sample_rate AS float) AS nrv, 
                    purchase_rate
                FROM 
                    item_details
                WHERE 
                    state_main IN (
                        SELECT state_main 
                        FROM emp_detail 
                        WHERE emp_id = @empId
                    )
                    AND TYPE = 'Product'
                    AND item_status = 'Active'
                ORDER BY 
                    item_name";

            var items = await db.Database
                .SqlQueryRaw<EmployeeTargetViewModel>(sqlQuery, new MySqlParameter("@empId", empId))
                .ToListAsync();

            return items ?? new List<EmployeeTargetViewModel>();
        }
    }
}