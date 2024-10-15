namespace BusinessManagementReporting.Core.DTOs.Report.Revenue
{
    public class RevenueByPaymentMethodDto
    {
        public string PaymentMethod { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
