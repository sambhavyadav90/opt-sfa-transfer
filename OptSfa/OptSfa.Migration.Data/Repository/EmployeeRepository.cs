using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using OptSfa.Migration.Data.Context;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.Models;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext db;
        public EmployeeRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<EmployeeMasterViewModel> createEmployee(EmployeeMasterViewModel employee)
        {

            EmployeeMaster newEmployee = new EmployeeMaster
            {
                empId = employee.empId,
                name = employee.name,
                userName = employee.userName,
                userPassword = employee.userPassword,
                designationsOid = employee.designationsOid,
                district = employee.district,
                distrectId = employee.distrectId,
                State = employee.State,
                stateMain = employee.stateMain,
                dateJoining = employee.dateJoining,
                mobile = employee.mobile,
                imageePath = employee.imageePath,
                empLevel = employee.empLevel,
                email = employee.email,
                dob = employee.dob,
                status = employee.status,
                gender = employee.gender,
                stopReporting = employee.stopReporting,
                isValid = employee.isValid,
                isLogout = employee.isLogout
            };

            await db.employeeMasters.AddAsync(newEmployee);
            await db.SaveChangesAsync();

            return employee;
        }


        public async Task<List<EmployeeMasterViewModel>> getAll()
        {
            List<EmployeeMaster> employeeList = await db.employeeMasters.ToListAsync();

            List<EmployeeMasterViewModel> result = employeeList.Select(e => new EmployeeMasterViewModel
            {
                empId = e.empId,
                name = e.name,
                userName = e.userName,
                userPassword = e.userPassword,
                designationsOid = e.designationsOid,
                district = e.district,
                distrectId = e.distrectId,
                State = e.State,
                stateMain = e.stateMain,
                dateJoining = e.dateJoining,
                mobile = e.mobile,
                imageePath = e.imageePath,
                empLevel = e.empLevel,
                email = e.email,
                dob = e.dob,
                status = e.status,
                gender = e.gender,
                stopReporting = e.stopReporting,
                isValid = e.isValid,
                isLogout = e.isLogout
            }).ToList();

            return result;
        }


        public async Task<EmployeeMasterViewModel> getbyId(string id)
        {

            EmployeeMaster? employee = await db.employeeMasters.FirstOrDefaultAsync(e => e.empId == id);

            if (employee == null)
            {
                return null;
            }

            EmployeeMasterViewModel result = new EmployeeMasterViewModel
            {
                empId = employee.empId,
                name = employee.name,
                userName = employee.userName,
                userPassword = employee.userPassword,
                designationsOid = employee.designationsOid,
                district = employee.district,
                distrectId = employee.distrectId,
                State = employee.State,
                stateMain = employee.stateMain,
                dateJoining = employee.dateJoining,
                mobile = employee.mobile,
                imageePath = employee.imageePath,
                empLevel = employee.empLevel,
                email = employee.email,
                dob = employee.dob,
                status = employee.status,
                gender = employee.gender,
                stopReporting = employee.stopReporting,
                isValid = employee.isValid,
                isLogout = employee.isLogout
            };

            return result;
        }

    }
}