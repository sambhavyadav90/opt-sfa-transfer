using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Application.Services
{
    public class EmployeeDetailsService : IEmployeeDetailsService
    {
        private readonly IEmployeeDetailsRepository employeeDetailsRepository;
        public EmployeeDetailsService(IEmployeeDetailsRepository employeeDetailsRepository)
        {
            this.employeeDetailsRepository = employeeDetailsRepository;
        }

        public async Task<List<EmployeeDetailsViewModel>> getAllEmployeesAsync(string distrectid, string state_main, string designations_oid, string emp_id)
        {
            return await employeeDetailsRepository.GetAllEmployees(distrectid, state_main, designations_oid, emp_id);

        }

    }
}