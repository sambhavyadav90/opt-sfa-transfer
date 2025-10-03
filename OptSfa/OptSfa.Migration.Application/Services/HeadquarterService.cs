using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Application.Services
{
    public class HeadquarterService : IHeadQuarterViewService
    {
        private readonly IHeadQuarterViewRepository headQuarterViewRepository;
        public HeadquarterService(IHeadQuarterViewRepository headQuarterViewRepository)
        {
            this.headQuarterViewRepository = headQuarterViewRepository;
        }
        public async Task<List<HeadQuarterViewModel>> getAllHeadQuarters()
        {
            return await headQuarterViewRepository.getAllHeadQuarters();
        }
    }
}