using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public Task<EmployeeMasterViewModel> createEmployee(EmployeeMasterViewModel employee)
        {
            return employeeRepository.createEmployee(employee);
        }

        public Task<List<EmployeeMasterViewModel>> getAll()
        {
            return employeeRepository.getAll();
        }

        public Task<EmployeeMasterViewModel> getbyId(string id)
        {
            return employeeRepository.getbyId(id);
        }
    }
}