using BusinessManagementReporting.Core.DTOs.Report.Appointment;
using BusinessManagementReporting.Core.DTOs.Report.CustomerDemographics;
using BusinessManagementReporting.Core.DTOs.Report.Revenue;
using BusinessManagementReporting.Core.Entities;
using BusinessManagementReporting.Core.Interfaces;
using BusinessManagementReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Revenue Report Methods

        public async Task<decimal> GetTotalRevenueAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? branchId = null,
            List<int>? serviceIds = null,
            string? paymentMethod = null)
        {
            var query = _context.Transactions
                .Include(t => t.Booking)
                .AsQueryable();

            query = ApplyCommonTransactionFilters(query, startDate, endDate, branchId, serviceIds, paymentMethod);

            var totalRevenue = await query.SumAsync(t => t.Amount);

            return totalRevenue;
        }

        public async Task<List<RevenueByServiceDto>> GetRevenueByServiceAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? branchId = null,
            List<int>? serviceIds = null,
            string? paymentMethod = null)
        {
            var query = _context.BookingServices
                .Include(bs => bs.Booking)
                    .ThenInclude(b => b.Transactions)
                .Include(bs => bs.Service)
                .AsQueryable();

            query = ApplyCommonBookingServiceFilters(query, startDate, endDate, branchId, serviceIds, paymentMethod);

            var result = await query
                .GroupBy(bs => new { bs.ServiceId, bs.Service.Name })
                .Select(g => new RevenueByServiceDto
                {
                    ServiceId = g.Key.ServiceId,
                    ServiceName = g.Key.Name,
                    TotalRevenue = g.Sum(bs => bs.Price)
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<RevenueByBranchDto>> GetRevenueByBranchAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            List<int>? serviceIds = null,
            string? paymentMethod = null)
        {
            var query = _context.Transactions
                .Include(t => t.Booking)
                    .ThenInclude(b => b.Branch)
                .AsQueryable();

            query = ApplyCommonTransactionFilters(query, startDate, endDate, null, serviceIds, paymentMethod);

            var result = await query
                .GroupBy(t => new { t.Booking.BranchId, t.Booking.Branch.Name })
                .Select(g => new RevenueByBranchDto
                {
                    BranchId = g.Key.BranchId,
                    BranchName = g.Key.Name,
                    TotalRevenue = g.Sum(t => t.Amount)
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<RevenueByPaymentMethodDto>> GetRevenueByPaymentMethodAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? branchId = null,
            List<int>? serviceIds = null)
        {
            var query = _context.Transactions
                .Include(t => t.Booking)
                .AsQueryable();

            query = ApplyCommonTransactionFilters(query, startDate, endDate, branchId, serviceIds, null);

            var result = await query
                .GroupBy(t => t.PaymentMethod)
                .Select(g => new RevenueByPaymentMethodDto
                {
                    PaymentMethod = g.Key,
                    TotalRevenue = g.Sum(t => t.Amount)
                })
                .ToListAsync();

            return result;
        }

        #endregion

        #region Appointment Report Methods

        public async Task<int> GetTotalAppointmentsAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? branchId = null,
            List<int>? serviceIds = null,
            string? paymentMethod = null)
        {
            var query = _context.Bookings.AsQueryable();

            query = ApplyCommonAppointmentFilters(query, startDate, endDate, branchId, serviceIds, paymentMethod);

            return await query.CountAsync();
        }

        public async Task<List<AppointmentsByServiceDto>> GetAppointmentsByServiceAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? branchId = null,
            List<int>? serviceIds = null,
            string? paymentMethod = null)
        {
            var query = _context.BookingServices
                .Include(bs => bs.Booking)
                .Include(bs => bs.Service)
                .AsQueryable();

            query = ApplyCommonBookingServiceFilters(query, startDate, endDate, branchId, serviceIds, paymentMethod);

            var result = await query
                .GroupBy(bs => new { bs.ServiceId, bs.Service.Name })
                .Select(g => new AppointmentsByServiceDto
                {
                    ServiceId = g.Key.ServiceId,
                    ServiceName = g.Key.Name,
                    AppointmentCount = g.Count()
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<AppointmentsByBranchDto>> GetAppointmentsByBranchAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? branchId = null,
            List<int>? serviceIds = null,
            string? paymentMethod = null)
        {
            var query = _context.Bookings
                .Include(b => b.Branch)
                .AsQueryable();

            query = ApplyCommonAppointmentFilters(query, startDate, endDate, branchId, serviceIds, paymentMethod);

            var result = await query
                .GroupBy(b => new { b.BranchId, b.Branch.Name })
                .Select(g => new AppointmentsByBranchDto
                {
                    BranchId = g.Key.BranchId,
                    BranchName = g.Key.Name,
                    AppointmentCount = g.Count()
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<AppointmentsByStatusDto>> GetAppointmentsByStatusAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? branchId = null,
            List<int>? serviceIds = null,
            string? paymentMethod = null)
        {
            var query = _context.Bookings.AsQueryable();

            query = ApplyCommonAppointmentFilters(query, startDate, endDate, branchId, serviceIds, paymentMethod);

            var result = await query
                .GroupBy(b => b.Status)
                .Select(g => new AppointmentsByStatusDto
                {
                    Status = g.Key,
                    AppointmentCount = g.Count()
                })
                .ToListAsync();

            return result;
        }

        #endregion

        #region Customer Demographics Report Method

        public async Task<List<CustomerDemographicsDto>> GetCustomerDemographicsAsync(int? branchId, string? gender = null, DateTime? birthDateStart = null, DateTime? birthDateEnd = null)
        {
            var query = _context.Clients
                .Include(c => c.Bookings)
                    .ThenInclude(b => b.Branch)
                .AsQueryable();

            if (branchId.HasValue)
                query = query.Where(c => c.Bookings.Any(b => b.BranchId == branchId.Value));

            if (!string.IsNullOrEmpty(gender))
                query = query.Where(c => c.Gender == gender);

            if (birthDateStart.HasValue)
                query = query.Where(c => c.Birthdate >= birthDateStart.Value);

            if (birthDateEnd.HasValue)
                query = query.Where(c => c.Birthdate <= birthDateEnd.Value);

            var result = await query
                .Select(c => new CustomerDemographicsDto
                {
                    ClientId = c.ClientId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Gender = c.Gender,
                    Birthdate = c.Birthdate,
                    Branches = c.Bookings
                        .Select(b => b.Branch.Name)
                        .Distinct()
                        .ToList(),
                })
                .ToListAsync();

            return result;
        }

        #endregion

        #region Filter Methods

        private IQueryable<Transaction> ApplyCommonTransactionFilters(
            IQueryable<Transaction> query,
            DateTime? startDate,
            DateTime? endDate,
            int? branchId,
            List<int>? serviceIds,
            string? paymentMethod)
        {
            if (startDate.HasValue)
                query = query.Where(t => t.PaymentDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.PaymentDate <= endDate.Value);

            if (branchId.HasValue)
                query = query.Where(t => t.Booking.BranchId == branchId.Value);

            if (serviceIds != null && serviceIds.Any())
                query = query.Where(t => t.Booking.BookingServices.Any(bs => serviceIds.Contains(bs.ServiceId)));

            if (!string.IsNullOrEmpty(paymentMethod))
                query = query.Where(t => t.PaymentMethod == paymentMethod);

            return query;
        }

        private IQueryable<BookingService> ApplyCommonBookingServiceFilters(
            IQueryable<BookingService> query,
            DateTime? startDate,
            DateTime? endDate,
            int? branchId,
            List<int>? serviceIds,
            string? paymentMethod)
        {
            if (startDate.HasValue || endDate.HasValue || !string.IsNullOrEmpty(paymentMethod))
            {
                query = query.Where(bs => bs.Booking.Transactions.Any(t =>
                    (!startDate.HasValue || t.PaymentDate >= startDate.Value) &&
                    (!endDate.HasValue || t.PaymentDate <= endDate.Value) &&
                    (string.IsNullOrEmpty(paymentMethod) || t.PaymentMethod == paymentMethod)
                ));
            }

            if (branchId.HasValue)
                query = query.Where(bs => bs.Booking.BranchId == branchId.Value);

            if (serviceIds != null && serviceIds.Any())
                query = query.Where(bs => serviceIds.Contains(bs.ServiceId));

            return query;
        }

        private IQueryable<Booking> ApplyCommonAppointmentFilters(
            IQueryable<Booking> query,
            DateTime? startDate,
            DateTime? endDate,
            int? branchId,
            List<int>? serviceIds,
            string? paymentMethod)
        {
            if (startDate.HasValue)
                query = query.Where(b => b.BookingDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(b => b.BookingDate <= endDate.Value);

            if (branchId.HasValue)
                query = query.Where(b => b.BranchId == branchId.Value);

            if (serviceIds != null && serviceIds.Any())
                query = query.Where(b => b.BookingServices.Any(bs => serviceIds.Contains(bs.ServiceId)));

            if (!string.IsNullOrEmpty(paymentMethod))
                query = query.Where(b => b.Transactions.Any(t => t.PaymentMethod == paymentMethod));

            return query;
        }

        #endregion
    }
}