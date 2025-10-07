using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Application.Interfaces
{
    public interface IEmployeeDetailsService
    {
        public Task<List<EmployeeDetailsViewModel> > getAllEmployeesAsync(string distrectid, string state_main, string designations_oid, string emp_id);

    }
}