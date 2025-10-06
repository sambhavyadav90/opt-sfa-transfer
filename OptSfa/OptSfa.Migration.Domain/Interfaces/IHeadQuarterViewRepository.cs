using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Domain.Interfaces
{
    public interface IHeadQuarterViewRepository
    {
        public Task<List<HeadQuarterViewModel>> getAllHeadQuarters();
    }
}