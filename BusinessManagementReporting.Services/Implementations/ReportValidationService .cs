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
        private readonly string[] _validPaymentMethods = { "cash", "credit card", "debit card" };

        public (bool IsValid, string? ErrorMessage) ValidateReportRequest(ReportRequest request)
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

            if (!string.IsNullOrEmpty(request.PaymentMethod) &&
                !_validPaymentMethods.Contains(request.PaymentMethod.ToLower()))
            {
                return (false, "Invalid payment method.");
            }

            return (true, null);
        }

        public (bool IsValid, string ErrorMessage) ValidateCustomerDemographicsReportRequest(CustomerDemographicsReportRequest request)
        {
            if (request.BirthDateStart.HasValue && request.BirthDateEnd.HasValue && request.BirthDateStart > request.BirthDateEnd)
            {
                return (false, "Birth date start must be earlier than or equal to birth date end.");
            }

            if (!string.IsNullOrEmpty(request.Gender) && !new[] { "male", "female", "other" }.Contains(request.Gender.ToLower()))
            {
                return (false, "Invalid gender specified. Allowed values are 'Male', 'Female', or 'Other'.");
            }

            if (request.BranchId.HasValue && request.BranchId <= 0)
            {
                return (false, "Invalid branch ID.");
            }

            return (true, string.Empty);
        }
    }
}
