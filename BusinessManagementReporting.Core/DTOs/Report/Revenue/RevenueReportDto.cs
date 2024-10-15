using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Core.DTOs.Report.Revenue
{
    public class RevenueReportDto
    {
        public decimal TotalRevenue { get; set; }
        public List<RevenueByServiceDto> RevenueByService { get; set; }
        public List<RevenueByBranchDto> RevenueByBranch { get; set; }
        public List<RevenueByPaymentMethodDto> RevenueByPaymentMethod { get; set; }
    }
}
