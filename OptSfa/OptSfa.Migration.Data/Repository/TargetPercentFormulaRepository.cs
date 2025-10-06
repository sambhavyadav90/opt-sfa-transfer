
using Microsoft.EntityFrameworkCore;
using OptSfa.Migration.Data.Context;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.Models;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Data.Repository
{
    public class TargetPercentFormulaRepository : ITargetPercentFormulaRepository
    {
        private readonly AppDbContext db;
        public TargetPercentFormulaRepository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<TargetPercentFomulaRequest> createTargetPercentFormula(TargetPercentFomulaRequest data)
        {
            List<TargetPercentFormula> targetPercentViewModel = data.inputdata;
            if (targetPercentViewModel == null || !targetPercentViewModel.Any())
                throw new ArgumentNullException(nameof(targetPercentViewModel));


            var res = targetPercentViewModel
                        .Select(x => new TargetPercentFormula
                        {
                            row_id = 0,
                            year = x.year == null ? 0 : x.year,
                            month = x.month == null ? 0 : x.month,
                            percentage = x.percentage == null ? 0 : x.percentage,
                            create_by = x.create_by == null ? "" : x.create_by,
                            create_date = DateTime.Now
                        })
                        .ToList();

            await db.targetPercentFormulaMasters.AddRangeAsync(res);
            await db.SaveChangesAsync();
            return data;
        }


        public async Task<List<TargetPercentFormula>> getAllTargetPercents()
        {
            var data = await db.targetPercentFormulaMasters.ToListAsync();

            return data;
        }

        public async Task<TargetPercentFormula> updateTargetPercent(int rowId, float newrate)
        {
            var existing = await db.targetPercentFormulaMasters
                                   .FirstOrDefaultAsync(x => x.row_id == rowId);



            existing.percentage = newrate;

            db.targetPercentFormulaMasters.Update(existing);
            await db.SaveChangesAsync();


            return existing;
        }
    }
}