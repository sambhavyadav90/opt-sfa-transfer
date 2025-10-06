using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Application.Interfaces
{
    public interface IStateMasterService
    {
         public Task<List<StateMasterViewModel>> getAll(string status, string stateMain);
    }
}