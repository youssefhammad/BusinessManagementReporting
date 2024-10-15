namespace BusinessManagementReporting.Core.DTOs.Report.Revenue
{
    public class RevenueByBranchDto
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
