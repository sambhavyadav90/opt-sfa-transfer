using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Domain.Models;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Application.Interfaces
{
    public interface ITargetPecentageFormulaService
    {
      public Task<List<TargetPercentFormula>> getAllTargetPercents();
        public Task<TargetPercentFomulaRequest> createTargetPercentFormula(TargetPercentFomulaRequest targetPercentViewModel);
        public Task<TargetPercentFormula> updateTargetPercent(int rowId, float rate);
    }
}