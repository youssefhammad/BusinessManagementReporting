using BusinessManagementReporting.Core.DTOs.Report.Appointment;
using BusinessManagementReporting.Core.DTOs.Report.CustomerDemographics;
using BusinessManagementReporting.Core.DTOs.Report.Revenue;
using BusinessManagementReporting.Core.Interfaces;
using BusinessManagementReporting.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RevenueReportDto> GetRevenueReportAsync(DateTime? startDate = null, DateTime? endDate = null, int? branchId = null, List<int>? serviceIds = null, string? paymentMethod = null)
        {
            var totalRevenue = await _unitOfWork.ReportRepository.GetTotalRevenueAsync(startDate, endDate, branchId, serviceIds, paymentMethod);
            var revenueByService = await _unitOfWork.ReportRepository.GetRevenueByServiceAsync(startDate, endDate, branchId, serviceIds, paymentMethod);
            var revenueByBranch = await _unitOfWork.ReportRepository.GetRevenueByBranchAsync(startDate, endDate, serviceIds, paymentMethod);
            var revenueByPaymentMethod = await _unitOfWork.ReportRepository.GetRevenueByPaymentMethodAsync(startDate, endDate, branchId, serviceIds);

            var report = new RevenueReportDto
            {
                TotalRevenue = totalRevenue,
                RevenueByService = revenueByService,
                RevenueByBranch = revenueByBranch,
                RevenueByPaymentMethod = revenueByPaymentMethod
            };

            return report;
        }

        public async Task<AppointmentReportDto> GetAppointmentReportAsync(
           DateTime? startDate = null,
           DateTime? endDate = null,
           int? branchId = null,
           List<int>? serviceIds = null,
           string? status = null)
        {
            var totalAppointments = await _unitOfWork.ReportRepository.GetTotalAppointmentsAsync(
                startDate, endDate, branchId, serviceIds, status);

            var appointmentsByService = await _unitOfWork.ReportRepository.GetAppointmentsByServiceAsync(
                startDate, endDate, branchId, serviceIds);

            var appointmentsByBranch = await _unitOfWork.ReportRepository.GetAppointmentsByBranchAsync(
                startDate, endDate, branchId, serviceIds, status);

            var appointmentsByStatus = await _unitOfWork.ReportRepository.GetAppointmentsByStatusAsync(
                startDate, endDate, branchId, serviceIds, status);


            var appointmentReport = new AppointmentReportDto
            {
                TotalAppointments = totalAppointments,
                AppointmentsByService = appointmentsByService,
                AppointmentsByBranch = appointmentsByBranch,
                AppointmentsByStatus = appointmentsByStatus
            };

            return appointmentReport;
        }

        public async Task<CustomerDemographicsReportDto> GetCustomerDemographicsReportAsync(int? branchId, string? gender = null, DateTime? birthDateStart = null, DateTime? birthDateEnd = null)
        {
            var customerDataList = await _unitOfWork.ReportRepository.GetCustomerDemographicsAsync(branchId, gender, birthDateStart, birthDateEnd);

            var customerDemographicsList = customerDataList.Select(c => new CustomerDemographicsDto
            {
                ClientId = c.ClientId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Gender = c.Gender,
                Age = CalculateAge(c.Birthdate), 
                Birthdate = c.Birthdate,
                Branches = c.Branches
            }).ToList();

            var customerDemographicsReport = new CustomerDemographicsReportDto
            {
                CustomerDemographics = customerDemographicsList
            };

            return customerDemographicsReport;
        }

        private int CalculateAge(DateTime birthdate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;
            if (birthdate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
