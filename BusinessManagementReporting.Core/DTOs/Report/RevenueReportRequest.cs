using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Core.DTOs.Report
{
    public class RevenueReportRequest : BaseReportRequest
    {
        public string? PaymentMethod { get; set; }
    }
}
