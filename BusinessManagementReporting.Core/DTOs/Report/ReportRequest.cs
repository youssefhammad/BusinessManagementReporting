using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Core.DTOs.Report
{
    public class ReportRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? BranchId { get; set; }
        public List<int>? ServiceIds { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
