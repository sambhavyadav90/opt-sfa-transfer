
using Microsoft.AspNetCore.Mvc;
using OptSfa.Migration.Application.Interfaces;

namespace OptSfa.Migration.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeTargetController : ControllerBase
    {
        private readonly IEmployeeTargetService _employeeTargetService;

        public EmployeeTargetController(IEmployeeTargetService employeeTargetService)
        {
            _employeeTargetService = employeeTargetService;
        }

        [HttpGet("getemployeetarget/{empId}/{itemType}/{itemStatus}")]
        public async Task<IActionResult> GetEmployeeTarget(string empId, string itemType, string itemStatus)
        {
            var result =await _employeeTargetService.getAllEmployeeTarget(empId, itemType, itemStatus);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
