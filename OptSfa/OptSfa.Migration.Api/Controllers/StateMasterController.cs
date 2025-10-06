using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateMasterController : ControllerBase
    {
        private readonly IStateMasterService stateMasterService;
        public StateMasterController(IStateMasterService stateMasterService)
        {
            this.stateMasterService = stateMasterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<MyJsonReturn<List<StateMasterViewModel>>>> GetAllStates(
            [FromQuery] string? status = null,
            [FromQuery] string? stateMain = null
        )
        {
            try
            {
                var states = await stateMasterService.getAll(status, stateMain);
                return Ok(new MyJsonReturn<List<StateMasterViewModel>>
                {
                    data = states,
                    status = HttpStatusCode.OK,
                    isSuccess = true,
                    message = "All States Fetched Successfully",
                    stackTrace = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new MyJsonReturn<List<StateMasterViewModel>>
                {
                    isSuccess = false,
                    status = HttpStatusCode.InternalServerError,
                    message = "Error fetching States.",
                    stackTrace = new List<string> { ex.Message },
                    data = null
                });
            }
        }
    }
}