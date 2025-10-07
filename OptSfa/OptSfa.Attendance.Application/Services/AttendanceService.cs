using OptSfa.Attendance.Application.Interfaces;
using OptSfa.Attendance.Domain.Interfaces;

namespace OptSfa.Attendance.Application.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _attendanceRepository;
    public AttendanceService(IAttendanceRepository attendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
    }

    public async Task<object> GetAttendance(string fromDate, string toDate)
    {
        var result = await _attendanceRepository.GetAttendance(fromDate, toDate);
        return result;
    }
}
