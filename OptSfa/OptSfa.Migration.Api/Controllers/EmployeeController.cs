using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        public readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet("/getallemployee")]
        public async Task<IActionResult> getAllEmployee()
        {
            try
            {
                List<EmployeeMasterViewModel> allEmployees = await employeeService.getAll();

                return Ok(allEmployees);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching employees.", error = ex.Message });
            }
        }

        [HttpGet("/getemployeebyid/{id}")]
        public async Task<IActionResult> getById([FromRoute] string id)
        {
            try
            {
                EmployeeMasterViewModel employee = await employeeService.getbyId(id);

                if (employee == null)
                {
                    return NotFound(new { message = $"Employee with id {id} not found." });
                }


                return Ok(employee);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the employee.", error = ex.Message });
            }
        }

        [HttpPost("/createemployee")]
        public async Task<IActionResult> createEmployee([FromBody] EmployeeMasterViewModel employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest(new { message = "Employee data is required." });
                }


                EmployeeMasterViewModel createdEmployee = await employeeService.createEmployee(employee);


                return CreatedAtAction(nameof(getById), new { id = createdEmployee.empId }, createdEmployee);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the employee.", error = ex.Message });
            }
        }

    }
}