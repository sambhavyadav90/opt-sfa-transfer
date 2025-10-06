using System.Net;
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
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<MyJsonReturn<List<ClientMasterViewModel>>>> GetAllClients(
            [FromQuery] string? empId = null,
            [FromQuery] string? clientType = null,
            [FromQuery] string? areaMain = null,
            [FromQuery] string? status = null, [FromQuery] int page = 0, [FromQuery] int pageSize = 20)
        {
            try
            {
                var clients = await _clientService.getAll(empId, clientType, areaMain, status, page, pageSize);

                return Ok(new MyJsonReturn<List<ClientMasterViewModel>>
                {
                    data = clients,
                    status = HttpStatusCode.OK,
                    isSuccess = true,
                    message = "All Clients Fetched Successfully",
                    stackTrace = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new MyJsonReturn<List<ClientMasterViewModel>>
                {
                    isSuccess = false,
                    status = HttpStatusCode.InternalServerError,
                    message = "Error fetching Clients.",
                    stackTrace = new List<string> { ex.Message },
                    data = null
                });
            }
        }
    }
}
