using BusinessManagementReporting.Core.DTOs.Booking;
using BusinessManagementReporting.Core.DTOs.ResponseModel;
using BusinessManagementReporting.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementReporting.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(IBookingService bookingService, ILogger<BookingsController> logger)
        {
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookingDto>>>> GetBookings()
        {
            _logger.LogInformation("Retrieving all bookings");

            try
            {
                var bookings = await _bookingService.GetAllBookingsAsync();
                _logger.LogInformation("Successfully retrieved bookings");
                return Ok(ApiResponse<IEnumerable<BookingDto>>.SuccessResponse(bookings, "Bookings retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving bookings");
                return StatusCode(500, ApiResponse<IEnumerable<BookingDto>>.ErrorResponse("An error occurred while retrieving bookings."));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<BookingDto>>> GetBooking(int id)
        {
            _logger.LogInformation("Retrieving booking with ID: {Id}", id);

            try
            {
                var booking = await _bookingService.GetBookingByIdAsync(id);
                _logger.LogInformation("Successfully retrieved booking with ID: {Id}", id);
                return Ok(ApiResponse<BookingDto>.SuccessResponse(booking, "Booking retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving booking with ID: {Id}", id);
                return NotFound(ApiResponse<BookingDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateBooking(BookingCreateDto bookingDto)
        {
            _logger.LogInformation("Creating new booking");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for booking creation");
                return BadRequest(ApiResponse<int>.ErrorResponse("Invalid data provided."));
            }

            try
            {
                var createdBookingId = await _bookingService.AddBookingAsync(bookingDto);
                _logger.LogInformation("Successfully created booking with ID: {Id}", createdBookingId);
                return CreatedAtAction(nameof(GetBooking), new { id = createdBookingId }, ApiResponse<int>.SuccessResponse(createdBookingId, "Booking created successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating booking");
                return StatusCode(500, ApiResponse<int>.ErrorResponse("An error occurred while creating the booking."));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateBooking(int id, BookingUpdateDto bookingDto)
        {
            _logger.LogInformation("Updating booking with ID: {Id}", id);

            if (id != bookingDto.BookingId)
            {
                _logger.LogWarning("Booking ID mismatch in update request. Route ID: {RouteId}, DTO ID: {DtoId}", id, bookingDto.BookingId);
                return BadRequest(ApiResponse<string>.ErrorResponse("Booking ID mismatch."));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for booking update");
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data provided."));
            }

            try
            {
                await _bookingService.UpdateBookingAsync(id, bookingDto);
                _logger.LogInformation("Successfully updated booking with ID: {Id}", id);
                return Ok(ApiResponse<string>.SuccessResponse("Booking updated successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating booking with ID: {Id}", id);
                return NotFound(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteBooking(int id)
        {
            _logger.LogInformation("Deleting booking with ID: {Id}", id);

            try
            {
                await _bookingService.DeleteBookingAsync(id);
                _logger.LogInformation("Successfully deleted booking with ID: {Id}", id);
                return Ok(ApiResponse<string>.SuccessResponse("Booking deleted successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting booking with ID: {Id}", id);
                return NotFound(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }
    }
}
