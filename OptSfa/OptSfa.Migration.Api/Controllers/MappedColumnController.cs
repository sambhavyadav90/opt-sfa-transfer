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
    public class MappedColumnController : ControllerBase
    {
        private readonly IMappedDbColumnService mappedDbColumnService;

        public MappedColumnController(IMappedDbColumnService mappedDbColumnService)
        {
            this.mappedDbColumnService = mappedDbColumnService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<MyJsonReturn<List<MappedDbColumnTran>>>> GetAll()
        {
            try
            {
                var res = await mappedDbColumnService.getAll();

                return new MyJsonReturn<List<MappedDbColumnTran>>
                {
                    isSuccess = true,
                    status = System.Net.HttpStatusCode.OK,
                    message = "All columns fetched successfully",
                    stackTrace = null,
                    data = res
                };
            }
            catch (Exception ex)
            {
                return new MyJsonReturn<List<MappedDbColumnTran>>
                {
                    isSuccess = false,
                    status = System.Net.HttpStatusCode.InternalServerError,
                    message = "Unable to fetch columns",
                    stackTrace = null,
                    data = null
                };
            }
        }

        [HttpGet("GetByDbColumn")]
        public async Task<ActionResult<MyJsonReturn<List<MappedDbColumnTran>>>> GetByDbColumn(
            [FromQuery] string? dbcolumn = null,
            [FromQuery] string? dbcolumnmapping = null)
        {
            try
            {
                var res = await mappedDbColumnService.getByDbColumn(dbcolumn, dbcolumnmapping);

                if (res == null || !res.Any())
                {
                    return new MyJsonReturn<List<MappedDbColumnTran>>
                    {
                        isSuccess = false,
                        status = System.Net.HttpStatusCode.NotFound,
                        message = "No records found for the given column and mapping",
                        stackTrace = null,
                        data = null
                    };
                }

                return new MyJsonReturn<List<MappedDbColumnTran>>
                {
                    isSuccess = true,
                    status = System.Net.HttpStatusCode.OK,
                    message = "Mapped columns fetched successfully",
                    stackTrace = null,
                    data = res
                };
            }
            catch (Exception ex)
            {
                return new MyJsonReturn<List<MappedDbColumnTran>>
                {
                    isSuccess = false,
                    status = System.Net.HttpStatusCode.InternalServerError,
                    message = "Error while fetching mapped columns",
                    stackTrace = null,
                    data = null
                };
            }
        }
    }
}