using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.Models;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Application.Services
{
    public class TargetFormulaService : ITargetPecentageFormulaService
    {
        private readonly ITargetPercentFormulaRepository targetPercentFormulaRepository;
        public TargetFormulaService(ITargetPercentFormulaRepository targetPercentFormulaRepository)
        {
            this.targetPercentFormulaRepository = targetPercentFormulaRepository;
        }

        public async Task<TargetPercentFomulaRequest> createTargetPercentFormula(TargetPercentFomulaRequest targetPercentViewModel)
        {
            return await targetPercentFormulaRepository.createTargetPercentFormula(targetPercentViewModel);
        }

        public async Task<List<TargetPercentFormula>> getAllTargetPercents()
        {
            return await targetPercentFormulaRepository.getAllTargetPercents();
        }

        public Task<TargetPercentFormula> updateTargetPercent(int rowId, float rate)
        {
            return targetPercentFormulaRepository.updateTargetPercent(rowId, rate);
        }
    }
}