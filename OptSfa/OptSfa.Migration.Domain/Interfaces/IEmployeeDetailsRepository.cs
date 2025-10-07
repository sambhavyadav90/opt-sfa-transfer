using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Domain.Interfaces
{
    public interface IEmployeeDetailsRepository
    {
        public Task<List<EmployeeDetailsViewModel> > GetAllEmployees(string distrectid, string state_main, string designations_oid, string emp_id);
    }
}