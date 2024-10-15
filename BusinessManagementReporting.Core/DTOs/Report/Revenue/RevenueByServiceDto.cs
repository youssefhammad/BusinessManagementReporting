namespace BusinessManagementReporting.Core.DTOs.Report.Revenue
{
    public class RevenueByServiceDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
