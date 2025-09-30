using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Application.Services
{
    public class EmployeeTargetService : IEmployeeTargetService
    {
        private readonly IEmployeeTargetRepository employeeTargetRepository;
        public EmployeeTargetService(IEmployeeTargetRepository employeeTargetRepository)
        {
            this.employeeTargetRepository = employeeTargetRepository;
        }
        public async Task<List<EmployeeTargetViewModel>> getAllEmployeeTarget(string empId, string itemType, string itemStatus)
        {
            return await employeeTargetRepository.getAllEmployeeTarget(empId, itemType, itemStatus);
        }
    }
}