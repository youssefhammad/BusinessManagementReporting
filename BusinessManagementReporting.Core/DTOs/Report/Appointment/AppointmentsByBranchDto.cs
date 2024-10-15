namespace BusinessManagementReporting.Core.DTOs.Report.Appointment
{
    public class AppointmentsByBranchDto
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int AppointmentCount { get; set; }
    }
}
