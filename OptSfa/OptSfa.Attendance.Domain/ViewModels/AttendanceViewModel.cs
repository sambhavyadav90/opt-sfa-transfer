namespace OptSfa.Attendance.Domain.ViewModels;

public class AttendanceViewModel
{
    public int dcrId { get; set; }                  // dp.dcr_id
    public DateTime workDate { get; set; }          // dp.dcr_i_date
    public string workType { get; set; }            // dp.client_status
    public string startDayTime { get; set; }     // dp.dcr_time_in
    public string endDayTime { get; set; }       // dp.dcr_time_out
    public string remarks { get; set; }             // dp.emp_remark
    public string name { get; set; }                // ed.name
    public string designation { get; set; }            // ed.designations_oid
    public string empCode { get; set; }             // ed.map_dis
    public string district { get; set; }            // ed.district
    public string state { get; set; }               // ed.state
    public string manager { get; set; }             // dp.immidiate_mgr
}

public class AttendancePunchTimeViewModel
{
    public int dcrId { get; set; }
    public string firstPunchIn { get; set; }      // MIN(dc.time_emp_clt)
    public string firstPunchOut { get; set; }     // MIN(dc.out_time)
    public string lastPunchIn { get; set; }       // MAX(dc.time_emp_clt)
    public string lastPunchOut { get; set; }      // MAX(dc.out_time)
    public int? clientVisits { get; set; }            // SUM(...) WHEN client_type = 'Client'
    public int? retailerVisits { get; set; }          // SUM(...) WHEN client_type = 'Retailer'
    public int? partyVisits { get; set; }
}
