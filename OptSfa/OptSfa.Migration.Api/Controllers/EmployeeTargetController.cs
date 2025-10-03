using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    public class EmployeeTargetController : ControllerBase
    {
        private readonly IEmployeeTargetService _employeeTargetService;

        public EmployeeTargetController(IEmployeeTargetService employeeTargetService)
        {
            _employeeTargetService = employeeTargetService;
        }

        [HttpGet("getemployeetarget/{empId}")]
        public async Task<ActionResult<MyJsonReturn<List<EmployeeTargetViewModel>>>> GetEmployeeTarget(string empId)
        {
            try
            {
                var result = await _employeeTargetService.getAllEmployeeTarget(empId);

                return Ok(new MyJsonReturn<List<EmployeeTargetViewModel>>
                {
                    isSuccess = true,
                    status = System.Net.HttpStatusCode.OK,
                    message = "Employee targets fetched successfully",
                    stackTrace = null,
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MyJsonReturn<List<EmployeeTargetViewModel>>
                {
                    isSuccess = false,
                    status = System.Net.HttpStatusCode.InternalServerError,
                    message = "Error fetching employee targets.",
                    stackTrace = new List<string> { ex.Message },
                    data = null
                });
            }
        }

        [HttpPost("CreateEmployeeTarget")]
        public async Task<ActionResult<MyJsonReturn<EmployeeTargetCreateListRequest>>> createEmployeeTarget([FromBody] EmployeeTargetCreateListRequest inputdata)
        {
            try
            {
                bool flag = await _employeeTargetService.createEmployee(inputdata);
                return Ok(new MyJsonReturn<EmployeeTargetCreateListRequest>
                {
                    isSuccess = true,
                    status = System.Net.HttpStatusCode.OK,
                    message = "Employee targets fetched successfully",
                    stackTrace = null,
                    data = inputdata
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MyJsonReturn<List<EmployeeTargetCreateListRequest>>
                {
                    isSuccess = false,
                    status = System.Net.HttpStatusCode.InternalServerError,
                    message = "Error fetching employee targets.",
                    stackTrace = new List<string> { ex.Message },
                    data = null
                });
            }
        }
    }
}
