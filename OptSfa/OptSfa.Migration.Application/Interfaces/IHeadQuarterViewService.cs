using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Application.Interfaces
{
    public interface IHeadQuarterViewService
    {
         public Task<List<HeadQuarterViewModel>> getAllHeadQuarters();
    }
}