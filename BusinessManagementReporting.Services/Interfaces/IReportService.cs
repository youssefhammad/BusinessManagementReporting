using BusinessManagementReporting.Core.DTOs.Report.Appointment;
using BusinessManagementReporting.Core.DTOs.Report.CustomerDemographics;
using BusinessManagementReporting.Core.DTOs.Report.Revenue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Services.Interfaces
{
    public interface IReportService
    {
        Task<RevenueReportDto> GetRevenueReportAsync(DateTime? startDate = null, DateTime? endDate = null, int? branchId = null, List<int>? serviceIds = null, string? paymentMethod = null);
        Task<AppointmentReportDto> GetAppointmentReportAsync(
        DateTime? startDate = null,
        DateTime? endDate = null,
        int? branchId = null,
        List<int>? serviceIds = null,
        string? paymentMethod = null);
        Task<CustomerDemographicsReportDto> GetCustomerDemographicsReportAsync(int? branchId, string? gender = null, DateTime? birthDateStart = null, DateTime? birthDateEnd = null);
    }
}
