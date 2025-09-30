using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeMasterViewModel>> getAll();
        Task<EmployeeMasterViewModel> getbyId(string id);

        Task<EmployeeMasterViewModel> createEmployee(EmployeeMasterViewModel employee);
    }
}