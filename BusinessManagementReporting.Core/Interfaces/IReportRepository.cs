using BusinessManagementReporting.Core.DTOs.Report.Appointment;
using BusinessManagementReporting.Core.DTOs.Report.CustomerDemographics;
using BusinessManagementReporting.Core.DTOs.Report.Revenue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Core.Interfaces
{
    public interface IReportRepository
    {
        // Revenue Report Methods
        Task<decimal> GetTotalRevenueAsync(DateTime? startDate = null, DateTime? endDate = null, int? branchId = null, List<int>? serviceIds = null, string? paymentMethod = null);
        Task<List<RevenueByServiceDto>> GetRevenueByServiceAsync(DateTime? startDate = null, DateTime? endDate = null, int? branchId = null, List<int>? serviceIds = null, string? paymentMethod = null);
        Task<List<RevenueByBranchDto>> GetRevenueByBranchAsync(DateTime? startDate = null, DateTime? endDate = null, List<int>? serviceIds = null, string? paymentMethod = null);
        Task<List<RevenueByPaymentMethodDto>> GetRevenueByPaymentMethodAsync(DateTime? startDate = null, DateTime? endDate = null, int? branchId = null, List<int>? serviceIds = null);

        // Appointment Report Methods
        Task<int> GetTotalAppointmentsAsync(DateTime? startDate = null, DateTime? endDate = null, int? branchId = null, List<int>? serviceIds = null, string? status = null);
        Task<List<AppointmentsByServiceDto>> GetAppointmentsByServiceAsync(DateTime? startDate = null, DateTime? endDate = null, int? branchId = null, List<int>? serviceIds = null);
        Task<List<AppointmentsByBranchDto>> GetAppointmentsByBranchAsync(DateTime? startDate = null, DateTime? endDate = null, int? branchId = null, List<int>? serviceIds = null, string? status = null);
        Task<List<AppointmentsByStatusDto>> GetAppointmentsByStatusAsync(DateTime? startDate = null, DateTime? endDate = null, int? branchId = null, List<int>? serviceIds = null, string? status = null);

        // Customer Demographics Report Method
        Task<List<CustomerDemographicsDto>> GetCustomerDemographicsAsync(int? branchId, string? gender = null, DateTime? birthDateStart = null, DateTime? birthDateEnd = null);
    }
}
