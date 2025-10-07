namespace OptSfa.Attendance.Application.Interfaces;

public interface IAttendanceService
{
    Task<object> GetAttendance(string fromDate, string toDate);
}
