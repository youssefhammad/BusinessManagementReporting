using BusinessManagementReporting.Core.DTOs.Report;
using BusinessManagementReporting.Core.DTOs.Report.Appointment;
using BusinessManagementReporting.Core.DTOs.Report.CustomerDemographics;
using BusinessManagementReporting.Core.DTOs.Report.Revenue;
using BusinessManagementReporting.Core.DTOs.ResponseModel;
using BusinessManagementReporting.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BusinessManagementReporting.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IReportValidationService _validationService;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(IReportService reportService, IReportValidationService validationService, ILogger<ReportsController> logger)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("Revenue")]
        public async Task<ActionResult<ApiResponse<RevenueReportDto>>> GetRevenueReport([FromQuery] ReportRequest request)
        {
            _logger.LogInformation("Generating revenue report. Request: {@Request}", request);

            var (isValid, errorMessage) = _validationService.ValidateReportRequest(request);
            if (!isValid)
            {
                _logger.LogWarning("Validation failed for revenue report request. Error: {ErrorMessage}", errorMessage);
                return BadRequest(ApiResponse<RevenueReportDto>.ErrorResponse(errorMessage));
            }

            try
            {
                var report = await _reportService.GetRevenueReportAsync(
                    request.StartDate,
                    request.EndDate,
                    request.BranchId,
                    request.ServiceIds,
                    request.PaymentMethod
                );
                _logger.LogInformation("Revenue report generated successfully");
                return Ok(ApiResponse<RevenueReportDto>.SuccessResponse(report, "Revenue report generated successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating revenue report");
                return StatusCode(500, ApiResponse<RevenueReportDto>.ErrorResponse("An error occurred while generating the revenue report."));
            }
        }

        [HttpGet("Appointment")]
        public async Task<ActionResult<ApiResponse<AppointmentReportDto>>> GetAppointmentReport([FromQuery] ReportRequest request)
        {
            _logger.LogInformation("Generating appointment report. Request: {@Request}", request);

            var (isValid, errorMessage) = _validationService.ValidateReportRequest(request);
            if (!isValid)
            {
                _logger.LogWarning("Validation failed for appointment report request. Error: {ErrorMessage}", errorMessage);
                return BadRequest(ApiResponse<AppointmentReportDto>.ErrorResponse(errorMessage));
            }

            try
            {
                var report = await _reportService.GetAppointmentReportAsync(
                    request.StartDate,
                    request.EndDate,
                    request.BranchId,
                    request.ServiceIds
                );
                _logger.LogInformation("Appointment report generated successfully");
                return Ok(ApiResponse<AppointmentReportDto>.SuccessResponse(report, "Appointment report generated successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating appointment report");
                return StatusCode(500, ApiResponse<AppointmentReportDto>.ErrorResponse("An error occurred while generating the appointment report."));
            }
        }

        [HttpGet("CustomerDemographics")]
        public async Task<ActionResult<ApiResponse<CustomerDemographicsReportDto>>> GetCustomerDemographicsReport([FromQuery] CustomerDemographicsReportRequest request)
        {
            _logger.LogInformation("Generating customer demographics report. Request: {@Request}", request);

            var (isValid, errorMessage) = _validationService.ValidateCustomerDemographicsReportRequest(request);
            if (!isValid)
            {
                _logger.LogWarning("Validation failed for customer demographics report request. Error: {ErrorMessage}", errorMessage);
                return BadRequest(ApiResponse<CustomerDemographicsReportDto>.ErrorResponse(errorMessage));
            }

            try
            {
                var report = await _reportService.GetCustomerDemographicsReportAsync(
                    request.BranchId,
                    request.Gender,
                    request.BirthDateStart,
                    request.BirthDateEnd
                );
                _logger.LogInformation("Customer demographics report generated successfully");
                return Ok(ApiResponse<CustomerDemographicsReportDto>.SuccessResponse(report, "Customer demographics report generated successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating customer demographics report");
                return StatusCode(500, ApiResponse<CustomerDemographicsReportDto>.ErrorResponse("An error occurred while generating the customer demographics report."));
            }
        }
    }
}