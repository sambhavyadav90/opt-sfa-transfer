using Microsoft.AspNetCore.Mvc;
using OptSfa.Attendance.Application.Interfaces;
using OptSfa.Attendance.Domain.Common;
using System.Net;

namespace OptSfa.Attendance.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;
    public AttendanceController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    [HttpGet("GetAttendance")]
    public async Task<IActionResult> GetAttendance(string fromDate, string toDate)
    {
        var appKey = Request.Headers["x-app-key"].ToString();

        if (string.IsNullOrEmpty(appKey))
        {
            return BadRequest(new MyJsonReturn<object>
            {
                status = HttpStatusCode.BadRequest,
                isSuccess = false,
                message = "Missing x-app-key"
            });
        }

        if (appKey != "E3888F2D-B42C-4686-96E6-C0F19234AF59")
        {
            return BadRequest(new MyJsonReturn<object>
            {
                status = HttpStatusCode.Unauthorized,
                isSuccess = false,
                message = "Invalid app key. Please provide a valid app key."
            });
        }

        var result = await _attendanceService.GetAttendance(fromDate, toDate);
        return Ok(new MyJsonReturn<object>
        {
            status = HttpStatusCode.OK,
            isSuccess = true,
            message = "Data populated successfully",
            data = new {Attendance = result}
        });
    }
}
