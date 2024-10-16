using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Core.DTOs.Report
{
    public class AppointmentReportRequest : BaseReportRequest
    {
        public string? BookingStatus { get; set; }
    }
}
