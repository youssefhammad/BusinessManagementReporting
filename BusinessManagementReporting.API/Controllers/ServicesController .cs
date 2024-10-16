using BusinessManagementReporting.Core.DTOs.ResponseModel;
using BusinessManagementReporting.Core.DTOs.Service;
using BusinessManagementReporting.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementReporting.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        private readonly ILogger<ServicesController> _logger;

        public ServicesController(IServiceService serviceService, ILogger<ServicesController> logger)
        {
            _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ServiceDto>>>> GetServices()
        {
            _logger.LogInformation("Retrieving all services");

            try
            {
                var services = await _serviceService.GetAllServicesAsync();
                _logger.LogInformation("Successfully retrieved services");
                return Ok(ApiResponse<IEnumerable<ServiceDto>>.SuccessResponse(services, "Services retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving services");
                return StatusCode(500, ApiResponse<IEnumerable<ServiceDto>>.ErrorResponse("An error occurred while retrieving services."));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ServiceDto>>> GetService(int id)
        {
            _logger.LogInformation("Retrieving service with ID: {Id}", id);

            try
            {
                var service = await _serviceService.GetServiceByIdAsync(id);
                _logger.LogInformation("Successfully retrieved service with ID: {Id}", id);
                return Ok(ApiResponse<ServiceDto>.SuccessResponse(service, "Service retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving service with ID: {Id}", id);
                return NotFound(ApiResponse<ServiceDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateService(ServiceCreateDto serviceDto)
        {
            _logger.LogInformation("Creating new service");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for service creation");
                return BadRequest(ApiResponse<int>.ErrorResponse("Invalid data provided."));
            }

            try
            {
                var createdServiceId = await _serviceService.AddServiceAsync(serviceDto);
                _logger.LogInformation("Successfully created service with ID: {Id}", createdServiceId);
                return CreatedAtAction(nameof(GetService), new { id = createdServiceId }, ApiResponse<int>.SuccessResponse(createdServiceId, "Service created successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating service");
                return StatusCode(500, ApiResponse<int>.ErrorResponse("An error occurred while creating the service."));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateService(int id, ServiceUpdateDto serviceDto)
        {
            _logger.LogInformation("Updating service with ID: {Id}", id);

            if (id != serviceDto.ServiceId)
            {
                _logger.LogWarning("Service ID mismatch in update request. Route ID: {RouteId}, DTO ID: {DtoId}", id, serviceDto.ServiceId);
                return BadRequest(ApiResponse<string>.ErrorResponse("Service ID mismatch."));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for service update");
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data provided."));
            }

            try
            {
                await _serviceService.UpdateServiceAsync(id, serviceDto);
                _logger.LogInformation("Successfully updated service with ID: {Id}", id);
                return Ok(ApiResponse<string>.SuccessResponse("Service updated successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating service with ID: {Id}", id);
                return NotFound(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteService(int id)
        {
            _logger.LogInformation("Deleting service with ID: {Id}", id);

            try
            {
                await _serviceService.DeleteServiceAsync(id);
                _logger.LogInformation("Successfully deleted service with ID: {Id}", id);
                return Ok(ApiResponse<string>.SuccessResponse("Service deleted successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting service with ID: {Id}", id);
                return NotFound(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }
    }
}
