using BusinessManagementReporting.Core.DTOs.Report;
using BusinessManagementReporting.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Services.Implementations
{
    public class ReportValidationService : IReportValidationService
    {
        private static readonly string[] ValidPaymentMethods = { "cash", "credit card", "debit card" };
        private static readonly string[] ValidBookingStatuses = { "confirmed", "pending" };
        private static readonly string[] ValidGenders = { "male", "female", "other" };

        public (bool IsValid, string? ErrorMessage) ValidateRevenueReportRequest(RevenueReportRequest request)
        {
            var baseValidation = ValidateCommonFields(request);
            if (!baseValidation.IsValid) return baseValidation;

            return ValidatePaymentMethod(request.PaymentMethod);
        }

        public (bool IsValid, string? ErrorMessage) ValidateAppointmentReportRequest(AppointmentReportRequest request)
        {
            var baseValidation = ValidateCommonFields(request);
            if (!baseValidation.IsValid) return baseValidation;

            return ValidateBookingStatus(request.BookingStatus);
        }

        public (bool IsValid, string ErrorMessage) ValidateCustomerDemographicsReportRequest(CustomerDemographicsReportRequest request)
        {
            if (request.BirthDateStart.HasValue && request.BirthDateEnd.HasValue && request.BirthDateStart > request.BirthDateEnd)
            {
                return (false, "Birth date start must be earlier than or equal to birth date end.");
            }

            if (!string.IsNullOrEmpty(request.Gender) && !ValidGenders.Contains(request.Gender.ToLower()))
            {
                return (false, "Invalid gender specified. Allowed values are 'Male', 'Female', or 'Other'.");
            }

            if (request.BranchId.HasValue && request.BranchId <= 0)
            {
                return (false, "Invalid branch ID.");
            }

            return (true, string.Empty);
        }

        private static (bool IsValid, string? ErrorMessage) ValidateCommonFields(BaseReportRequest request)
        {
            if (request.StartDate.HasValue && request.EndDate.HasValue && request.StartDate > request.EndDate)
            {
                return (false, "Start date cannot be greater than end date.");
            }

            if (request.BranchId.HasValue && request.BranchId <= 0)
            {
                return (false, "Invalid branch ID.");
            }

            if (request.ServiceIds != null && request.ServiceIds.Any(id => id <= 0))
            {
                return (false, "One or more service IDs are invalid.");
            }

            return (true, null);
        }

        private static (bool IsValid, string? ErrorMessage) ValidatePaymentMethod(string? paymentMethod)
        {
            if (!string.IsNullOrEmpty(paymentMethod) && !ValidPaymentMethods.Contains(paymentMethod.ToLower()))
            {
                return (false, $"Invalid payment method. Valid choices are: {string.Join(", ", ValidPaymentMethods)}.");
            }
            return (true, null);
        }

        private static (bool IsValid, string? ErrorMessage) ValidateBookingStatus(string? bookingStatus)
        {
            if (!string.IsNullOrEmpty(bookingStatus) && !ValidBookingStatuses.Contains(bookingStatus.ToLower()))
            {
                return (false, $"Invalid Booking Status. Valid choices are: {string.Join(", ", ValidBookingStatuses)}.");
            }
            return (true, null);
        }
    }
}
