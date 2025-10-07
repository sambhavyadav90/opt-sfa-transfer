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

        [HttpGet("GetAll")]
        public async  Task<ActionResult<MyJsonReturn<List<HeadQuaterMasterViewModel>>>> GetAllHeadquarters([FromQuery] int id)
        {
            try
            {
                var datas = await _headquarterService.getAllHeadquaters(id);

                return new MyJsonReturn<List<HeadQuaterMasterViewModel>>
                {
                    isSuccess = true,
                    status = System.Net.HttpStatusCode.OK,
                    message = "Headquarters fetched successfully",
                    stackTrace = null,
                    data = datas
                };
            }
            catch (Exception ex)
            {
                return new MyJsonReturn<List<HeadQuaterMasterViewModel>>
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
