using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain;
using OptSfa.Migration.Domain.Models;

namespace OptSfa.Migration.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaveDataMasterController : ControllerBase
    {
        private readonly ISaveMasterService saveMasterService;
        public SaveDataMasterController(ISaveMasterService saveMasterService)
        {
            this.saveMasterService = saveMasterService;
        }

        [HttpPost("CreateData")]
        public async Task<ActionResult<MyJsonReturn<SaveDataMaster>>> Create([FromBody] SaveDataMaster data)
        {
            try
            {
                var res = await saveMasterService.saveData(data);
                return new MyJsonReturn<SaveDataMaster>
                {
                    isSuccess = true,
                    status = System.Net.HttpStatusCode.OK,
                    message = "Data Saved Successfully",
                    stackTrace = null,
                    data = res
                };
            }
            catch (Exception ex)
            {
                return new MyJsonReturn<SaveDataMaster>
                {
                    isSuccess = false,
                    status = System.Net.HttpStatusCode.BadRequest,
                    message = "Unable to save data",
                    stackTrace = null,
                    data = null
                };
            }
        }
    }
}