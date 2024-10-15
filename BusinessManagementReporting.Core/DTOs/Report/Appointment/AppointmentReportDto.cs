using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Core.DTOs.Report.Appointment
{
    public class AppointmentReportDto
    {
        public int TotalAppointments { get; set; }
        public List<AppointmentsByServiceDto> AppointmentsByService { get; set; }
        public List<AppointmentsByBranchDto> AppointmentsByBranch { get; set; }
        public List<AppointmentsByStatusDto> AppointmentsByStatus { get; set; }
    }
}
