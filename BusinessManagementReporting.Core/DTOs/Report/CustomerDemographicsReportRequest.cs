using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Core.DTOs.Report
{
    public class CustomerDemographicsReportRequest
    {
        public int? BranchId { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDateStart { get; set; }
        public DateTime? BirthDateEnd { get; set; }
    }
}
