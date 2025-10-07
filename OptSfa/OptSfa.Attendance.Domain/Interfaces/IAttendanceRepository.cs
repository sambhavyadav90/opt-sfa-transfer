using OptSfa.Attendance.Domain.ViewModels;

namespace OptSfa.Attendance.Domain.Interfaces;

public interface IAttendanceRepository
{
    Task<object> GetAttendance(string fromDate, string toDate);
}
