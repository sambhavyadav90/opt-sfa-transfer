using Microsoft.AspNetCore.Mvc;

namespace OptSfa.Gateway.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GatewayController : ControllerBase
{
    public IActionResult TestSuccess()
    {
        return Ok(new { success = true });
    }
}
