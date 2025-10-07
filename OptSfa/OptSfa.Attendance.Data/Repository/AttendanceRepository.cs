using Microsoft.EntityFrameworkCore;
using OptSfa.Attendance.Data.Context;
using OptSfa.Attendance.Domain.Interfaces;
using OptSfa.Attendance.Domain.ViewModels;

namespace OptSfa.Attendance.Data.Repository;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly AppDbContext _context;

    public AttendanceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<object> GetAttendance(string fromDate, string toDate)
    {
        // 1. Get all attendance records
        string dcrQuery = string.Format("SELECT dp.dcr_id AS dcrId, dp.dcr_i_date AS workDate, dp.client_status AS workType, dp.dcr_time_in AS startDayTime, dp.dcr_time_out AS endDayTime, dp.emp_remark AS remarks, ed.name, ed.designations_oid AS designation, ed.map_dis AS empCode, ed.district, ed.state, dp.immidiate_mgr AS manager FROM dcr_parent_summary AS dp JOIN emp_detail AS ed ON(dp.emp_id = ed.emp_id) WHERE dp.dcr_i_date BETWEEN '{0}' AND '{1}'; ", fromDate, toDate);
        var dcrData = await _context.Database.SqlQueryRaw<AttendanceViewModel>(dcrQuery).ToListAsync();

        // 2. Get punch time data for all dcrIds in the date range
        string punchTimeQuery = string.Format(@"SELECT CAST(dc.dcr_id AS UNSIGNED) AS dcrId,
                                                               MIN(dc.time_emp_clt) AS firstPunchIn,
                                                               MIN(dc.out_time) AS firstPunchOut,
                                                               MAX(dc.time_emp_clt) AS lastPunchIn,
                                                               MAX(dc.out_time) AS lastPunchOut,
                                                               SUM(CASE WHEN dc.client_type = 'Client' THEN 1 ELSE 0 END) AS clientVisits,
                                                               SUM(CASE WHEN dc.client_type = 'Retailer' THEN 1 ELSE 0 END) AS retailerVisits,
                                                               SUM(CASE WHEN dc.client_type = 'Party' THEN 1 ELSE 0 END) AS partyVisits
                                                        FROM dcr_client_summary AS dc
                                                        WHERE dc.dcr_id IN ({0})
                                                        GROUP BY dc.dcr_id;", string.Join(",", dcrData.Select(x => $"'{x.dcrId}'")));
        var punchData = await _context.Database.SqlQueryRaw<AttendancePunchTimeViewModel>(punchTimeQuery).ToListAsync();

        // 3. Merge both datasets by dcrId
        var result = (from dcr in dcrData
                      join punch in punchData on dcr.dcrId equals punch.dcrId into punchGroup
                      from punch in punchGroup.DefaultIfEmpty()
                      select new
                      {
                          dcr.dcrId,
                          dcr.workDate,
                          dcr.workType,
                          dcr.startDayTime,
                          dcr.endDayTime,
                          dcr.remarks,
                          dcr.name,
                          dcr.designation,
                          dcr.empCode,
                          dcr.district,
                          dcr.state,
                          dcr.manager,
                          punch?.firstPunchIn,
                          punch?.firstPunchOut,
                          punch?.lastPunchIn,
                          punch?.lastPunchOut,
                          punch?.clientVisits,
                          punch?.retailerVisits,
                          punch?.partyVisits
                      }).ToList();

        return result;
    }
}
