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
    public class HeadquarterController : ControllerBase
    {
        private readonly IHeadQuarterViewService _headquarterService;

        public HeadquarterController(IHeadQuarterViewService headquarterService)
        {
            _headquarterService = headquarterService;
        }

        // Relative route (do NOT start with "/")
        [HttpGet("getallheadquarters")]
        public async  Task<ActionResult<MyJsonReturn<List<HeadQuarterViewModel>>>> GetAllHeadquarters()
        {
            try
            {
                var all = await _headquarterService.getAllHeadQuarters();

                return new MyJsonReturn<List<HeadQuarterViewModel>>
                {
                    isSuccess = true,
                    status = System.Net.HttpStatusCode.OK,
                    message = "Headquarters fetched successfully",
                    stackTrace = null,
                    data = all
                };
            }
            catch (Exception ex)
            {
                return new MyJsonReturn<List<HeadQuarterViewModel>>
                {
                    isSuccess = false,
                    status = System.Net.HttpStatusCode.InternalServerError,
                    message = "Error fetching headquarters.",
                    stackTrace = new List<string> { ex.Message },
                    data = null
                };
            }
        }
    }
}
