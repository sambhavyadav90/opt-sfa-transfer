using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeDetailsController : ControllerBase
    {
        private readonly IEmployeeDetailsService employeeDetailsService;
        public EmployeeDetailsController(IEmployeeDetailsService employeeDetailsService)
        {
            this.employeeDetailsService = employeeDetailsService;
        }

        [HttpGet]
        public async Task<ActionResult<MyJsonReturn<List<EmployeeDetailsViewModel>>>> GetAllEmployeed(
            [FromQuery] string distrectid = null, 
            [FromQuery] string state_main = null, 
            [FromQuery] string designations_oid = null, 
            [FromQuery] string emp_id = null)
        {
            var response = new MyJsonReturn<List<EmployeeDetailsViewModel>>
            {
                isSuccess = false,
                status = System.Net.HttpStatusCode.InternalServerError,
                message = string.Empty,
                stackTrace = new List<string>()
            };

            try
            {
                var result = await employeeDetailsService.getAllEmployeesAsync(distrectid, state_main, designations_oid, emp_id);
                response.isSuccess = true;
                response.status = System.Net.HttpStatusCode.OK;
                response.data = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                if (ex.StackTrace != null)
                {
                    response.stackTrace = ex.StackTrace.Split('\n').ToList();
                }
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, response);
            }
        }
    }
}