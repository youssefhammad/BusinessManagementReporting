using BusinessManagementReporting.Core.DTOs.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementReporting.Services.Interfaces
{
    public interface IReportValidationService
    {
        (bool IsValid, string? ErrorMessage) ValidateReportRequest(ReportRequest request);
        (bool IsValid, string ErrorMessage) ValidateCustomerDemographicsReportRequest(CustomerDemographicsReportRequest request);
    }
}
