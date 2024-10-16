using BusinessManagementReporting.Core.DTOs.BookingService;
using BusinessManagementReporting.Core.DTOs.ResponseModel;
using BusinessManagementReporting.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementReporting.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingServicesController : ControllerBase
    {
        private readonly IBookingServiceService _bookingServiceService;
        private readonly ILogger<BookingServicesController> _logger;

        public BookingServicesController(IBookingServiceService bookingServiceService, ILogger<BookingServicesController> logger)
        {
            _bookingServiceService = bookingServiceService ?? throw new ArgumentNullException(nameof(bookingServiceService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookingServiceDto>>>> GetBookingServices()
        {
            _logger.LogInformation("Retrieving all booking services");

            try
            {
                var bookingServices = await _bookingServiceService.GetAllBookingServicesAsync();
                _logger.LogInformation("Successfully retrieved booking services");
                return Ok(ApiResponse<IEnumerable<BookingServiceDto>>.SuccessResponse(bookingServices, "Booking services retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving booking services");
                return StatusCode(500, ApiResponse<IEnumerable<BookingServiceDto>>.ErrorResponse("An error occurred while retrieving booking services."));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<BookingServiceDto>>> GetBookingService(int id)
        {
            _logger.LogInformation("Retrieving booking service with ID: {Id}", id);

            try
            {
                var bookingService = await _bookingServiceService.GetBookingServiceByIdAsync(id);
                _logger.LogInformation("Successfully retrieved booking service with ID: {Id}", id);
                return Ok(ApiResponse<BookingServiceDto>.SuccessResponse(bookingService, "Booking service retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving booking service with ID: {Id}", id);
                return NotFound(ApiResponse<BookingServiceDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateBookingService(BookingServiceCreateDto bookingServiceDto)
        {
            _logger.LogInformation("Creating new booking service");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for booking service creation");
                return BadRequest(ApiResponse<int>.ErrorResponse("Invalid data provided."));
            }

            try
            {
                var createdBookingServiceId = await _bookingServiceService.AddBookingServiceAsync(bookingServiceDto);
                _logger.LogInformation("Successfully created booking service with ID: {Id}", createdBookingServiceId);
                return CreatedAtAction(nameof(GetBookingService), new { id = createdBookingServiceId }, ApiResponse<int>.SuccessResponse(createdBookingServiceId, "Booking service created successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating booking service");
                return StatusCode(500, ApiResponse<int>.ErrorResponse("An error occurred while creating the booking service."));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateBookingService(int id, BookingServiceUpdateDto bookingServiceDto)
        {
            _logger.LogInformation("Updating booking service with ID: {Id}", id);

            if (id != bookingServiceDto.BookingServiceId)
            {
                _logger.LogWarning("Booking service ID mismatch in update request. Route ID: {RouteId}, DTO ID: {DtoId}", id, bookingServiceDto.BookingServiceId);
                return BadRequest(ApiResponse<string>.ErrorResponse("Booking service ID mismatch."));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for booking service update");
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data provided."));
            }

            try
            {
                await _bookingServiceService.UpdateBookingServiceAsync(id, bookingServiceDto);
                _logger.LogInformation("Successfully updated booking service with ID: {Id}", id);
                return Ok(ApiResponse<string>.SuccessResponse("Booking service updated successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating booking service with ID: {Id}", id);
                return NotFound(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteBookingService(int id)
        {
            _logger.LogInformation("Deleting booking service with ID: {Id}", id);

            try
            {
                await _bookingServiceService.DeleteBookingServiceAsync(id);
                _logger.LogInformation("Successfully deleted booking service with ID: {Id}", id);
                return Ok(ApiResponse<string>.SuccessResponse("Booking service deleted successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting booking service with ID: {Id}", id);
                return NotFound(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }
    }
}
