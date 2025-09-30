using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Domain.Interfaces
{
    public interface IEmployeeTargetRepository
    {
        public Task<List<EmployeeTargetViewModel>> getAllEmployeeTarget(string empId, string itemType, string itemStatus);
    }
}