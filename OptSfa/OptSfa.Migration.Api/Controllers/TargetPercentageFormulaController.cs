using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain;
using OptSfa.Migration.Domain.Models;
using OptSfa.Migration.Domain.ViewModel;
namespace OptSfa.Migration.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    public class TargetPercentageFormulaController : ControllerBase
    {
        private readonly ITargetPecentageFormulaService targetPecentageFormulaService;
        public TargetPercentageFormulaController(ITargetPecentageFormulaService targetPecentageFormulaService)
        {
            this.targetPecentageFormulaService = targetPecentageFormulaService;
        }

        [HttpGet("GetAllTargets")]
        public async Task<ActionResult<MyJsonReturn<List<TargetPercentFormula>>>> GetAll()
        {
            try
            {
                var res = await targetPecentageFormulaService.getAllTargetPercents();
                return Ok(new MyJsonReturn<List<TargetPercentFormula>>
                {
                    data = res,
                    status = HttpStatusCode.OK,
                    isSuccess = true,
                    message = "All Target Percentage Data Fetched Successfully",
                    stackTrace = null,
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new MyJsonReturn<List<TargetPercentFormula>>
                {
                    isSuccess = false,
                    status = System.Net.HttpStatusCode.InternalServerError,
                    message = "Error fetching headquarters.",
                    stackTrace = new List<string> { ex.Message },
                    data = null
                });
            }
        }
        [HttpPost("CreateTarget")]
        public async Task<ActionResult<MyJsonReturn<TargetPercentFomulaRequest>>> CreateNewTarget([FromBody] TargetPercentFomulaRequest data)
        {
            try
            {
                var res = await targetPecentageFormulaService.createTargetPercentFormula(data);
                return Ok(new MyJsonReturn<TargetPercentFomulaRequest>
                {
                    data = res,
                    status = HttpStatusCode.OK,
                    isSuccess = true,
                    message = "Target Saved Successfully",
                    stackTrace = null,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new MyJsonReturn<TargetPercentFormula>
                {
                    data = null,
                    status = HttpStatusCode.BadRequest,
                    isSuccess = false,
                    message = "Problem while saving the target",
                    stackTrace = new List<string> { ex.Message },
                });
            }
        }

        [HttpPost("UpdateTarget")]
        public async Task<ActionResult<MyJsonReturn<TargetPercentFormula>>> UpdateTarget([FromQuery] int rowid, [FromQuery] float rate)
        {
            try
            {
                var res = await targetPecentageFormulaService.updateTargetPercent(rowid, rate);
                return Ok(new MyJsonReturn<TargetPercentFormula>
                {
                    data = res,
                    status = HttpStatusCode.OK,
                    isSuccess = true,
                    message = "Target Updated Successfully",
                    stackTrace = null,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new MyJsonReturn<TargetPercentFormula>
                {
                    data = null,
                    status = HttpStatusCode.BadRequest,
                    isSuccess = true,
                    message = "Problem while Updating the target",
                    stackTrace = new List<string> { ex.Message },
                });
            }
        }
    }
}