using Microsoft.AspNetCore.Mvc;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeadquarterController : ControllerBase
    {
        private readonly IHeadquarterService headquarterService;
        public HeadquarterController(IHeadquarterService headquarterService)
        {
            this.headquarterService = headquarterService;
        }

        [HttpPost("/createheadquarter")]
        public async Task<IActionResult> CreateHeadquarter([FromBody] HeadQuaterMasterViewModel data)
        {
            if (data == null)
                return BadRequest(new { message = "Headquarter data is required." });

            try
            {
                var created = await headquarterService.createHeadquarter(data);
                return CreatedAtAction(nameof(GetById), new { id = created.districtId }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating headquarter.", error = ex.Message });
            }
        }

        [HttpGet("/getallheadquarters")]
        public async Task<IActionResult> GetAllHeadquarters()
        {
            try
            {
                var all = await headquarterService.getAllHeadquaters();
                return Ok(all);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching headquarters.", error = ex.Message });
            }
        }

        [HttpGet("/getheadquarterbyid/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var hq = await headquarterService.getbyId(id);
                if (hq == null)
                    return NotFound(new { message = $"Headquarter with id {id} not found." });
                return Ok(hq);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching headquarter.", error = ex.Message });
            }
        }
    }
}
