namespace BusinessManagementReporting.Core.DTOs.Report.Appointment
{
    public class AppointmentsByServiceDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int AppointmentCount { get; set; }
    }
}
